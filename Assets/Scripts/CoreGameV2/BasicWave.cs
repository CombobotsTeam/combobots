using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BasicWaveClass {

    [HideInInspector]
    public List<ConfigurationEnemy> entities = new List<ConfigurationEnemy>();

    public float TimeBetweenWave = 1.0f;
    public string TextDisplay = "Text To Configure";

    public void CopyFromOther(BasicWaveClass other)
    {
        entities = other.entities;
        TimeBetweenWave = other.TimeBetweenWave;
        TextDisplay = other.TextDisplay;
    }
}

public class BasicWave : MonoBehaviour
{
    [SerializeField]
    public BasicWaveClass data;

    public void Start()
    {
    }

    public void CopyFromOther(BasicWave other)
    {
        data.CopyFromOther(other.data);
    }
}
