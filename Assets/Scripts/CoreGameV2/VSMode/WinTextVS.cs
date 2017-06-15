using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinTextVS : MonoBehaviour {

    private VSGameManager GM; //GameManerger script 
    private string Print = null;//print life text in gamescene string Later it change image ofr sprite 

    void Start()
    {
        GM = VSGameManager.instance;
    }

    void GetPlayersLife()//Get player's life function 
    {
        if (GM.GetWinner() == 1)
        {
            if (this.tag == "Player1")
                GetComponent<Text>().text = "YOU WIN";//change print text
            else
                GetComponent<Text>().text = "YOU LOSE";//change print text
        }
        else
        {
            if (this.tag == "Player2")
                GetComponent<Text>().text = "YOU WIN";//change print text
            else
                GetComponent<Text>().text = "YOU LOSE";//change print text
        }

    }

    void Update()
    {
        GetPlayersLife();//Call function for get player's life
    }
}
