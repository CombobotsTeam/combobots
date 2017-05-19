using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnermySystem : MonoBehaviour {
	public int Arraynum = 0;//combination array's number
	public int ArrayMax = 4;
	public float EnermyDownSpeed = 1.0f;//enermy's down speed if it is 1.0f down each frame 1 pixel(maybe?)
	//public int[] combination = new int[ArrayMax];//make combination information by dynamic int array( i will make combostate by int)
	public Vector3 EnermyPosition;

	void Start () {
		EnermyPosition = GetComponent<RectTransform>().localPosition;
	}

	public void SetCombination(int combostate)
	{
		//combostate[Arraynum] = combostate;
		//Arraynum++;
	}

	void MoveEnemy()//Enemy move down
	{
		EnermyPosition.y -= EnermyDownSpeed;
		GetComponent<RectTransform> ().localPosition = new Vector3 (EnermyPosition.x, EnermyPosition.y ,EnermyPosition.z);
	}

	public void DestoryEnemy()//Destory this Enemy
	{

		Destroy (gameObject);
	}

	void DestroyEnemyByLine()//Enermy reach the attack line
	{
		if (EnermyPosition.y - GetComponent<RectTransform> ().rect.height/2 <= -100) {//reach red line
			Debug.Log ("GameOver");
			Destroy (gameObject);
			SceneManager.LoadScene (0);
			//Application.LoadLevel (0);
		}
	}

	void Update () {
		//test destory
		if (Input.GetKey (KeyCode.Q)) {//Destory Enemy Success
			DestoryEnemy ();
			Debug.Log ("Delete by script Get KeyCode Q");
		}

		MoveEnemy ();
		DestroyEnemyByLine();//Enermy reach the attack line
	}
}
