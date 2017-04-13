using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setprint_Arcade_Combo : MonoBehaviour {
	private ArcadeGM GM; //GameManerger script 
	private string PrintText = null;//print combo text in gamescene string

	void Start () {
		GM = Camera.main.GetComponent<ArcadeGM> ();//Get GameManeger script from main camera's GameManeger script
	}

	void GetPlayerCombo()//Get player's combo function 
	{
		PrintText = "Combo : " + GM.ComboCount.ToString ();//get player's combocount from gamemaneger And set 
		GetComponent<Text> ().text = PrintText;//change print text 
	}

	void Update () {
		GetPlayerCombo();//Call function for get player's combo
	}
}
