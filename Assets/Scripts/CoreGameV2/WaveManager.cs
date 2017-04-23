using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {

    public List<GameObject> Waves = new List<GameObject>(); // GameObject must be an empty GameObject with BasicEnemies as children
	public bool 				RandomWave;
	public int 					EnemyMax;					// Maximum number of enemies display

    public int nbrOfSpawn = 5;

    private ConfigurationGame config;
    private CombinationGenerator combinationGenerator = new CombinationGenerator();


    List< List<ConfigurationEnemy> >	WaveLeft = new List< List<ConfigurationEnemy> >();	
	List<ConfigurationEnemy>			EnemiesForCurrentWave;

	float 						TimeLeft = 0;					// Min time between each enemy summon
	float						NbrEnemiesOnScreen = 0;		// Actual number of enemies 

	List<Vector3>				SpawnerPositionList;

    bool canSummon = true;

	// Update is called once per frame
	void Update () {}

	// Use this for initialization
	void Start ()
	{
        config = ConfigurationGame.instance;
		// Generate random waves
		if (RandomWave)
			GenerateRandomWave (); // Not implement yet

		// Use waves 
		else
		{
            int count = 0;
            GameObject waveTMP = null;
            List<BasicWaveClass> t = config.GenerateWaves();
             foreach (BasicWaveClass waveComponent in t)
             {
                waveTMP = new GameObject("Wave" + count);
                waveTMP.AddComponent<BasicWave>();
                waveTMP.GetComponent<BasicWave>().data = waveComponent;
                count++;
                Waves.Add(waveTMP);
             }
            foreach (GameObject waveObj in Waves)
            {
                //BasicEnnemy[] wave = waveObj.GetComponentsInChildren<BasicEnnemy> ();
                BasicWave wave = waveObj.GetComponent<BasicWave>();

                List<ConfigurationEnemy> tmpEnemyList = new List<ConfigurationEnemy>();
                foreach (ConfigurationEnemy enemy in wave.data.entities)
                    if (enemy.prefab.gameObject.GetComponent<BasicEnnemy>())
                        tmpEnemyList.Add(enemy);

                WaveLeft.Add(tmpEnemyList);
            }

            // SET EnemiesForCurrentWave
            EnemiesForCurrentWave = WaveLeft [0];
			WaveLeft.RemoveAt (0);
		}

		// SET TimeLeft
		/*if (EnemiesForCurrentWave.Count >= 2)
			TimeLeft = EnemiesForCurrentWave [1].SpawnCooldown;*/
 	}

	void Summon()
	{
		// New wave
		if (EnemiesForCurrentWave.Count <= 0 && NbrEnemiesOnScreen == 0)
		{
			if (WaveLeft.Count > 0)
			{
                TimeLeft = 5;
                EnemiesForCurrentWave = WaveLeft [0];
				WaveLeft.RemoveAt (0);
                StartCoroutine("SummonLater", TimeLeft);
                return;
            }
		}

		// The wave continue
		if (EnemiesForCurrentWave.Count >= 1)
		{
			Transform BasicEnemy = EnemiesForCurrentWave [0].prefab.transform;
			int randomIndex = Random.Range(0, SpawnerPositionList.Count - 1);
            Vector3 pos = SpawnerPositionList[randomIndex];

            float ysize = BasicEnemy.GetComponent<BoxCollider>().size.y;
            float ycenter = BasicEnemy.GetComponent<BoxCollider>().center.y;
            float yscale = BasicEnemy.GetComponent<Transform>().localScale.y;
            pos.y -= (ysize * (yscale / 2)) + (ycenter * yscale);


            BasicEnnemy enemy = Instantiate(BasicEnemy.gameObject, pos, Quaternion.identity).GetComponent<BasicEnnemy>();

            if (!enemy)
                Debug.LogError("[WaveManager]Can't instanciate the enemy !");

            enemy.UpdateWithConfig(EnemiesForCurrentWave[0]);

            combinationGenerator.FixedSize = enemy.CombinationSize;
            enemy.Combination = combinationGenerator.GetListButton();
            enemy.Setup();
			NbrEnemiesOnScreen++;
            GameManager.instance.EnemiesOnScreen.Add(enemy.gameObject);

			if (EnemiesForCurrentWave.Count >= 2)
				TimeLeft = EnemiesForCurrentWave [1].SpawnCoolDown;

			EnemiesForCurrentWave.RemoveAt(0);
		}
	}

	IEnumerator SummonLater(float delay)
	{
		float TimeUntilSummon = delay;

        canSummon = false;
        NbrEnemiesOnScreen++;

		while (TimeUntilSummon > 0)
		{
			TimeUntilSummon -= Time.deltaTime;
			yield return null;
		}

		NbrEnemiesOnScreen--;

        canSummon = true;
		Summon ();
	}

	void GenerateRandomWave()
	{
		// Implement it later
	}

	// The GameManager must call this function each time an enemy die
	public void EnemyDie(GameObject enemyObj)
	{
		// Use enemyObj for BossPart enemy

		NbrEnemiesOnScreen--;
	}

	// The GameManager must start this coroutine
	IEnumerator StartWaveManager()
	{
		while (Waves.Count > 0)
		{
			if (TimeLeft >= 0)
			{
				TimeLeft -= Time.deltaTime;
			}

			if (NbrEnemiesOnScreen < EnemyMax)
			{
				if (TimeLeft == 0) {
					Summon ();
				}

				else {
                    if (canSummon)
    					StartCoroutine ("SummonLater", TimeLeft);
				}
			}

			yield return null;
		}
	}

    public void launch()
    {
        // Set Spawner position
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

        for (int i = 0; i < SpawnerPositionList.Count; i++)
        //Debug.Log("Spawner Position " + SpawnerPositionList[i]);
        StartCoroutine("StartWaveManager");
    }
}
