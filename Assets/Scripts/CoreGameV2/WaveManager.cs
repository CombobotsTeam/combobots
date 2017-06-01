using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{

	[HideInInspector]
	public List<GameObject> Waves = new List<GameObject> (); 	// Contain all informations about the waves
	public bool RandomWave;
	public int EnemyMax;										// Maximum number of enemies display

	public int nbrOfSpawn = 5;									// Number of spawn area

	public WaveChange wc;
	private ConfigurationGame config;
	private CombinationGenerator combinationGenerator = new CombinationGenerator ();

	private List<List<ConfigurationEnemy>>	WaveLeft = new List<List<ConfigurationEnemy>> ();
	private List<string> TransitionLeft = new List<string> ();
	private List<float> TimeTransitionLeft = new List<float> ();
	private List<ConfigurationEnemy> EnemiesForCurrentWave;

	private float TimeLeft = 0;									// Min time between each enemy summon
	public float NbrEnemiesOnScreen = 0;						// Actual number of enemies

	private Dictionary<int, GameObject> SpawnLineInfo = new Dictionary<int, GameObject> ();

	private List<Vector3> SpawnerPositionList;

	private bool canSummon = true;

	// Use this for initialization
	void Start ()
	{
		//Instanciate config gameobject

		#if UNITY_EDITOR
			GameObject configGO = Resources.Load ("Prefabs/Waves/ConfigurationWaveForTesting") as GameObject;

			config = Instantiate (configGO, new Vector3 (), Quaternion.identity).GetComponent<ConfigurationGame>();
			config.gameObject.name = "ConfigurationWaveForTesting";
		#else
			GameObject scGO = GameObject.Find("SceneInfos");
			ScenesInfos sc = scGO.GetComponent<ScenesInfos> ();

			GameObject configGO = Resources.Load ("Prefabs/Waves/ConfigurationWave" + sc.actualChapter + "-" + sc.actualLevel) as GameObject;

			config = Instantiate (configGO, new Vector3 (), Quaternion.identity).GetComponent<ConfigurationGame>();//ConfigurationGame.instance;
			config.gameObject.name = "ConfigurationWave" + sc.actualChapter + "-" + sc.actualLevel;
		#endif

		// Generate random waves
		if (RandomWave)
			GenerateRandomWave (); // Not implement yet

		// Use config to instanciate wave 
		else
		{
			int count = 0;
			GameObject waveTMP = null;
			List<BasicWaveClass> t = config.GenerateWaves ();

			foreach (BasicWaveClass waveComponent in t)
			{
				waveTMP = new GameObject ("Wave" + count);
				waveTMP.AddComponent<BasicWave> ();
				waveTMP.GetComponent<BasicWave> ().data = waveComponent;
				count++;
				Waves.Add (waveTMP);
			}

			foreach (GameObject waveObj in Waves)
			{
				BasicWave wave = waveObj.GetComponent<BasicWave> ();

				List<ConfigurationEnemy> tmpEnemyList = new List<ConfigurationEnemy> ();

				foreach (ConfigurationEnemy enemy in wave.data.entities)
					if (enemy.prefab.gameObject.GetComponent<BasicEnnemy> ())
						tmpEnemyList.Add (enemy);
				
				TransitionLeft.Add (wave.data.TextDisplay);
				TimeTransitionLeft.Add (wave.data.TimeBetweenWave);
				WaveLeft.Add (tmpEnemyList);
			}

			// INIT settings for first wave
			EnemiesForCurrentWave = WaveLeft [0];

			wc.changeWave (TimeTransitionLeft [0], TransitionLeft [0]);
			TransitionLeft.RemoveAt (0);

			TimeLeft = TimeTransitionLeft [0];
			TimeTransitionLeft.RemoveAt (0);

			WaveLeft.RemoveAt (0);
		}
	}

	void Summon ()
	{
		// The wave continue
		if (EnemiesForCurrentWave.Count >= 1)
		{
			Transform BasicEnemy = EnemiesForCurrentWave [0].prefab.transform;
			int randomIndex = Random.Range (0, SpawnerPositionList.Count - 1);

			// Summon a BOSS
			if (EnemiesForCurrentWave [0].t == ConfigurationEnemy.Type.Boss)
			{
				GameManager.instance.isBoss = true;
				randomIndex = SpawnerPositionList.Count / 2 - ((SpawnerPositionList.Count % 2 == 0) ? 1 : 0);
				SoundManager.instance.PlayerMusic ("MusicBossInGame");
			}
			else
				GameManager.instance.isBoss = false;

			// Instanciate a basic enemy
			//randomIndex = AdjustSpawn (randomIndex, BasicEnemy.gameObject); 			////////// ADJUST SPAWN
			Vector3 pos = SpawnerPositionList [randomIndex];

			float ysize = BasicEnemy.GetComponent<BoxCollider> ().size.y;
			float ycenter = BasicEnemy.GetComponent<BoxCollider> ().center.y;
			float yscale = BasicEnemy.GetComponent<Transform> ().localScale.y;
			pos.y -= (ysize * (yscale / 2)) + (ycenter * yscale);

			BasicEnnemy enemy = Instantiate (BasicEnemy.gameObject, pos, Quaternion.identity).GetComponent<BasicEnnemy> ();
			SpawnLineInfo [randomIndex] = enemy.gameObject;

			if (!enemy)
				Debug.LogError ("[WaveManager]Can't instanciate the enemy !");

			// SETUP the new enemy
			enemy.UpdateWithConfig (EnemiesForCurrentWave [0]);

			combinationGenerator.FixedSize = enemy.CombinationSize;
			enemy.Combination = combinationGenerator.GetListButton ();

			enemy.Setup ();

			NbrEnemiesOnScreen++;
			GameManager.instance.EnemiesOnScreen.Add (enemy.gameObject);

			if (EnemiesForCurrentWave.Count >= 2)
				TimeLeft = EnemiesForCurrentWave [0].SpawnCoolDown;

			EnemiesForCurrentWave.RemoveAt (0);
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

			Debug.Log ("Il y a deja un index");
			Debug.Log ("Ancien index : " + index);

			int bestIndex = newIndex;
			float bestDistance = 0;

			for (int i = 1; i <= nbrOfSpawn; i++)
			{
				if (!SpawnLineInfo.ContainsKey (newIndex))
					return (newIndex);

				// Information about the enemy on the line
				GameObject enemy = SpawnLineInfo [newIndex];

				float ysizeEnemy = enemy.GetComponent<BoxCollider> ().size.y;
				float yscaleEnemy = enemy.GetComponent<Transform> ().localScale.y;

				// Information about the new enemy
				float ysizeNewEnemy = newEnemy.GetComponent<BoxCollider> ().size.y;
				float yscaleNewEnemy = newEnemy.GetComponent<Transform> ().localScale.y;

				// Distance calculation
				float minDistance = ysizeNewEnemy;

				float begining = SpawnerPositionList [index].y - (ysizeNewEnemy * (yscaleNewEnemy));
				float ending = enemy.transform.position.y + (ysizeEnemy * (yscaleEnemy / 2));

				Debug.Log ("BEGINING : " + begining);
				Debug.Log ("ENDING : " + ending);

				// 2 enemy want to spawn in the same time
				if (begining - ending > 0)
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
					Debug.Log("1 - Nouvel index : " + newIndex);
					return newIndex;
				}
			}

			// All lines are too busy, so we just use the best one
			Debug.Log("Bestindex");
			return bestIndex;
			
		}

		// Line empty, the ennemy can spawn
		else
			return newIndex;
	}

	IEnumerator SummonLater (float delay)
	{
		canSummon = false;

		yield return GameManager.instance.WaitFor (delay);

		canSummon = true;
		Summon ();
	}

	void GenerateRandomWave ()
	{
		// Implement it later
	}

	IEnumerator Victory (float time)
	{
		yield return new WaitForSeconds (time);
		GameManager.instance.LoadVictory ();
	}

	// The GameManager must call this function each time an enemy die
	public void EnemyDie (GameObject enemyObj)
	{
		NbrEnemiesOnScreen--;

		if (NbrEnemiesOnScreen == 0 && EnemiesForCurrentWave.Count == 0)
		{
			// No more waves, the player won
			if (WaveLeft.Count == 0)
				StartCoroutine ("Victory", 2);

			// Load a new wave
			else
			{
				EnemiesForCurrentWave = WaveLeft [0];
				TimeLeft = TimeTransitionLeft [0];

				// Display the message betweent 2 waves
				wc.changeWave (TimeTransitionLeft [0], TransitionLeft [0]);

				TransitionLeft.RemoveAt (0);
				TimeTransitionLeft.RemoveAt (0);
				WaveLeft.RemoveAt (0);

				// summon the first enemy after the break time
				StartCoroutine ("SummonLater", TimeLeft);
			}
		}

	}

	public void Spawn (ConfigurationEnemy enemy)
	{
		EnemiesForCurrentWave.Add (enemy);
	}
		
	IEnumerator StartWaveManager ()
	{
		while (Waves.Count > 0)
		{
			// Decrease the time each frame
			if (TimeLeft >= 0)
				TimeLeft -= Time.deltaTime;

			// Summon enemies
			if (NbrEnemiesOnScreen < EnemyMax && EnemiesForCurrentWave.Count > 0)
			{
				if (TimeLeft <= 0 && canSummon)
					Summon ();
				else
				{
					if (canSummon)
						StartCoroutine ("SummonLater", TimeLeft);
				}
			}

			yield return null;
		}
	}

	// The GameManager must start this coroutine
	public void launch ()
	{
		// Set Spawner position
		SpawnerPositionList = new List<Vector3> ();
		RectTransform Pos = GameObject.Find ("Canvas").GetComponent<RectTransform> ();
		RectTransform TopPos = GameObject.Find ("Canvas/TopBackground").GetComponent<RectTransform> ();

		Vector3 tmpPos = Pos.localPosition;

		// Y AXIS
		tmpPos.y = TopPos.position.y;

		// X AXIS
		Vector2 topRightCorner = new Vector2 (1, 1);
		Vector2 edgeVector = Camera.main.ViewportToWorldPoint (topRightCorner);
		edgeVector *= 2;

		float width = edgeVector.x;

		for (int i = 0; i <= nbrOfSpawn; i++)
		{
			tmpPos.x = TopPos.position.x + ((width / (nbrOfSpawn + 1)) * (i + 1));
			SpawnerPositionList.Add (tmpPos);
		}

		for (int i = 0; i < SpawnerPositionList.Count; i++)

        StartCoroutine ("StartWaveManager");
	}

	// Update is called once per frame
	void Update ()
	{
	}
}
