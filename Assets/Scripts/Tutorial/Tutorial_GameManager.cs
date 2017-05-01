using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Tutorial_GameManager : MonoBehaviour {

    // Instance to turn GameManager into Singleton
    public static Tutorial_GameManager instance = null;

    public int Score = 0;//score of player
	public int Life = 0;//life of player
	public int ComboCount = 0;//player's combo count
    public int Gold = 0;
    bool launch = true;
    public bool Pause = true;
    public int Pause_count = 0;
    public int killEnemy_count = 0;
    // Will contain all the enemies on the screen (Not the enemies that will be instanciate)
    public List<GameObject> EnemiesOnScreen = new List<GameObject>();
    public bool IsScore = false;
    public bool IsGold = false;
    public bool ISCombo = false;
    // Contain the current combination of button pressed
    CombinationHandler Combination;
    Tutorial_WaveManager WaveManager;
    Tutorial_Ennemy lockEnemy;

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
        Pause = true;
        Combination = GetComponent<CombinationHandler>();
		WaveManager = GetComponent<Tutorial_WaveManager>();
    }

	void Update ()
    {
        //if(killEnemy_count == 3)
        //{
        //    Pause_count = 2;
        //}
        if (launch)
        {
            WaveManager.launch();
            launch = false;
        }
        /*if (Combination.GetCurrentCombination().Count > 0)
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
        }*/ ///KEEP IT FOR THE BOSS
            if (Combination.GetCurrentCombination().Count > 0 && EnemiesOnScreen.Count > 0)
        {
            Tutorial_Ennemy enemy;
            if (lockEnemy)
            {
                enemy = lockEnemy;
            }
            else
            {
                GameObject firstEnemy = EnemiesOnScreen[0];
                foreach (GameObject e in EnemiesOnScreen)
                {
                    if (e.GetComponent<Transform>().position.y < firstEnemy.GetComponent<Transform>().position.y)
                        firstEnemy = e;
                }
                enemy = firstEnemy.GetComponent<Tutorial_Ennemy>();
            }
                if (Combination.CompareCombination(enemy.Combination))
                {
                    lockEnemy = enemy;
                    if (Combination.isSameCombination(enemy.Combination))
                    {
                        enemy.Die();
                    }
                }
                else
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
