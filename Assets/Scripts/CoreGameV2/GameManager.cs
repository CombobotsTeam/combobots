using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    // Instance to turn GameManager into Singleton
    public static GameManager instance = null;

    public bool isBoss = false;
    public int Score = 0;//score of player
	public int Life = 0;//life of player
	public int ComboCount = 1;//player's combo count
    public int Gold = 0;
    bool launch = true;
    GameObject lockEnnemy = null;

    // Will contain all the enemies on the screen (Not the enemies that will be instanciate)
    public List<GameObject> EnemiesOnScreen = new List<GameObject>();

    // Contain the current combination of button pressed
    CombinationHandler Combination;
    [HideInInspector]
    public WaveManager WaveManager;

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
            if (!isBoss)
            {
                if (lockEnnemy == null)
                         {
                             foreach (GameObject enemy in EnemiesOnScreen)
                             {
                                 if (lockEnnemy == null || lockEnnemy.transform.localPosition.y > enemy.transform.localPosition.y)
                                     lockEnnemy = enemy;
                             }
                         }
                         if (lockEnnemy != null)
                         {
                             BasicEnnemy e = lockEnnemy.GetComponent<BasicEnnemy>();
                             if (Combination.CompareCombination(e.Combination))
                             {
                                 e.FeedBackCombination(Combination);
                                 if (Combination.isSameCombination(e.Combination))
                                 {
                                     e.Die();
                                     Score += 10 * ComboCount;
                                     ComboCount++;
                                     if (e.NbrGold > 0)
                                         Gold += e.NbrGold;
                                     Combination.Reset();
                                 }
                             }
                             else
                             {
                                 e.FeedBackCombination(Combination, true);
                                 ResetComboPoint();
                                 Combination.Reset();
                             }
                         }
            }
            else
            {
                bool combinationExist = false;
                foreach (GameObject enemy in EnemiesOnScreen)
                {
                    BasicEnnemy e = enemy.GetComponent<BasicEnnemy>();
                    if (e.getCombination().Count == 0)
                        continue;
                    if (Combination.CompareCombination(e.Combination))
                    {
                        combinationExist = true;
                        e.FeedBackCombination(Combination);
                        if (Combination.isSameCombination(e.Combination))
                        {
                            e.Die();
                            Score += 10 * (ComboCount == 0 ? 1 : ComboCount);
                            ComboCount++;
                            if (e.NbrGold > 0)
                                Gold += e.NbrGold;
                            Combination.Reset();
                            break;
                        }
                    }
                    else
                    {
                        e.FeedBackCombination(Combination, true);
                    }
                }
                if (!combinationExist)
                {
                    ComboCount = 0;
                    Combination.Reset();
                }
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

    //player's combocount up (example 1 => 2)
    public void AddComboPoint(int comboPoint)
	{
        ComboCount += comboPoint;
	}

    //set player's combocount zero(example 3 => 1)
    //player fail the combo, make combo count zero
    public void ResetComboPoint()
	{
		ComboCount = 1;
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
        if (lockEnnemy == enemy)
            lockEnnemy = null;
        WaveManager.EnemyDie(enemy);
        EnemiesOnScreen.Remove(enemy);
    }
}
