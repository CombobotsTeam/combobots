using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetPrintLife : MonoBehaviour
{
    private GameManager GM; //GameManerger script 
    private string PrintLife = null;//print life text in gamescene string Later it change image ofr sprite 

    void Start()
    {
        GM = GameManager.instance;
    }

    void GetPlayerCombo()//Get player's life function 
    {
        PrintLife = GM.Life.ToString();//get player's life from gamemaneger And set 
        GetComponent<Text>().text = PrintLife;//change print text 
    }

    void Update()
    {
        GetPlayerCombo();//Call function for get player's life
    }
}