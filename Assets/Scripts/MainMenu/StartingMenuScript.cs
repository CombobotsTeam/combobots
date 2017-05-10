using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingMenuScript : MonoBehaviour {
    private SoundManager soundManager;
    private bool Started = false;

    void StartMusic()
    {
        soundManager = SoundManager.instance;
        soundManager.PlayerMusic("MusicMenu");
        Started = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (!Started)
            StartMusic();
	}
}
