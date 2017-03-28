using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetPrintScore : MonoBehaviour
{
    private GameManager GM;//Get GameManeger from main camera's GameManeger script
    private string PrintScore = null;//print score text in gamescene string

    void Start()
    {
        GM = GameManager.instance;
        //Camera.main.GetComponent<GameManager> ();//Get GameManeger script from main camera's GameManeger script
    }

    void GetPlayerScore()//Get player's score function 
    {
        PrintScore = GM.Score.ToString();//get player's score from gamemaneger
        GetComponent<Text>().text = PrintScore;//change print text 
    }

    void Update()
    {
        GetPlayerScore();//Call function for get player's score
    }
}
