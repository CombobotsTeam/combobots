﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetPrintCombo : MonoBehaviour
{
    private GameManager GM; //GameManerger script 
    private string PrintCombo = null;//print combo text in gamescene string

    void Start()
    {
        GM = GameManager.instance;//Get GameManeger script from main camera's GameManeger script
    }

    void GetPlayerCombo()//Get player's combo function 
    {
        PrintCombo = "ComboX" + GM.ComboCount.ToString();//get player's combocount from gamemaneger And set 
        GetComponent<Text>().text = PrintCombo;//change print text 
    }

    void Update()
    {
        GetPlayerCombo();//Call function for get player's combo
    }
}