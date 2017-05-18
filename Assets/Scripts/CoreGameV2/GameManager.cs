using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    // Instance to turn GameManager into Singleton
    public static GameManager instance = null;

    public bool isPaused = false;

    public IPowerUp powerUp;
    public bool isBoss = false;
    public int Score = 0;//score of player
    public int Life = 3;//life of player
    public int ComboCount = 1;//player's combo count
    public int Gold = 0;
    bool launch = true;

    private GameObject HeartEmpty;
    private GameObject HeartFull;

    private Dictionary<int, GameObject> heartList;
    bool LateInit = true;

    // Will contain all the enemies on the screen (Not the enemies that will be instanciate)
    public List<GameObject> EnemiesOnScreen = new List<GameObject>();

    // Contain the current combination of button pressed
    public CombinationHandler Combination;
    [HideInInspector]
    public WaveManager WaveManager;
    public ComboManager cm;

    private SoundManager soundManager;

    public Text EndMessage;

    public IEnumerator WaitFor(float time)
    {
        GameManager gm = GameManager.instance;
        while (time > 0)
        {
            if (!gm.isPaused)
                time -= Time.deltaTime;
            yield return null;
        }
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

        Combination = GetComponent<CombinationHandler>();
        WaveManager = GetComponent<WaveManager>();
        powerUp = GetComponent<IPowerUp>();
        cm = GetComponent<ComboManager>();
    }

    void Start()
    {
        soundManager = SoundManager.instance;
        soundManager.PlayerMusic("MusicInGame");
        EndMessage.enabled = false;
    }

    void Update()
    {
        if (isPaused)
            return;

        if (LateInit)
        {
            SetLife();
            LateInit = false;
        }

        int i = 0;
        while (i < EnemiesOnScreen.Count)
        {
            if (EnemiesOnScreen[i].GetComponent<BasicEnnemy>().Died == true)
            {
                Destroy(EnemiesOnScreen[i], 0.8f);
                EnemiesOnScreen.Remove(EnemiesOnScreen[i]);
            }
            else
                i++;
        }
        if (launch)
        {
            WaveManager.launch();
            launch = false;
        }
        cm.checkCombo();
        if (Input.GetKeyDown("space"))
            powerUp.activate();
    }

    void SetLife()
    {
        Debug.Log("setLife");
        RectTransform canvas = GameObject.Find("Canvas").GetComponent<RectTransform>();

        HeartFull = Resources.Load("Prefabs/Life/FullHeart") as GameObject;
        HeartEmpty = Resources.Load("Prefabs/Life/DeadHeart") as GameObject;

        Vector3 upperRightPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 1));
        float xheartSize = HeartFull.GetComponent<SpriteRenderer>().sprite.bounds.size.x * HeartFull.transform.localScale.x;
        float yheartSize = HeartFull.GetComponent<SpriteRenderer>().sprite.bounds.size.y * HeartFull.transform.localScale.y;

        heartList = new Dictionary<int, GameObject>();

        GameObject heart = Instantiate(HeartFull, new Vector3(upperRightPos.x - (xheartSize / 2), upperRightPos.y - (yheartSize / 2), 1), Quaternion.identity);
        heartList.Add(1, heart);
        heart = Instantiate(HeartFull, new Vector3(upperRightPos.x - (xheartSize / 2) - xheartSize, upperRightPos.y - (yheartSize / 2), 1), Quaternion.identity);
        heartList.Add(2, heart);
        heart = Instantiate(HeartFull, new Vector3(upperRightPos.x - (xheartSize / 2) - (xheartSize * 2), upperRightPos.y - (yheartSize / 2), 1), Quaternion.identity);
        heartList.Add(3, heart);
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
        GameObject oldHeart = heartList[Life];
        GameObject heart = Instantiate(HeartEmpty, oldHeart.transform.position, Quaternion.identity);
        heartList[Life] = heart;
        Destroy(oldHeart);

        Life -= lifeToRemove;

        if (Life < 0)
            Life = 0;
        if (Life == 0) LoadDefeat();
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

        soundManager.Play("DeathEnemy", false);
        if (e == cm.lockEnemy)
            cm.lockEnemy = null;
        WaveManager.EnemyDie(enemy);
    }

    public void LoadVictory()
    {
        isPaused = true;
        EndMessage.text = "Victory !";
        EndMessage.enabled = true;
        StartCoroutine("GoMenu");
    }

    public void LoadDefeat()
    {
        isPaused = true;
        EndMessage.text = "Defeat...";
        EndMessage.enabled = true;
        StartCoroutine("GoMenu");
    }

    IEnumerator GoMenu()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("MainMenu");
    }
}
