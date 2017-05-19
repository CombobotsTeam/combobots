using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetPrintLife : MonoBehaviour
{
    public GameObject[] currentObjectLife;

    private GameManager GM; //GameManerger script 
    private int life = 0;
    //private Text UIText;
    private Sprite Heart;
    private Sprite DeadHeart;


    void Start()
    {
        GM = GameManager.instance;
        //UIText = GetComponent<Text>();
        Heart = Resources.Load("Prefabs/Life/Heart") as Sprite;
        DeadHeart = Resources.Load("Prefabs/Life/DeadHeart") as Sprite;
    }

    void GetPlayerCombo()//Get player's life function 
    {
        if (GM.Life != life)
        {
            GM.Life = life;
            UpdateLifeDisplay(life); 
        }
    }

    void UpdateLifeDisplay(int Life)
    {
        for (int i = 0; i < currentObjectLife.Length; i++)
            currentObjectLife[i].GetComponent<SpriteRenderer>().sprite = (i > Life) ? DeadHeart : Heart;
    }

    void Update()
    {
        GetPlayerCombo();//Call function for get player's life
    }
}