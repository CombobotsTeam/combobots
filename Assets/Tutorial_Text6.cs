﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial_Text6 : MonoBehaviour {
    public string Text1 = "This is Enemy. That will attack you If you want to kill it. Click the same color button";
    private Tutorial_GameManager GM;//Get GameManeger from main camera's GameManeger script
    //private string PrintText = null;//print score text in gamescene string
    public Vector3 Position = Vector3.zero;
    void Start () {
        GM = Tutorial_GameManager.instance;
        GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/6, Screen.height/4);
    }
	void SetText()
    {
        switch(GM.Pause_count)
        {
            case 0:
                GetComponent<Text>().text = "This is Enemy. That will attack you If you want to kill it. Click the same color button";
                break;
            case 1:
                GetComponent<Text>().text = "Now Kill of all Enemy";
                    break;
            case 2:
                GetComponent<Text>().text = "Good job! Finish";
                break;
        }
        GetComponent<RectTransform>().localPosition = Position;
    }
	void Update () {
        SetText();
    }
}
