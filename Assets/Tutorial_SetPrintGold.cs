using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial_SetPrintGold : MonoBehaviour
{
    private Tutorial_GameManager GM; //GameManerger script 
    private string PrintGold = null;//print life text in gamescene string Later it change image ofr sprite 

    void Start()
    {
        GM = Tutorial_GameManager.instance;
    }

    void GetPlayerCombo()//Get player's life function 
    {
        PrintGold = "Gold : " + GM.Gold.ToString();//get player's Gold from gamemaneger And set 
        GetComponent<Text>().text = PrintGold;//change print text 
    }

    void Update()
    {
        GetPlayerCombo();//Call function for get player's life
    }
}