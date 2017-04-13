using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetprintMoney : MonoBehaviour {
	private ArcadeGM GM; //GameManerger script 
	private string PrintMoney = null;//print combo text in gamescene string

	void Start () {
		GM = Camera.main.GetComponent<ArcadeGM> ();//Get GameManeger script from main camera's GameManeger script
	}

	void GetPlayerMoeny()//Get player's combo function 
	{
		PrintMoney = "Money : " + GM.Money.ToString ();//get player's combocount from gamemaneger And set 
		GetComponent<Text> ().text = PrintMoney;//change print text 
	}

	void Update () {
		GetPlayerMoeny();//Call function for get player's combo
	}
}
