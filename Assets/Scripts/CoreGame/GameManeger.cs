using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManeger : MonoBehaviour {
	public int Score = 0;//score of player
	public int Life = 0;//life of player
	public int ComboCount = 0;//player's combo count 
	public GameObject EnermyObject;

	void Start () {
		
	}
	
	void Update () {
	
		if (Input.GetKey (KeyCode.D)) {
			MakeEnemy ();		}
	}

	public void MakeEnemy()//create new enermy
	{
		Instantiate (EnermyObject);// (EnermyObject, new Vector3(Random.Range(0,Screen.width), 480, 0), Quaternion.identity);
	}

	public void AddScore(int addscore)//add player's score that i want score(example player score is 0 and AddScore(10) => player score is 10)
	{
		this.Score += addscore;
	}

	public void ComboCountUP()//player's combocount up (example 0 => 1)
	{
		this.ComboCount++;
	}

	public void ComboCountInit()//set player's combocount zero(example 3 => 0)
	{//player fail the combo, make combo count zero
		this.ComboCount = 0;
	}
	public void LifeUp()//player's life up(example life 2 => 3)
	{
		this.Life++;
	}

	public void LifeDown()//player's Life down(example life 3 => 2)
	{
		this.Life--;
	}
}
