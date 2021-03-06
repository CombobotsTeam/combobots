﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//make by seonghwan
public class ButtonUI : MonoBehaviour {
	
	public bool bottom = false;
    public bool top = false;
    public bool middle = false;
    public int percentofsizeheight = 25;
    public int VerticalOffset = 0;
    public bool FullWidth = false;

	private Vector3 Position = new Vector3(0.0f, 0.0f, 0.0f);//value of setting position
    private ButtonScript button;

	void Start () {
        button = GetComponent<ButtonScript>();
        if (FullWidth)
            GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height * percentofsizeheight / 100);
        else
		    GetComponent<RectTransform> ().sizeDelta = new Vector2(Screen.width / 4, Screen.height * percentofsizeheight / 100);
        int VerticalOffsetPixel = Screen.height * VerticalOffset / 100;

        if (top)
        {
            if (FullWidth)
                Position.x = -Screen.width / 2.0f;
            else
                Position.x = Screen.width / 4 * (3 - (int)button.Type) - Screen.width /4 ;
            Position.y = Screen.height - Screen.height / 2;
        }

        if (bottom)
        {
            if (FullWidth)
                Position.x = -Screen.width / 2.0f;
            else
                Position.x = Screen.width / 4 * (int)button.Type - Screen.width / 2;
            Position.y = 0 - Screen.height / 2 + VerticalOffsetPixel;
        }

        if (middle)
        {
            if (FullWidth)
                Position.x = -Screen.width / 2.0f;
            else
                Position.x = -Screen.width / 8f;// - Screen.width / 2;
            Position.y = 0 -  Screen.height / 2 + VerticalOffsetPixel;
        }

        GetComponent<RectTransform> ().localPosition = Position; 
	}
}
