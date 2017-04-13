using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    // Instance to turn GameManager into Singleton
    public static GameManager instance = null;

    public int Score = 0;//score of player
	public int Life = 0;//life of player
	public int ComboCount = 0;//player's combo count
    public int Gold = 0;
    bool launch = true;

    // Will contain all the enemies on the screen (Not the enemies that will be instanciate)
    public List<GameObject> EnemiesOnScreen = new List<GameObject>();

    // Contain the current combination of button pressed
    CombinationHandler Combination;
	WaveManager WaveManager;

    private void Start()
    {
    }

    void Awake () {
        //Check if instance already exists
        if (instance == null)
            //if not, set instance to this
            instance = this;
        //If instance already exists and it's not this:
        else if (instance != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        Combination = GetComponent<CombinationHandler>();
		WaveManager = GetComponent<WaveManager>();
    }

	void Update ()
    {
        if (launch)
        {
            WaveManager.launch();
            launch = false;
        }
        if (Combination.GetCurrentCombination().Count > 0)
        {
            bool combinationExist = false;
            foreach (GameObject enemy in EnemiesOnScreen)
            {
                BasicEnnemy e = enemy.GetComponent<BasicEnnemy>();
                if (Combination.CompareCombination(e.Combination))
                {
                    combinationExist = true;
                    if (Combination.isSameCombination(e.Combination))
                    {
                        e.Die();
                        Combination.Reset();
                        break;
                    }
                }
            }
            if (!combinationExist)
            {
                Combination.Reset();
            }
        }
    }

    public List<GameObject> GetEnemiesOnScreen()
    {
        return EnemiesOnScreen;
    }

    //add player's score that i want score(example player score is 0 and AddScore(10) => player score is 10)
    public void AddScore(int addscore)
	{
		Score += addscore;
	}

    //player's combocount up (example 0 => 1)
    public void AddComboPoint(int comboPoint)
	{
        ComboCount += comboPoint;
	}

    //set player's combocount zero(example 3 => 0)
    //player fail the combo, make combo count zero
    public void ResetComboPoint()
	{
		ComboCount = 0;
	}

    //player's life up(example life 2 => 3)
    public void AddLife(int additionalLife)
	{
        Life += additionalLife;
	}

    //player's Life down(example life 3 => 2)
    public void RemoveLife(int lifeToRemove)
	{
        Life -= lifeToRemove;
        if (Life < 0) Life = 0;
	}

    // Will add the amount of Gold
    public void AddGold(int amount)
    {
        Gold += amount;
    }

    // Get the type of button pressed and update the Combination Handler
    public void ButtonPressed(CombinationHandler.Button button)
    {
        Combination.AddButtonToCombination(button);
    }

    public void NotifyDie(GameObject enemy)
    {
        WaveManager.EnemyDie(enemy);
        EnemiesOnScreen.Remove(enemy);
    }
}
