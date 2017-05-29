using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManagerUnlimited : GameManager
{
    public int nbrOfSpawn = 5;
    List<Vector3> SpawnerPositionList;
    float NbrEnemiesOnScreen = 0;		// Actual number of enemies 
    private CombinationGenerator combinationGenerator = new CombinationGenerator();
    ConfigurationEnemy newconfigurationEnemy;
    float respowntime;
    float checktime;
    
    protected override void init()
    {
        base.init();
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

    protected override void Update()
    {
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
        checkDeath();
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
}
