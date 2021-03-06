﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombiManager : MonoBehaviour {
    public int Size = 5;

    private List<VSCombinationHandler.Button> CurrentCombination = new List<VSCombinationHandler.Button>();


    public Sprite green;
    public Sprite blue;
    public Sprite red;
    public Sprite yellow;

    private SoundManager soundManager;

    // Use this for initialization
    void Start () {
        soundManager = SoundManager.instance;
        soundManager.PlayerMusic("MusicInGame");
    }
	
    public void CreateCombination()
    {
        CurrentCombination.Clear();
        int color = 0;
        for (int i = 0; i < Size; i++)
        {
            color = Random.Range(0, 4);
            if (color == 0)
                CurrentCombination.Add(VSCombinationHandler.Button.RED);
            if (color == 1)
                CurrentCombination.Add(VSCombinationHandler.Button.GREEN);
            if (color == 2)
                CurrentCombination.Add(VSCombinationHandler.Button.BLUE);
            if (color == 3)
                CurrentCombination.Add(VSCombinationHandler.Button.YELLOW);
        }
    }

    public List<VSCombinationHandler.Button> GetCombination()
    {
        return CurrentCombination;
    }
	// Update is called once per frame
	void Update () {
		
	}
}
