using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class PersistantData : MonoBehaviour {

    public static PersistantData instance;
    public GameData data = new GameData();

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else if (instance != null)
            Destroy(this);

        if (!Application.isEditor)
        {
            Load();
        }
    }

    public void Save()
    {
        BinaryFormatter bd = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/game.dat");

        bd.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/game.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/game.dat", FileMode.Open);
            data = (GameData)bf.Deserialize(file);
            file.Close();
        } else
        {
            Debug.Log("GAME SAV not found. Starting from 0");
        } 
    }

    public void DestroySave()
    {
        BinaryFormatter bd = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/game.dat");

        bd.Serialize(file, new GameData());
        file.Close();
    }
}

[Serializable]
public class GameData
{
    public float SoundVolume = 0f;
    public int Gold = 0;
    public int[] CurrentPowerUp = new int[6] {0, 0, 0, 0, 0, 0 };
    public int Story = 0;
    public PowerUp PowerUpUsing = PowerUp.NONE;

    public enum PowerUp {
        PAWN = 0,
        BISHOP,
        KNIGHT,
        ROOK,
        KING,
        QUEEN,
        NONE
    }
}
