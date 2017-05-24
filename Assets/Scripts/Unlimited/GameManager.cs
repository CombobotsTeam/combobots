using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
namespace Unlimited
{
public class GameManager : MonoBehaviour
{

    // Instance to turn GameManager into Singleton
    public static GameManager instance = null;

    public PawnPowerUp powerUp;
    public bool isBoss = false;
    public int Score = 0;//score of player
    public int Life = 1;//life of player
    public int ComboCount = 1;//player's combo count
    public int Gold = 0;

    // Will contain all the enemies on the screen (Not the enemies that will be instanciate)
    public List<GameObject> EnemiesOnScreen = new List<GameObject>();

    // Contain the current combination of button pressed
    public CombinationHandler Combination;
    [HideInInspector]
    public WaveManager WaveManager;
    public ComboManager cm;
    public int nbrOfSpawn = 5;
    List<Vector3> SpawnerPositionList;
    float NbrEnemiesOnScreen = 0;		// Actual number of enemies 
    private CombinationGenerator combinationGenerator = new CombinationGenerator();
    ConfigurationEnemy newconfigurationEnemy;
    float respowntime;
    float checktime;
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

        Combination = GetComponent<CombinationHandler>();
        WaveManager = GetComponent<WaveManager>();
        powerUp = GetComponent<PawnPowerUp>();
        cm = GetComponent<ComboManager>();

        SpawnerPositionList = new List<Vector3>();
        RectTransform Pos = GameObject.Find("Canvas").GetComponent<RectTransform>();
        RectTransform TopPos = GameObject.Find("Canvas/TopBackground").GetComponent<RectTransform>();

        Vector3 tmpPos = Pos.localPosition;

        // Y AXIS
        tmpPos.y = TopPos.position.y;

        // X AXIS
        Vector2 topRightCorner = new Vector2(1, 1);
        Vector2 edgeVector = Camera.main.ViewportToWorldPoint(topRightCorner);
        edgeVector *= 2;

        float width = edgeVector.x;
        for (int i = 0; i <= nbrOfSpawn; i++)
        {
            tmpPos.x = TopPos.position.x + ((width / (nbrOfSpawn + 1)) * (i + 1));
            SpawnerPositionList.Add(tmpPos);
        }

        respowntime = 0.0f;
        checktime = 0.0f;
    }

    void MakeEnemy()
    {
        string PathToEnemiesPrefab = "Prefabs/UnlimitedEnemies/";
        string type = "Robot01";
        GameObject BasicEnemy = Resources.Load<GameObject>(PathToEnemiesPrefab + type);

        if (!BasicEnemy)
            Debug.LogError("Impossible to instantiate " + type + " (Path: " + PathToEnemiesPrefab + type + ")");

        int randomIndex = Random.Range(0, SpawnerPositionList.Count - 1);


        RectTransform TopPos = GameObject.Find("Canvas/TopBackground").GetComponent<RectTransform>();

        Vector3 pos = SpawnerPositionList[randomIndex];
        pos.y = TopPos.position.y;
        float ysize = BasicEnemy.GetComponent<BoxCollider>().size.y;
        float ycenter = BasicEnemy.GetComponent<BoxCollider>().center.y;
        float yscale = BasicEnemy.GetComponent<Transform>().localScale.y;
        pos.y -= (ysize * (yscale / 2)) + (ycenter * yscale);


        BasicEnnemy enemy = Instantiate(BasicEnemy.gameObject, pos, Quaternion.identity).GetComponent<BasicEnnemy>();

        if (!enemy)
            Debug.LogError("[WaveManager]Can't instanciate the enemy !");


        combinationGenerator.FixedSize = enemy.CombinationSize;
        enemy.Combination = combinationGenerator.GetListButton();
        enemy.Setup();
        NbrEnemiesOnScreen++;
        GameManager.instance.EnemiesOnScreen.Add(enemy.gameObject);

    }

    void EndGame()//set about gameover
    {
    }

    void Update()
    {
        int i = 0;
        while (i < EnemiesOnScreen.Count)
        {
            if (EnemiesOnScreen[i].GetComponent<BasicEnnemy>().Died == true)
            {
                Destroy(EnemiesOnScreen[i], 1);
                EnemiesOnScreen.Remove(EnemiesOnScreen[i]);
            }
            else
                i++;
        }
        checktime += Time.deltaTime;
        if (respowntime < checktime)
        {
            MakeEnemy();
            checktime = 0.0f;
            respowntime = Random.Range(1.0f, 5.0f);
        }
        if (Life < 0)
        {
            EndGame();
        }
    
        cm.checkCombo();
    }

    public List<GameObject> GetEnemiesOnScreen()
    {
        return EnemiesOnScreen;
    }

    //add player's score that i want score(example player score is 0 and AddScore(10) => player score is 10)
    public void AddScore(int addscore)
    {
        Score += addscore * ComboCount;
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
        if (button != CombinationHandler.Button.POWER_UP)
        {
            Combination.AddButtonToCombination(button);
            powerUp.ChargePowerUp(5);
        }
        else
            powerUp.activate();
    }

    public void NotifyDie(GameObject enemy)
    {
        BasicEnnemy e = enemy.GetComponent<BasicEnnemy>();
        if (e.NbrGold > 0)
            Gold += e.NbrGold;
        e.Died = true;

        if (e == cm.lockEnemy)
            cm.lockEnemy = null;
    }
}
}
