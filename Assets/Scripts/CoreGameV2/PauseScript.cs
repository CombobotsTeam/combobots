using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour {

    public Sprite imgPause;
    public Sprite imgPlay;

    Image img; 
    bool pause = false;
    GameManager gm;
    float timeScale;

	// Use this for initialization
	void Start () {
        img = GetComponent<Image>();
        gm = GameManager.instance;
        timeScale = Time.timeScale;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TogglePause()
    {
        pause = !pause;

        if (pause)
        {
            Time.timeScale = 0;
            img.sprite = imgPlay;
        } else
        {
            Time.timeScale = timeScale;
            img.sprite = imgPause;
        }

        gm.isPaused = pause;
    }
}
