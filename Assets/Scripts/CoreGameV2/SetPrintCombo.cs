using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetPrintCombo : MonoBehaviour
{
    private GameManager GM; //GameManerger script 
    private string PrintCombo = null;//print combo text in gamescene string
    private Text UIText;

    void Start()
    {
        GM = GameManager.instance;//Get GameManeger script from main camera's GameManeger script
        UIText = GetComponent<Text>();
    }

    void GetPlayerCombo()//Get player's combo function 
    {
        if (PrintCombo != "ComboX" + GM.ComboCount.ToString())
        {
            PrintCombo = "ComboX" + GM.ComboCount.ToString();//get player's combocount from gamemaneger And set 
            UIText.text = PrintCombo;//change print text 
        }
    }

    void Update()
    {
        GetPlayerCombo();//Call function for get player's combo
    }
}