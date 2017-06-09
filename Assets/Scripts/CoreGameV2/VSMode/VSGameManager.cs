using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VSGameManager : MonoBehaviour {

    // Instance to turn GameManager into Singleton
    public static VSGameManager instance = null;
    public int Damage = 1;
    public int Life = 0;//life of player
    public Canvas gameCanvas;
    public Canvas winCanvas;
    public Canvas delayCanvas;
    public ParticleSystem WrongCombo1;
    public ParticleSystem WrongCombo2;
    public List<GameObject> Backgrounds;
    public List<Sprite> Background_sprites;

    private SoundManager soundManager;
    private int player_1_life;
    private int player_2_life;
    private int winner = 0;
    private float startTime;
    private int round = 1;
    private Sprite background;
    // Contain the current combination of button pressed
    VSCombinationHandler Combinations;

    CombiManager combination;
    //WaveManager WaveManager;

    void Start()
    {
        soundManager = SoundManager.instance;
        soundManager.PlayerMusic("MusicInGame");

        gameCanvas.enabled = false;
        winCanvas.enabled = false;
        delayCanvas.enabled = true;

        background = Background_sprites[Random.Range(0, Background_sprites.Count - 1)];
        foreach (GameObject Background in Backgrounds)
        {
            Background.GetComponent<Image>().sprite = background;
        }

        combination.CreateCombination();
    }

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        startTime = Time.time;
        Combinations = GetComponent<VSCombinationHandler>();
        combination = GetComponent<CombiManager>();
        player_1_life = Life;
        player_2_life = Life;
    }

    void Update()
    {
        if (Time.time - startTime > 3.5F)
        {
            delayCanvas.enabled = false;
            gameCanvas.enabled = true;
        }
        if (Combinations.GetCurrentCombinationPlayer(1).Count > 0)
        {
            bool PlayerHIt = false;
            PlayerHIt = Combinations.IsSameCombinationPlayer(combination.GetCombination(),1);
            if (!Combinations.CompareCombinationPlayer(combination.GetCombination(),1))
            {
                DoParticle(WrongCombo1);
                Combinations.ResetPlayer(1);
                soundManager.Play("WrongCombo", false);
            }
            if (PlayerHIt)
            {
                PlayerHit(2);
            }
        }

        if (Combinations.GetCurrentCombinationPlayer(2).Count > 0)
        {
            bool PlayerHIt = false;
            PlayerHIt = Combinations.IsSameCombinationPlayer(combination.GetCombination(),2);

            if (!Combinations.CompareCombinationPlayer(combination.GetCombination(),2))
            {
                DoParticle(WrongCombo2);
                Combinations.ResetPlayer(2);
                soundManager.Play("WrongCombo", false);
            }
            if (PlayerHIt)
            {
                PlayerHit(1);
            }
        }

        if (winner != 0)
        {
            winCanvas.enabled = true;
            gameCanvas.enabled = false;
            StartCoroutine("GoMenu");
        }

    }

    private void PlayerHit(int player)
    {
        soundManager.Play("RightCombo", false);
        RemoveLife(Damage, player);
        combination.CreateCombination();
        Combinations.ResetPlayer(1);
        Combinations.ResetPlayer(2);
        Delay();
        round++;
    }

    private void Delay()
    {
        startTime = Time.time;
        delayCanvas.enabled = true;
        gameCanvas.enabled = false;
    }

    //player's Life down(example life 3 => 2)
    public void RemoveLife(int lifeToRemove, int player)
    {
        if (player == 1)
        {
            player_1_life -= lifeToRemove;
            if (player_1_life <= 0)
            {
                player_1_life = 0;
                winner = 2;
            }
        }
        if (player == 2)
        {
            player_2_life -= lifeToRemove;
            if (player_2_life <= 0)
            {
                player_2_life = 0;
                winner = 1;
            }
        }
    }

    // Get the type of button pressed and update the Combination Handler
    public void ButtonPressed(VSCombinationHandler.Button button, int player)
    {
        if (player == 1)
        {
            Combinations.AddButtonToCombinatioPlayer(button,1);
        }

        if (player == 2)
        {
            Combinations.AddButtonToCombinatioPlayer(button,2);
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

    public float GetStartTime()
    {
        return Time.time - startTime;
    }

    IEnumerator GoMenu()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("MainMenu");
    }

    public void DoParticle(ParticleSystem particleSystem)
    {
        particleSystem.Play();
    }
}
