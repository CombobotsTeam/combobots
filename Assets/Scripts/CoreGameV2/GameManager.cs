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
	private GameObject Coin;
	private Text CoinText;
	private Text ScoreText;

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
			SetCoin();
			SetScore();
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

	void SetScore()
	{
		ScoreText = GameObject.Find("Score").GetComponent<Text>();

		AddScore (0);
	}

	void SetCoin()
	{
		Coin = Resources.Load("Prefabs/Coins") as GameObject;

		RectTransform toprect = GameObject.Find("TopBackground").GetComponent<RectTransform>();

		Vector3[] toprectEdges = new Vector3[4];
		toprect.GetWorldCorners (toprectEdges);

		float toprectWidth = toprectEdges [2].x - toprectEdges [0].x;
		float toprectHeight = toprectEdges [1].y - toprectEdges [0].y;

		float marginWidth = 2 * (toprectWidth / 100);
		float marginHeight = 5.5f * (toprectHeight / 100);

		// SCALE
		float actualScale = Coin.transform.localScale.y;
		float actualLenght = Coin.GetComponent<SpriteRenderer>().sprite.bounds.size.y * Coin.transform.localScale.y;

		float expectedLenght = (toprectHeight / 2) - (marginHeight * 1.5f); // 8
		float expectedScale = (actualScale * expectedLenght) / actualLenght;

		Vector3 coinScale = new Vector3 (expectedScale, expectedScale, 1);

		float xcoinSize = Coin.GetComponent<SpriteRenderer> ().sprite.bounds.size.x * expectedScale;
		float ycoinSize = Coin.GetComponent<SpriteRenderer> ().sprite.bounds.size.y * expectedScale;

		GameObject coins;
		coins = Instantiate (Coin, new Vector3 (toprectEdges[2].x - (xcoinSize / 2) - marginWidth, toprectEdges[2].y - (ycoinSize / 2) - marginHeight, 1), Quaternion.identity);
		coins.transform.localScale = coinScale;

		///////////////

		CoinText = GameObject.Find("Gold").GetComponent<Text>();

		CoinText.text = Gold.ToString();

		/*Vector3 cointextPosition = new Vector3(1, 1, 1);

		cointextPosition.x = toprectEdges[2].x - (xcoinSize) - (marginWidth * 2);
		cointextPosition.y = toprectEdges[0].y - marginHeight;

		Debug.Log (cointextPosition.x);

		CoinText.rectTransform.localPosition = cointextPosition;*/
	}

	void SetLife()
	{
		RectTransform canvas = GameObject.Find("Canvas").GetComponent<RectTransform>();
		RectTransform toprect = GameObject.Find("TopBackground").GetComponent<RectTransform>();

		Vector3[] toprectEdges = new Vector3[4];
		toprect.GetWorldCorners (toprectEdges);

		Vector3 toprectMiddle = new Vector3 (toprectEdges [0].x + ((toprectEdges [2].x - toprectEdges [0].x) / 2), toprectEdges [0].y + ((toprectEdges [1].y - toprectEdges [0].y) / 2),0);

		float toprectWidth = toprectEdges [2].x - toprectEdges [0].x;
		float toprectHeight = toprectEdges [1].y - toprectEdges [0].y;

		float marginWidth = 2 * (toprectWidth / 100);
		float marginHeight = 5 * (toprectHeight / 100);

		HeartFull = Resources.Load("Prefabs/Life/FullHeart") as GameObject;
		HeartEmpty = Resources.Load("Prefabs/Life/DeadHeart") as GameObject;

		// SCALE
		float actualScale = HeartFull.transform.localScale.y; // 20
		float actualLenght = HeartFull.GetComponent<SpriteRenderer>().sprite.bounds.size.y * HeartFull.transform.localScale.y; // 12

		float expectedLenght = (toprectHeight / 2) - (marginHeight * 1.5f); // 8
		float expectedScale = (actualScale * expectedLenght) / actualLenght;

		Vector3 heartScale = new Vector3 (expectedScale, expectedScale, 1);

		float xheartSize = HeartFull.GetComponent<SpriteRenderer> ().sprite.bounds.size.x * expectedScale;
		float yheartSize = HeartFull.GetComponent<SpriteRenderer>().sprite.bounds.size.y * expectedScale;

		heartList = new Dictionary<int, GameObject>();
		GameObject heart;

		for (int i = 0; i < 3; i++)
		{
			heart = Instantiate (HeartFull, new Vector3 (toprectEdges[0].x + (xheartSize / 2) + (xheartSize * i) + marginWidth, toprectEdges[2].y - (yheartSize / 2) - marginHeight, 1), Quaternion.identity);
			heart.transform.localScale = heartScale;
			heartList.Add (i + 1, heart);
		}
	}

    public List<GameObject> GetEnemiesOnScreen()
    {
        return EnemiesOnScreen;
    }

    //add player's score that i want score(example player score is 0 and AddScore(10) => player score is 10)
    public void AddScore(int addscore)
    {
		Score += addscore * ComboCount;
		ScoreText.text = "";

		ScoreText.text += "<color=#808080ff>";

		for (int i = 10000; i > 1; i = i / 10) {
			if (i > Score)
				ScoreText.text += "0";
		}


		ScoreText.text += "</color><color=#c0c0c0ff>" + Score.ToString() + "</color><color=#808080ff> pts</color>";
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
		heart.transform.localScale = oldHeart.transform.localScale;
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
		CoinText.text = Gold.ToString();
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
