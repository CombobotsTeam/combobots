using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManagerUnlimited : GameManager
{
    // Instance to turn GameManager into Singleton
    //public static GameManager instance = null;

    //public PawnPowerUp powerUp;
    //public bool isBoss = false;
    //public int Score = 0;//score of player
    //public int Life = 1;//life of player
    //public int ComboCount = 1;//player's combo count
    //public int Gold = 0;

    //// Will contain all the enemies on the screen (Not the enemies that will be instanciate)
    //public List<GameObject> EnemiesOnScreen = new List<GameObject>();

    //// Contain the current combination of button pressed
    //public CombinationHandler Combination;
    //[HideInInspector]
    //public WaveManager WaveManager;
    //public ComboManager cm;

    public int nbrOfSpawn = 3;
    List<Vector3> SpawnerPositionList;
    float NbrEnemiesOnScreen = 0;		// Actual number of enemies 
    private CombinationGenerator combinationGenerator = new CombinationGenerator();
    ConfigurationEnemy newconfigurationEnemy;
    float respowntime;
    float checktime;
    public bool CheckBoss;
    public int CheckBossEnergy;

    protected override void Init()
    {
        Life = 1;

        //Check if instance already exists
        if (instance == null)
            //if not, set instance to this
            instance = this;
        //If instance already exists and it's not this:
        else if (instance != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        base.Combination = GetComponent<CombinationHandler>();
        base.WaveManager = GetComponent<WaveManager>();
        base.powerUp = GetComponent<PawnPowerUp>();
        base.cm = GetComponent<ComboManager>();


        SpawnerPositionList = new List<Vector3>();
        RectTransform Pos = GameObject.Find("Canvas").GetComponent<RectTransform>();
        RectTransform TopPos = GameObject.Find("Canvas/TopBackground").GetComponent<RectTransform>();

        Vector3 tmpPos = Pos.localPosition;

        // Y AXIS
        tmpPos.y = TopPos.position.y;

        // X AXIS
        Vector2 topRightCorner = new Vector2(1, 1);
        Vector2 edgeVector = Camera.main.ViewportToWorldPoint(topRightCorner);
        edgeVector *= 2;

        float width = edgeVector.x;
        for (int i = 0; i <= nbrOfSpawn; i++)
        {
            tmpPos.x = TopPos.position.x + ((width / (nbrOfSpawn + 1)) * (i + 1));
            SpawnerPositionList.Add(tmpPos);
        }

        respowntime = 0.0f;
        checktime = 0.0f;
        CheckBoss = false;
        CheckBossEnergy = 0;
    }

    void MakeEnemy()
    {
        if (!CheckBoss)
        {
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

            GameObject BasicEnemy = Resources.Load<GameObject>(PathToEnemiesPrefab + type);

            if (!BasicEnemy)
                Debug.LogError("Impossible to instantiate " + type + " (Path: " + PathToEnemiesPrefab + type + ")");

            int randomIndex = Random.Range(0, SpawnerPositionList.Count - 1);

            RectTransform TopPos = GameObject.Find("Canvas/TopBackground").GetComponent<RectTransform>();

            Vector3 pos = SpawnerPositionList[randomIndex];
            pos.y = TopPos.position.y;
            float ysize = BasicEnemy.GetComponent<BoxCollider>().size.y;
            float ycenter = BasicEnemy.GetComponent<BoxCollider>().center.y;
            float yscale = BasicEnemy.GetComponent<Transform>().localScale.y;
            pos.y -= (ysize * (yscale / 2)) + (ycenter * yscale);

            BasicEnnemy enemy = Instantiate(BasicEnemy.gameObject, pos, Quaternion.identity).GetComponent<BasicEnnemy>();

			int nbGold = Random.Range (0, 2) * 5;

            enemy.Init(Random.Range(2, 5), nbGold, Random.Range(1, 4));
            if (!enemy)
                Debug.LogError("[WaveManager]Can't instanciate the enemy !");


            combinationGenerator.FixedSize = enemy.CombinationSize;
            enemy.Combination = combinationGenerator.GetListButton();
            enemy.Speed = 0.3f;
            enemy.Setup();
            NbrEnemiesOnScreen++;
            GameManager.instance.EnemiesOnScreen.Add(enemy.gameObject);

            CheckBossEnergy += 5;

            if(CheckBossEnergy > 100)
            {
                CheckBoss = true;
            }
        }
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

        int randomIndex = Random.Range(0, SpawnerPositionList.Count - 1);

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

    void EndGame()//set about gameover
    {
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
        
        if (powerUp && AbilityEnabled && powerUp.Charge >= powerUp.ChargeMax)
            SetAbilityActive(true);

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

        checktime += Time.deltaTime;
        if (respowntime < checktime)
        {
            MakeEnemy();
            checktime = 0.0f;
            respowntime = Random.Range(2.0f, 5.0f);
        }

        if (Life < 0)
        {
            EndGame();
        }

        base.cm.checkCombo();
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
    }
}

