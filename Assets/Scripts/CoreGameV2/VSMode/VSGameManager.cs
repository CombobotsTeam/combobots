using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VSGameManager : MonoBehaviour {

    // Instance to turn GameManager into Singleton
    public static VSGameManager instance = null;

    public int Life = 0;//life of player

    private int player_1_life;
    private int player_2_life;
    private string test;
    // Contain the current combination of button pressed
    VSCombinationHandler Combinations;
    //WaveManager WaveManager;

    private void Start()
    {
        player_1_life = Life;
        player_2_life = Life;
    }

    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
            //if not, set instance to this
            instance = this;
        //If instance already exists and it's not this:
        else if (instance != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        Combinations = GetComponent<VSCombinationHandler>();
        //WaveManager = GetComponent<WaveManager>();
    }

    void Update()
    {
        //if (Combination_Player1.GetCurrentCombination().Count > 0)
        //{
        //    bool combinationExist = false;
        //    if (!combinationExist)
        //    {
        //        Combination_Player1.Reset();
        //    }
        //}

        //if (Combination_Player2.GetCurrentCombination().Count > 0)
        //{
        //    bool combinationExist = false;
        //    if (!combinationExist)
        //    {
        //        Combination_Player2.Reset();
        //    }
        //}
    }

    //player's Life down(example life 3 => 2)
    public void RemoveLife(int lifeToRemove, int player)
    {
        Life -= lifeToRemove;
        if (Life < 0) Life = 0;
    }

    // Get the type of button pressed and update the Combination Handler
    public void ButtonPressed(VSCombinationHandler.Button button, int player)
    {
        test = "";
        if (player == 1)
        {
            Combinations.AddButtonToCombinatioPlayer1(button);
            List<VSCombinationHandler.Button> player1 = Combinations.GetCurrentCombinationPlayer1();
            foreach(CombinationHandler.Button x in player1)
            {
                test = test + " " + x;
            }
            Debug.Log("Player 1 =" + test);
        }

        if (player == 2)
        {
            Combinations.AddButtonToCombinationPlayer2(button);
            List<VSCombinationHandler.Button> player2 = Combinations.GetCurrentCombinationPlayer2();
            foreach (CombinationHandler.Button x in player2)
            {
                test = test + " " + x;
            }
            Debug.Log("Player 2 =" + test);
        }
    }

    public int GetLifePlayer1()
    {
        return player_1_life;
    }

    public int GetLifePlayer2()
    {
        return player_2_life;
    }
}
