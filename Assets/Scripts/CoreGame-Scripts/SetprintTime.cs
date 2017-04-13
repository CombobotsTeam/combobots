using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetprintTime : MonoBehaviour {
	private ArcadeGM GM; //GameManerger script 
	private string PrintTime = null;//print combo text in gamescene string

	void Start () {
		GM = Camera.main.GetComponent<ArcadeGM> ();//Get GameManeger script from main camera's GameManeger script
	}

	void GetPlayerTime()//Get player's combo function 
	{
		int Minute = (int)GM.Time / 60;
		int Second = (int)GM.Time % 60;
		PrintTime = Minute.ToString () + " : " + Second.ToString ();
		GetComponent<Text> ().text = PrintTime;//change print text 
	}

	void Update () {
		GetPlayerTime();//Call function for get player's combo
	}
}
