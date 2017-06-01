using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VSPlayerLife : MonoBehaviour {

    public bool delay;
    public ParticleSystem ParticleSystem;

    private VSGameManager GM; //GameManerger script 
    private string PrintLife = null;//print life text in gamescene string Later it change image ofr sprite
    private int prevLife;

    void Start()
    {
        GM = VSGameManager.instance;
        prevLife = GM.Life;
    }

    void GetPlayersLife()//Get player's life function 
    {
        if (this.tag == "Player1")
        {
            PrintLife = GM.GetLifePlayer1().ToString();//get player's life from gamemaneger And set
            if (GM.GetLifePlayer1() != prevLife)
            {
                DoParticle();
                prevLife = GM.GetLifePlayer1();
            }
        }
        if (this.tag == "Player2")
        {
            PrintLife = GM.GetLifePlayer2().ToString();//get player's life from gamemaneger And set
            if (GM.GetLifePlayer2() != prevLife)
            {
                DoParticle();
                prevLife = GM.GetLifePlayer2();
            }
        }
        GetComponent<Text>().text = PrintLife;//change print text
    }

    void Update()
    {
        GetPlayersLife();//Call function for get player's life
    }

    public void DoParticle()
    {
        ParticleSystem.Play();
    }
}
