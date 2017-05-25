using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ConfigurationEnemy
{

    public enum Type
    {
        BasicEnemy,
        Boss,
    }

    public string 	name = "Enemy01";
    public int 		CombinationSize = 3;
    public float 	Speed = 1.0f;
    public int 		Gold = 0;
    public float 	SpawnCoolDown = 2.0f;
    public int 		Life = 2;
    public Type 	t = Type.BasicEnemy;

    [HideInInspector]
    public GameObject prefab = null;
}

[System.Serializable]
public class ConfigurationWave
{
    public List<ConfigurationEnemy> Entities = new List<ConfigurationEnemy>();
    public float TimeBetweenWave = 3.0f;
    public string TextDisplay = "Be ready for the new wave !";
}

public class ConfigurationGame : MonoBehaviour
{
    // Instance to turn ConfigurationGame into Singleton
    public static ConfigurationGame instance = null;

    public string PathToEnemiesPrefab = "Prefabs/Enemies/";
    public List<ConfigurationWave> Waves = new List<ConfigurationWave>();

    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
            //if not, set instance to this
            instance = this;
        //If instance already exists and it's not this:
        else if (instance != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
    }

    public GameObject GenerateEnemy(string type = "Robot01")//, int CombinationSize = 3, float Speed = 1, int Gold = 0, float SpawnCoolDown = 1)
    {
        GameObject toInstantiate = Resources.Load<GameObject>(PathToEnemiesPrefab + type);

        if (!toInstantiate)
            Debug.LogError("Impossible to instantiate " + type + " (Path: " + PathToEnemiesPrefab + type + ")");

       /* BasicEnnemy component = toInstantiate.GetComponent<BasicEnnemy>();
        component.CombinationSize = CombinationSize;
        component.Speed = Speed;
        component.NbrGold = Gold;
        component.SpawnCooldown = SpawnCoolDown;*/

        return toInstantiate;
    }

    public List<BasicWaveClass> GenerateWaves()
    {
        List<BasicWaveClass> WaveResult = new List<BasicWaveClass>();

        for (int i = 0; i < Waves.Count; i++)
        {
            //GameObject Wave = new GameObject("Wave" + i);
            //Wave.AddComponent<BasicWave>();

            BasicWaveClass WaveComponent = new BasicWaveClass();
            WaveComponent.TextDisplay = Waves[i].TextDisplay;
            WaveComponent.TimeBetweenWave = Waves[i].TimeBetweenWave;

            GameObject entity = null;
            for (int j = 0; j < Waves[i].Entities.Count; j++)
            {
                entity = GenerateEnemy(Waves[i].Entities[j].name);
                   /* Waves[i].Entities[j].CombinationSize, 
                    Waves[i].Entities[j].Speed, 
                    Waves[i].Entities[j].Gold, 
                    Waves[i].Entities[j].SpawnCoolDown
                    );*/
                Waves[i].Entities[j].prefab = entity;
                WaveComponent.entities.Add(Waves[i].Entities[j]);
            }

            WaveResult.Add(WaveComponent);
        }

        return WaveResult;
    }
}
