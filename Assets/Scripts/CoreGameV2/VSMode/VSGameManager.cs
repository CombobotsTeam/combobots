using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VSGameManager : MonoBehaviour {

    // Instance to turn GameManager into Singleton
    public static VSGameManager instance = null;
    public int Damage = 1;
    public int Life = 0;//life of player
    public Canvas gameCanvas;
    public Canvas winCanvas;
    public Canvas startCanvas;

    private int player_1_life;
    private int player_2_life;
    private string test;
    private int winner = 0;
    private bool start = false;
    private float startTime;
    // Contain the current combination of button pressed
    VSCombinationHandler Combinations;

    CombiManager combination;
    //WaveManager WaveManager;

    private void Start()
    {
    }

    void Awake()
    {
        startTime = Time.time;
        startCanvas.enabled = true;
        gameCanvas.enabled = false;
        winCanvas.enabled = false;
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
        if (!start)
        {
            if (Time.time - startTime > 5F)
            {
                start = true;
                startCanvas.enabled = false;
                gameCanvas.enabled = true;
            }
            return;
        }
        if (Combinations.GetCurrentCombinationPlayer(1).Count > 0)
        {
            bool PlayerHIt = false;
            PlayerHIt = Combinations.IsSameCombinationPlayer(combination.GetCombination(),1);
            if (!Combinations.CompareCombinationPlayer(combination.GetCombination(),1))
            {
                Combinations.ResetPlayer(1);
            }
            if (PlayerHIt)
            {
                RemoveLife(Damage, 2);
                combination.CreateCombination();
                Combinations.ResetPlayer(1);
                Combinations.ResetPlayer(2);
            }
        }

        if (Combinations.GetCurrentCombinationPlayer(2).Count > 0)
        {
            bool PlayerHIt = false;
            PlayerHIt = Combinations.IsSameCombinationPlayer(combination.GetCombination(),2);

            if (!Combinations.CompareCombinationPlayer(combination.GetCombination(),2))
            {
                Combinations.ResetPlayer(2);
            }
            if (PlayerHIt)
            {
                RemoveLife(Damage, 1);
                combination.CreateCombination();
                Combinations.ResetPlayer(1);
                Combinations.ResetPlayer(2);
            }
        }

        if (winner != 0)
        {
            winCanvas.enabled = true;
            gameCanvas.enabled = false;
        }

    }

    //player's Life down(example life 3 => 2)
    public void RemoveLife(int lifeToRemove, int player)
    {
        if (player == 1)
        {
            player_1_life -= lifeToRemove;
            if (player_1_life < 0)
            {
                player_1_life = 0;
                winner = 2;
            }
        }
        if (player == 2)
        {
            player_2_life -= lifeToRemove;
            if (player_2_life < 0)
            {
                player_2_life = 0;
                winner = 1;
            }
        }
    }

    // Get the type of button pressed and update the Combination Handler
    public void ButtonPressed(VSCombinationHandler.Button button, int player)
    {
        test = "";
        if (player == 1)
        {
            Combinations.AddButtonToCombinatioPlayer(button,1);
            List<VSCombinationHandler.Button> player1 = Combinations.GetCurrentCombinationPlayer(1);
            foreach(CombinationHandler.Button x in player1)
            {
                test = test + " " + x;
            }
            Debug.Log("Player 1 =" + test);
        }

        if (player == 2)
        {
            Combinations.AddButtonToCombinatioPlayer(button,2);
            List<VSCombinationHandler.Button> player2 = Combinations.GetCurrentCombinationPlayer(2);
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

    public int GetWinner()
    {
        return winner;
    }

    public int GetStartTime()
    {
        return (6 - (int)(Time.time - startTime))/2;
    }
}
