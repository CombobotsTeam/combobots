using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VSGameManager : MonoBehaviour {

    // Instance to turn GameManager into Singleton
    public static VSGameManager instance = null;
    public int Damage = 1;
    public int Life = 0;//life of player

    private int player_1_life;
    private int player_2_life;
    private string test;
    // Contain the current combination of button pressed
    VSCombinationHandler Combinations;

    CombiManager combination;
    //WaveManager WaveManager;

    private void Start()
    {
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
        combination = GetComponent<CombiManager>();
        player_1_life = Life;
        player_2_life = Life;
        combination.CreateCombination();
        //WaveManager = GetComponent<WaveManager>();
    }

    void Update()
    {
        if (Combinations.GetCurrentCombinationPlayer1().Count > 0)
        {
            bool PlayerHIt = false;
            PlayerHIt = Combinations.isSameCombinationPlayer1(combination.GetCombination());
            if (!Combinations.CompareCombinationPlayer1(combination.GetCombination()))
            {
                Combinations.ResetPlayer1();
            }
            if (PlayerHIt)
            {
                RemoveLife(Damage, 2);
                combination.CreateCombination();
                Combinations.ResetPlayer1();
                Combinations.ResetPlayer2();
            }
        }

        if (Combinations.GetCurrentCombinationPlayer2().Count > 0)
        {
            bool PlayerHIt = false;
            PlayerHIt = Combinations.isSameCombinationPlayer2(combination.GetCombination());

            if (!Combinations.CompareCombinationPlayer2(combination.GetCombination()))
            {
                Combinations.ResetPlayer2();
            }
            if (PlayerHIt)
            {
                RemoveLife(Damage, 1);
                combination.CreateCombination();
                Combinations.ResetPlayer1();
                Combinations.ResetPlayer2();
            }
        }


    }

    //player's Life down(example life 3 => 2)
    public void RemoveLife(int lifeToRemove, int player)
    {
        if (player == 1)
        {
            player_1_life -= lifeToRemove;
            if (player_1_life < 0) player_1_life = 0;
        }
        if (player == 2)
        {
            player_2_life -= lifeToRemove;
            if (player_2_life < 0) player_2_life = 0;
        }
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
