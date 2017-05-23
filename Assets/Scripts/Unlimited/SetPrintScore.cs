using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Unlimited
{
    public class SetPrintScore : MonoBehaviour
    {
        private GameManager GM;//Get GameManeger from main camera's GameManeger script
        private string PrintScore = null;//print score text in gamescene string
        private Text UIText;

        void Start()
        {
            GM = GameManager.instance;
            UIText = GetComponent<Text>();
        }

        void GetPlayerScore()//Get player's score function 
        {
            if (PrintScore != GM.Score.ToString())
            {
                PrintScore = GM.Score.ToString();//get player's score from gamemaneger
                UIText.text = PrintScore;//change print text 
            }
        }

        void Update()
        {
            GetPlayerScore();//Call function for get player's score
        }
    }
}