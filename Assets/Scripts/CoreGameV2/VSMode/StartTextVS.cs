using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartTextVS : MonoBehaviour {

    private VSGameManager GM; //GameManerger script 
    private string Print = null;//print life text in gamescene string Later it change image ofr sprite 

    void Start()
    {
        GM = VSGameManager.instance;
    }

    void GetPlayersLife()//Get player's life function 
    {
        Print = GM.GetStartTime().ToString();
        GetComponent<Text>().text = Print;//change print text 
    }

    void Update()
    {
        GetPlayersLife();//Call function for get player's life
    }
}
