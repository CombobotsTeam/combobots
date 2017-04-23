using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetPrintLife : MonoBehaviour
{
    private GameManager GM; //GameManerger script 
    private string PrintLife = null;//print life text in gamescene string Later it change image ofr sprite 
    private Text UIText;

    void Start()
    {
        GM = GameManager.instance;
        UIText = GetComponent<Text>();
    }

    void GetPlayerCombo()//Get player's life function 
    {
        if (GM.Life.ToString() != PrintLife)
        {
            PrintLife = GM.Life.ToString();//get player's life from gamemaneger And set
            UIText.text = PrintLife;//change print text 
        }
    }

    void Update()
    {
        GetPlayerCombo();//Call function for get player's life
    }
}