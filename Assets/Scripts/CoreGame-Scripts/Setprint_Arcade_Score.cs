using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setprint_Arcade_Score : MonoBehaviour {
	private ArcadeGM GM; //GameManerger script 
	private string PrintText = null;//print combo text in gamescene string

	void Start () {
		GM = Camera.main.GetComponent<ArcadeGM> ();//Get GameManeger script from main camera's GameManeger script
	}

	void GetPlayerScore()//Get player's combo function 
	{
		PrintText =  GM.Score.ToString ();//get player's combocount from gamemaneger And set 
		GetComponent<Text> ().text = PrintText;//change print text 
	}

	void Update () {
		GetPlayerScore();//Call function for get player's combo
	}
}
