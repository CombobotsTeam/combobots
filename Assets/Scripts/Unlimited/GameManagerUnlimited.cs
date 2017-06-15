using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManagerUnlimited : GameManager
{
    public int 						nbrOfSpawn = 3;				// Number of enemy spawn

	// /!\ BOSS NOT AVAILABLE
	private bool 					CheckBoss;					// If true, the next enemy will be a boss
	private int 					CheckBossEnergy;			// If reach 100, checkBoss will become true

	private float 					NbrEnemiesOnScreen = 0;		// Number of enemies curently display on the screen

	private float 					respawntime;				// Enemy cooldown respawn time (randomly generated)
	private float 					checktime;					// Time counter using to get the timelapse between each respawn

    private List<Vector3> 			SpawnerPositionList;		// Actual number of enemies 
	private Dictionary<int, GameObject> SpawnLineInfo = new Dictionary<int, GameObject> ();

	private CombinationGenerator	combinationGenerator = new CombinationGenerator();
	private ConfigurationEnemy		newconfigurationEnemy;

	private int						NumberOfEnemiesDestroyed = 0;
	private int 					DifficultyLevel = 1;

	/// Difficulty Informations /////////////////////////////
	/// 
	/// +----------------------------------------------+
	/// | Difficulty |  Life  | Buttons | respawn time |
	/// +------------+--------+---------+--------------+
	/// |     1      | 1 - 2  |  2 - 3  |     2 - 3    |
	/// |     2      | 2 - 3  |    3    |     3 - 4    |
	/// |     3      | 2 - 3  |  3 - 4  |     5 - 6    |
	/// |     4      | 3 - 4  |    4    |     6 - 7    |
	/// |     5      | 3 - 4  |  4 - 5  |     6 - 7    |
	/// +------------+--------+---------+--------------+
	/// 
	/////////////////////////////////////////////////////////

    protected override void Init()
	{
        //Check if instance already exists
        if (instance == null)
           	//if not, set instance to this
            instance = this;
        //If instance already exists and it's not this:
        else if (instance != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

		// Init scripts
        base.Combination = GetComponent<CombinationHandler>();
        base.WaveManager = GetComponent<WaveManager>();
        base.powerUp = GetComponent<PawnPowerUp>();
        base.cm = GetComponent<ComboManager>();

		// Init spawn informations ///////////////////////////
        SpawnerPositionList = new List<Vector3>();

        RectTransform Pos = GameObject.Find("Canvas").GetComponent<RectTransform>();
        RectTransform TopPos = GameObject.Find("Canvas/TopBackground").GetComponent<RectTransform>();

        Vector3 tmpPos = Pos.localPosition;

        // Set spawn position for Y AXIS
        tmpPos.y = TopPos.position.y;

		// Set spawn position for X AXIS
        Vector2 topRightCorner = new Vector2(1, 1);
        Vector2 edgeVector = Camera.main.ViewportToWorldPoint(topRightCorner);
        edgeVector *= 2;

        float width = edgeVector.x;
        for (int i = 0; i <= nbrOfSpawn; i++)
        {
            tmpPos.x = TopPos.position.x + ((width / (nbrOfSpawn + 1)) * (i + 1));
            SpawnerPositionList.Add(tmpPos);
        }

		// Set spawn informations values
        respawntime = 0.0f;
        checktime = 0.0f;
        CheckBoss = false;
        CheckBossEnergy = 0;
    }

    void MakeEnemy()
    {
		// Instanciate a basic enemy
        if (!CheckBoss)
        {
			// Find a random kind of enemy
            string PathToEnemiesPrefab = "Prefabs/UnlimitedEnemies/";
            string type = "Robot01";
            switch (Random.Range(0, 3))
            {
                case 0:
                    type = "Robot01";
                    break;

                case 1:
                    type = "Robot02";
                    break;

                case 2:
                    type = "Robot03";
                    break;

                default:
                    break;
            }

			// Get enemy prefab
            GameObject BasicEnemy = Resources.Load<GameObject>(PathToEnemiesPrefab + type);

            if (!BasicEnemy)
                Debug.LogError("Impossible to instantiate " + type + " (Path: " + PathToEnemiesPrefab + type + ")");

			// Get enemy spawn position (random)
            int randomIndex = Random.Range(0, SpawnerPositionList.Count - 1);
			randomIndex = AdjustSpawn (randomIndex, BasicEnemy.gameObject);

            RectTransform TopPos = GameObject.Find("Canvas/TopBackground").GetComponent<RectTransform>();

            Vector3 pos = SpawnerPositionList[randomIndex];
            pos.y = TopPos.position.y;
            
			float ysize = BasicEnemy.GetComponent<BoxCollider>().size.y;
            float ycenter = BasicEnemy.GetComponent<BoxCollider>().center.y;
            float yscale = BasicEnemy.GetComponent<Transform>().localScale.y;
            pos.y -= (ysize * (yscale / 2)) + (ycenter * yscale);

			// Instanciate the enemy
            BasicEnnemy enemy = Instantiate(BasicEnemy.gameObject, pos, Quaternion.identity).GetComponent<BasicEnnemy>();
			SpawnLineInfo [randomIndex] = enemy.gameObject;

			// Set up enemy data (random)
			int nbGold = Random.Range (0, 2) * 5;

			int nbLife;
			int nbButton;

			if (DifficultyLevel == 1) {
				nbLife = Random.Range (1, 3);
				nbButton = Random.Range (2, 4);
			} 
			else if (DifficultyLevel == 2) {
				nbLife = Random.Range (2, 4);
				nbButton = 3;
			}
			else if (DifficultyLevel == 3) {
				nbLife = Random.Range (2, 4);
				nbButton = Random.Range (3, 5);
			}
			else if (DifficultyLevel == 4) {
				nbLife = Random.Range (3, 5);
				nbButton = 4;
			}
			else {
				nbLife = Random.Range (3, 5);
				nbButton = Random.Range (4, 6);
			}

            enemy.Init(nbLife, nbGold, nbButton);
            if (!enemy)
                Debug.LogError("[WaveManager]Can't instanciate the enemy !");

            combinationGenerator.FixedSize = enemy.CombinationSize;
            enemy.Combination = combinationGenerator.GetListButton();

            enemy.Speed = 0.3f;

            enemy.Setup();
            NbrEnemiesOnScreen++;
            GameManager.instance.EnemiesOnScreen.Add(enemy.gameObject);

			// Update BOSS "timer"
            CheckBossEnergy += 0;

            if(CheckBossEnergy >= 100)
            {
                CheckBoss = true;
            }
        }

		// Instanciate a boss
        else
        {
            if (NbrEnemiesOnScreen == 0)
            {
                MakeBoss();
                CheckBossEnergy = 0;
            }
         }
    }

    void MakeBoss()
    {
        string PathToEnemiesPrefab = "Prefabs/Enemies/";
        string type = "BlackPawn";

        GameObject BasicEnemy = Resources.Load<GameObject>(PathToEnemiesPrefab + type);

        if (!BasicEnemy)
            Debug.LogError("Impossible to instantiate " + type + " (Path: " + PathToEnemiesPrefab + type + ")");

		int randomIndex = SpawnerPositionList.Count / 2 - ((SpawnerPositionList.Count % 2 == 0) ? 1 : 0);

        RectTransform TopPos = GameObject.Find("Canvas/TopBackground").GetComponent<RectTransform>();

        Vector3 pos = SpawnerPositionList[randomIndex];
        pos.y = TopPos.position.y;
        float ysize = BasicEnemy.GetComponent<BoxCollider>().size.y;
        float ycenter = BasicEnemy.GetComponent<BoxCollider>().center.y;
        float yscale = BasicEnemy.GetComponent<Transform>().localScale.y;
        pos.y -= (ysize * (yscale / 2)) + (ycenter * yscale);

        BasicEnnemy enemy = Instantiate(BasicEnemy.gameObject, pos, Quaternion.identity).GetComponent<BasicEnnemy>();
        enemy.GetComponent<BasicEnnemy>().Init(Random.Range(2, 5), 1, Random.Range(1, 4));
        if (!enemy)
            Debug.LogError("[WaveManager]Can't instanciate the enemy !");

        combinationGenerator.FixedSize = enemy.CombinationSize;
        enemy.Combination = combinationGenerator.GetListButton();
        enemy.Setup();
        NbrEnemiesOnScreen++;
        GameManager.instance.EnemiesOnScreen.Add(enemy.gameObject);
    }

    protected override void Update()
    {
        checkDeath();

        if (isPaused)
            return;

        if (LateInit)
        {
            SetLife();
            SetCoin();
            SetScore();
            SetCombo();
            soundManager.PlayerMusic("MusicInGame");
            LateInit = false;
        }

		// Check combo
		base.cm.checkCombo();

		// Set animation for the power up
        if (powerUp && AbilityEnabled && powerUp.Charge >= powerUp.ChargeMax)
            SetAbilityActive(true);

		// Delete dead enemy
        int i = 0;
        while (i < base.EnemiesOnScreen.Count)
        {
            if (base.EnemiesOnScreen[i].GetComponent<BasicEnnemy>().Died == true)
            {
                Destroy(EnemiesOnScreen[i], 1);
                base.EnemiesOnScreen.Remove(EnemiesOnScreen[i]);
            }
            else
                i++;
        }

		// Spawn enemies
        checktime += Time.deltaTime;
        if (respawntime < checktime)
        {
            MakeEnemy();
            checktime = 0.0f;

			if (DifficultyLevel == 1)
				respawntime = Random.Range(2.0f, 3.0f);
			else if (DifficultyLevel == 2)
				respawntime = Random.Range(2.0f, 3.0f);
			else if (DifficultyLevel == 3)
				respawntime = Random.Range(2.0f, 4.0f);
			else if (DifficultyLevel == 4)
				respawntime = Random.Range(2.0f, 5.0f);
			else 
				respawntime = Random.Range(2.0f, 5.0f);
        }
    }

    public override void NotifyDie(GameObject enemy, bool killedByPlayer)
    {
        BasicEnnemy e = enemy.GetComponent<BasicEnnemy>();

        e.Died = true;

        soundManager.Play("DeathEnemy", false);
        if (e == cm.lockEnemy)
            cm.lockEnemy = null;

        // GOLD 
        if (killedByPlayer)
        {
            Vector3 randomPos = new Vector3();
            GameObject c;
            float delay;

            for (int i = 1; i <= (e.NbrGold / 5); i++)
            {
                randomPos = Random.insideUnitCircle * 7;
                c = Instantiate(FloatingCoins, enemy.transform.position + randomPos, Quaternion.identity);
                delay = (float)i / 10.0f;
                c.GetComponent<CoinAnimation>().Play(delay);
            }
        }

        NbrEnemiesOnScreen--;

		NumberOfEnemiesDestroyed++;
		if (NumberOfEnemiesDestroyed % 8 == 0)
		{
			Debug.Log ("Add difficulty");
			DifficultyLevel++;
		}
    }

	int AdjustSpawn (int index, GameObject newEnemy)
	{
		int newIndex = index;

		// There is already an enemy on this line
		if (SpawnLineInfo.ContainsKey (newIndex))
		{

			if (SpawnLineInfo [newIndex] == null)
			{
				SpawnLineInfo.Remove (newIndex);
				return newIndex;
			}

			int bestIndex = newIndex;
			float bestDistance = 0;

			for (int i = 1; i <= nbrOfSpawn; i++)
			{
				if (!SpawnLineInfo.ContainsKey (newIndex))
					return (newIndex);

				if (SpawnLineInfo [newIndex] == null)
				{
					SpawnLineInfo.Remove (newIndex);
					return newIndex;
				}

				// Information about the enemy on the line
				GameObject enemy = SpawnLineInfo [newIndex];

				float ysizeEnemy = enemy.GetComponent<BoxCollider> ().size.y;
				float yscaleEnemy = enemy.GetComponent<Transform> ().localScale.y;

				// Information about the new enemy
				float ysizeNewEnemy = newEnemy.GetComponent<BoxCollider> ().size.y;
				float yscaleNewEnemy = newEnemy.GetComponent<Transform> ().localScale.y;

				// Distance calculation
				float minDistance = ysizeNewEnemy * yscaleNewEnemy;

				float begining = SpawnerPositionList [index].y;
				float ending = enemy.transform.position.y + (ysizeEnemy * (yscaleEnemy));

				// 2 enemy want to spawn in the same time
				if (begining - ending == minDistance)
					newIndex++;

				// Can't use this line, the distance is too short
				else if (begining - ending < minDistance)
				{
					// Update the best index
					if (begining - ending > bestDistance) {
						bestDistance = begining - ending;
						bestIndex = newIndex;
					}

					newIndex++;

					if (newIndex >= nbrOfSpawn)
						newIndex = 0;
				}
				else
				{ // Can use this line, even if there is already an enemy
					return newIndex;
				}
			}

			// All lines are too busy, so we just use the best one
			return bestIndex;

		}

		// Line empty, the ennemy can spawn
		else
			return newIndex;
	}
}

