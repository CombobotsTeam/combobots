using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VSPlayerLife : MonoBehaviour {

    private VSGameManager GM; //GameManerger script 
    private string PrintLife = null;//print life text in gamescene string Later it change image ofr sprite 

    void Start()
    {
        GM = VSGameManager.instance;
    }

    void GetPlayersLife()//Get player's life function 
    {
        if (this.tag == "Player1")
            PrintLife = GM.GetLifePlayer1().ToString();//get player's life from gamemaneger And set
        if (this.tag == "Player2")
            PrintLife = GM.GetLifePlayer2().ToString();//get player's life from gamemaneger And set
        GetComponent<Text>().text = "Life = " + PrintLife;//change print text 
    }

    void Update()
    {
        GetPlayersLife();//Call function for get player's life
    }
}
