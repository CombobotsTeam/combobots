using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

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
	private int ComboMult = 1;//score multiplication
    public int Gold = 0;
    bool launch = true;
    public bool immunity = false;
    protected bool AbilityEnabled = true;

    private GameObject HeartEmpty;
    private GameObject HeartFull;
	private GameObject Coin;
	private GameObject ComboGood;
	private GameObject ComboSuper;
	private GameObject ComboAmazing;
	private GameObject FloatingPoints;
	protected GameObject FloatingCoins;
	private Text CoinText;
	private Text ScoreText;
	private TextMeshPro FloatingPointsText;

    private Animator AbilityButton;
    private GameObject ParticleSystemAbility;
    private bool isPowerActivated = false;

    private Dictionary<int, GameObject> heartList;
    protected bool LateInit = true;

    // Will contain all the enemies on the screen (Not the enemies that will be instanciate)
    public List<GameObject> EnemiesOnScreen = new List<GameObject>();

    // Contain the current combination of button pressed
    public CombinationHandler Combination;
    [HideInInspector]
    public WaveManager WaveManager;
    public ComboManager cm;

    protected SoundManager soundManager;

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
        Init();
    }

    protected virtual void Init()
    {
        Combination = GetComponent<CombinationHandler>();
        WaveManager = GetComponent<WaveManager>();
        powerUp = GetComponent<IPowerUp>();
        cm = GetComponent<ComboManager>();
    }

    void Start()
    {
        soundManager = SoundManager.instance;
        EndMessage.enabled = false;
        GameObject tmp = GameObject.Find("AbilityButton");
        ParticleSystemAbility = tmp.transform.FindChild("ButtonEntity").FindChild("ReadyParticle").gameObject;
        AbilityButton = tmp.gameObject.GetComponentInChildren<Animator>();
        ParticleSystemAbility.gameObject.SetActive(false);
    }

    protected void checkDeath()
    {
        float time = 0f;
        int i = 0;
        while (i < EnemiesOnScreen.Count)
        {
            if (EnemiesOnScreen[i].GetComponent<BasicEnnemy>().Died == true)
            {
                time = EnemiesOnScreen[i].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
                if (EnemiesOnScreen[i].GetComponent<Animator>().GetBool("Attack"))
                    time += 1f;
                time -= 0.05f; 
                //Debug.Log("Time is " + time);
                Destroy(EnemiesOnScreen[i], time);
                EnemiesOnScreen.Remove(EnemiesOnScreen[i]);
            }
            else
                i++;
        }
    }

    protected virtual void Update()
    {
        checkDeath();
        if (isPaused)
            return;

        if (LateInit)
        {
            SetLife();
			SetCoin();
			SetScore();
			SetCombo();
			soundManager.PlayerMusic("MusicInGame");
            LateInit = false;
        }
        
        if (powerUp && AbilityEnabled && powerUp.Charge >= powerUp.ChargeMax)
            SetAbilityActive(true);

        if (launch)
        {
            WaveManager.launch();
            launch = false;
        }
        cm.checkCombo();
    }

	protected void SetCombo()
	{
		ComboGood = Resources.Load("Prefabs/Combo/ComboGood") as GameObject;
		ComboSuper = Resources.Load("Prefabs/Combo/ComboSuper") as GameObject;
		ComboAmazing = Resources.Load("Prefabs/Combo/ComboAmazing") as GameObject;
	}

    protected void SetScore()
	{
		FloatingPoints = Resources.Load("Prefabs/FloatingPoints") as GameObject;
		FloatingPointsText = FloatingPoints.GetComponent<TextMeshPro> ();

		ScoreText = GameObject.Find("Score").GetComponent<Text>();

		AddScore (0, new Vector3());
	}

    // Make the ability ready to be used or not (Visual effect)
    protected void SetAbilityActive(bool isActive)
    {
        if (isPowerActivated && isActive)
            return;

        ParticleSystemAbility.SetActive(isActive);
        if (isActive)
            AbilityButton.SetBool("Ready", true);
        else
            AbilityButton.SetBool("Ready", false);
        isPowerActivated = false;
;    }

    protected void SetCoin()
	{
		Coin = Resources.Load("Prefabs/Coins") as GameObject;
		FloatingCoins = Resources.Load("Prefabs/FloatingCoins") as GameObject;

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
	}

    protected void SetLife()
	{
		RectTransform toprect = GameObject.Find("TopBackground").GetComponent<RectTransform>();

		Vector3[] toprectEdges = new Vector3[4];
		toprect.GetWorldCorners (toprectEdges);

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
	public void AddScore(int addscore, Vector3 position)
    {
		// Display the floating number of point
		if (addscore != 0) {
			FloatingPointsText.text = "<color=#808080ff>+</color><color=#c0c0c0ff>" + (addscore * ComboMult).ToString() + "</color>";
			position.y += FloatingPointsText.rectTransform.rect.size.y / 2;
			Instantiate (FloatingPoints, position, Quaternion.identity);
		}

		// Update the score
		Score += addscore * ComboMult;

		// Display the total number of point of the player
		ScoreText.text = "";
		ScoreText.text += "<color=#808080ff>";

		for (int i = 10000; i > 1; i = i / 10) {
			if (i > Score)
				ScoreText.text += "0";
		}
			
		ScoreText.text += "</color><color=#c0c0c0ff>" + Score.ToString() + "</color><color=#808080ff> pts</color>";
    }

    //player's combocount up (example 1 => 2)
	public void AddComboPoint(int comboPoint, Vector3 position)
    {
        ComboCount += comboPoint;

		// Display the "GOOD !" combo message
		if (ComboCount == 3) {
			position.z = 0;
			ComboMult = 2;
			Instantiate (ComboGood, position, Quaternion.identity);
		}

		// Display the "SUPER !!" combo message
		if (ComboCount == 6) {
			position.z = 0;
			ComboMult = 3;
			Instantiate (ComboSuper, position, Quaternion.identity);
		}

		// Display the "AMAZING !!" combo message
		if (ComboCount == 9) {
			position.z = 0;
			ComboMult = 4;
			Instantiate (ComboAmazing, position, Quaternion.identity);
		}
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

        if (Life < heartList.Count)
        {
            Life = Math.Min(heartList.Count, Life + additionalLife);
            GameObject oldHeart = heartList[Life];
            GameObject heart = Instantiate(HeartFull, oldHeart.transform.position, Quaternion.identity);
            heart.transform.localScale = oldHeart.transform.localScale;
            heartList[Life] = heart;
            Destroy(oldHeart);
        }
    }

	//player's Life down(example life 3 => 2)
	public void RemoveLife(int lifeToRemove)
	{
        if (!immunity)
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
    }

    // Will add the amount of Gold
    public void AddGold(int amount)
    {
        Gold += amount;
		soundManager.Play ("CollectCoin", false);
		CoinText.text = Gold.ToString();
    }

    // Get the type of button pressed and update the Combination Handler
    public void ButtonPressed(CombinationHandler.Button button)
    {
		#if UNITY_EDITOR
        if (button != CombinationHandler.Button.POWER_UP)
        {
            Combination.AddButtonToCombination(button);
        }
        else
        {
			foreach (GameObject g in EnemiesOnScreen)
			{
				BasicEnnemy e = g.GetComponent<BasicEnnemy>();
				e.DecreaseLifePoint(1);
				if (e.Died)
					ComboCount += 1;
			}
			// You can change this if you don t like this behaviour or for testing abilities.
			Debug.Log("I changed the ability behaviour in Unity editor. You press the ability to kill every enemy (better for debugging). - Victor -");
        }
		#else
		if (button != CombinationHandler.Button.POWER_UP)
		{
			Combination.AddButtonToCombination(button);
		}
		else if (AbilityEnabled && powerUp)
		{
			powerUp.activate();
			SetAbilityActive(false);
		}
		#endif
    }

	public virtual void NotifyDie(GameObject enemy, bool killedByPlayer)
    {
        BasicEnnemy e = enemy.GetComponent<BasicEnnemy>();
		/*if (e.NbrGold > 0) {
			AddGold(e.NbrGold);
		}*/
        e.Died = true;

        soundManager.Play("DeathEnemy", false);
        if (e == cm.lockEnemy)
            cm.lockEnemy = null;

		// GOLD
		if (killedByPlayer)
		{
			Vector3 randomPos = new Vector3 ();
			GameObject c;
			float delay;

			for (int i = 1; i <= (e.NbrGold / 5); i++) {
				randomPos = UnityEngine.Random.insideUnitCircle * 7;
				c = Instantiate (FloatingCoins, enemy.transform.position + randomPos, Quaternion.identity);
				delay = (float)i / 10.0f;
				c.GetComponent<CoinAnimation> ().Play (delay);
			}
		}

        if (WaveManager)
            WaveManager.EnemyDie(enemy);
    }

    public void LoadVictory()
    {
        isPaused = true;
        EndMessage.text = "Victory !";
        EndMessage.enabled = true;
        GameObject scGO = GameObject.Find("SceneInfos");
        if (scGO)
        {
            ScenesInfos sc = scGO.GetComponent<ScenesInfos>();

            if (sc.actualLevel >= PersistantData.instance.data.Story)
                PersistantData.instance.data.Story = sc.actualLevel + 1;
        }
        StartCoroutine("GoMenu");
    }

    public void LoadDefeat()
    {
        isPaused = true;
        EndMessage.text = "Defeat...";
        EndMessage.enabled = true;
        StartCoroutine("GoMenu");
    }

    // Activate or desactivate the ability
    public void SetEnableAbility(bool state)
    {
        AbilityEnabled = state;
    }

    IEnumerator GoMenu()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("MainMenu");
    }
}
