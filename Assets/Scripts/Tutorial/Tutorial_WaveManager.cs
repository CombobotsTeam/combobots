﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_WaveManager : MonoBehaviour
{
    
    public List<GameObject> 	Waves;						// GameObject must be an empty GameObject with BasicEnemies as children
	public bool 				RandomWave;
	public int 					EnemyMax;					// Maximum number of enemies display
	public int					nbrOfSpawn = 5;             // Enemy's move down line number(1 one line down, 3 random of 3 line one)

	List< List<Tutorial_Ennemy> >	WaveLeft = new List< List<Tutorial_Ennemy> >();	
	List<Tutorial_Ennemy>			EnemiesForCurrentWave;

	float 						TimeLeft = 0;					// Min time between each enemy summon
	float						NbrEnemiesOnScreen = 0;		// Actual number of enemies 

	List<Vector3>				SpawnerPositionList;

    bool canSummon = true;

	// Update is called once per frame
	void Update () {}

	// Use this for initialization
	void Start ()
	{
		// Generate random waves
		if (RandomWave)
			GenerateRandomWave (); // Not implement yet

		// Use waves 
		else
		{
			// SET WaveLeft
			foreach (GameObject waveObj in Waves) 
			{
                Tutorial_Ennemy[] wave = waveObj.GetComponentsInChildren<Tutorial_Ennemy> ();

				List<Tutorial_Ennemy> tmpEnemyList = new List<Tutorial_Ennemy>();
				foreach (Tutorial_Ennemy enemy in wave)
					tmpEnemyList.Add (enemy);
				WaveLeft.Add (tmpEnemyList);
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
			Transform BasicEnemy = EnemiesForCurrentWave [0].transform;
			int randomIndex = Random.Range(0, SpawnerPositionList.Count - 1);
			//Debug.Log ("count : " + SpawnerPositionList.Count);

            Vector3 pos = SpawnerPositionList[randomIndex];

			float ysize = BasicEnemy.GetComponent<BoxCollider> ().size.y;
			float ycenter = BasicEnemy.GetComponent<BoxCollider> ().center.y;
			float yscale = BasicEnemy.GetComponent<Transform>().localScale.y;
			pos.y -= (ysize * (yscale / 2))+ (ycenter * yscale);

			Debug.Log ("test : " + BasicEnemy.GetComponent<BoxCollider> ().center.y);

            //pos.y -= BasicEnemy.GetComponent<BoxCollider>().size.y * BasicEnemy.GetComponent<Transform>().localScale.y / 2;
            Tutorial_Ennemy enemy = Instantiate(BasicEnemy.gameObject, pos, Quaternion.identity).GetComponent<Tutorial_Ennemy>();
            CombinationGenerator c = new CombinationGenerator();
            c.FixedSize = enemy.Combination.Count;
            enemy.Combination = c.GetListButton();
            enemy.Setup();
			NbrEnemiesOnScreen++;
            Tutorial_GameManager.instance.EnemiesOnScreen.Add(enemy.gameObject);

			if (EnemiesForCurrentWave.Count >= 2)
				TimeLeft = EnemiesForCurrentWave [1].SpawnCooldown;

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

		for (int i = 0; i <= nbrOfSpawn; i++) {
			tmpPos.x = TopPos.position.x + ((width / (nbrOfSpawn + 1)) * (i + 1));
			SpawnerPositionList.Add (tmpPos);
		}

        StartCoroutine("StartWaveManager");
    }
}
