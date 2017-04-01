using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnermySystem : MonoBehaviour {
	public int Arraynum = 0;//combination array's number
	public int ArrayMax = 4;
	public float EnermyDownSpeed = 1.0f;//enermy's down speed if it is 1.0f down each frame 1 pixel(maybe?)
	//public int[] combination = new int[ArrayMax];//make combination information by dynamic int array( i will make combostate by int)
	public Vector3 EnermyPosition;

	void Start () {
		EnermyPosition = GetComponent<Transform>().localPosition;
	}

	public void SetCombination(int combostate)
	{
		//combostate[Arraynum] = combostate;
		//Arraynum++;
	}

	void MoveEnemy()//Enemy move down
	{
		EnermyPosition.y -= EnermyDownSpeed;
		GetComponent<Transform> ().localPosition = new Vector3 (EnermyPosition.x, EnermyPosition.y ,EnermyPosition.z);
	}

	public void DestoryEnemy()//Destory this Enemy
	{
		Destroy (gameObject);
	}
    
	void Update () {
		//test destory
		if (Input.GetKey (KeyCode.S)) {//Destory Enemy Success
			DestoryEnemy ();
		}

		MoveEnemy ();
	}
}
