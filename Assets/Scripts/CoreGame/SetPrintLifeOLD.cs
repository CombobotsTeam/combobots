using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetPrintLifeOLD : MonoBehaviour {
	private GameManeger GM; //GameManerger script 
	private string PrintLife = null;//print life text in gamescene string Later it change image ofr sprite 

	void Start () {
		GM = Camera.main.GetComponent<GameManeger> ();//Get GameManeger script from main camera's GameManeger script
	}

	void GetPlayerCombo()//Get player's life function 
	{
		PrintLife = GM.Life.ToString ();//get player's life from gamemaneger And set 
		GetComponent<Text> ().text = PrintLife;//change print text 
	}

	void Update () {
		GetPlayerCombo();//Call function for get player's life
			}
}
