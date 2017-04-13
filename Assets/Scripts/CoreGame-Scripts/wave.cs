using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class wave : MonoBehaviour {
	private GameManeger GM; //GameManerger script 
	private string PrintCombo = null;//print combo text in gamescene string

	void Start () {
		GM = Camera.main.GetComponent<GameManeger> ();//Get GameManeger script from main camera's GameManeger script
	}

	void GetPlayerCombo()//Get player's combo function 
	{
		PrintCombo = "Wave : " + GM.Wave.ToString ();//get player's combocount from gamemaneger And set 
		GetComponent<Text> ().text = PrintCombo;//change print text 
	}

	void Update () {
		GetPlayerCombo();//Call function for get player's combo
	}
}
