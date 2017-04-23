using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnnemy : MonoBehaviour {


    //public GameObject[] buttonRef;

    [HideInInspector]
    public List<CombinationHandler.Button> Combination = new List<CombinationHandler.Button>();
    public int CombinationSize = 3;
    public int Life = 3;
    public float Speed = 0.1f;
    public bool IsMoving = true;
    public int NbrGold = 0;
    [HideInInspector]
    public Vector3 Position;
    public float SpawnCooldown = 1000.0f;

    GameManager Gm;
    private Dictionary<CombinationHandler.Button, GameObject> ObjectToInstantiate = new Dictionary<CombinationHandler.Button, GameObject>();
    private BoxCollider boxCollider;
    private List<GameObject> ButtonsEnemy = new List<GameObject>();

    private void Awake()
    { 
        ObjectToInstantiate[CombinationHandler.Button.BLUE] = Resources.Load<GameObject>("Prefabs/ButtonsEnemy/BlueButton");
        ObjectToInstantiate[CombinationHandler.Button.YELLOW] = Resources.Load<GameObject>("Prefabs/ButtonsEnemy/YellowButton");
        ObjectToInstantiate[CombinationHandler.Button.GREEN] = Resources.Load<GameObject>("Prefabs/ButtonsEnemy/GreenButton");
        ObjectToInstantiate[CombinationHandler.Button.RED] = Resources.Load<GameObject>("Prefabs/ButtonsEnemy/RedButton");
        boxCollider = GetComponent<BoxCollider>();
        Position = GetComponent<Transform>().position;
    }

    void Start()
    {
        Gm = GameManager.instance;  
    }

    void Start (List<CombinationHandler.Button> combination, int nbrGold, Vector3 position) {
        Combination = combination;
        NbrGold = nbrGold;
        Position = position;
        Gm = GameManager.instance;
    }

    private void Attack()
    {
        Gm.NotifyDie(gameObject);
        Destroy(gameObject);
    }

    public void Die()
    {
        Gm.NotifyDie(gameObject);
        Destroy(gameObject);
    }

    private void Move()
    {
        Position.y -= Speed;
        GetComponent<Transform>().position = new Vector3(Position.x, Position.y, Position.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "EndObject")
            Attack();
    }

    public void Setup()
    {
        int count = 1;
        foreach (CombinationHandler.Button b in Combination)
        {
            GameObject button = Instantiate(ObjectToInstantiate[b], Position, Quaternion.identity);

            button.GetComponent<SpriteRenderer>().enabled = true;
            Vector3 buttonPosition = Position;
            buttonPosition.y += boxCollider.size.y * 0.9f;
            float buttonSizeX = button.GetComponent<SpriteRenderer>().sprite.bounds.size.x * button.transform.localScale.x * 0.65f;
            float positionButtonCompare = - (float)CombinationSize * (float)buttonSizeX * 0.5f; 
            positionButtonCompare += buttonSizeX * count;
            if (CombinationSize % 2 != 0)
                positionButtonCompare -= buttonSizeX * 0.5f;
            buttonPosition.x += positionButtonCompare;
            button.transform.position = buttonPosition;
            button.transform.SetParent(gameObject.transform);
            count += 1;

            ButtonsEnemy.Add(button);
        }
    }

    // Update is called once per frame
    void Update () {
        Move();
	}

    public void UpdateWithConfig(ConfigurationEnemy setting)
    {
        CombinationSize = setting.CombinationSize;
        Speed = setting.Speed;
        NbrGold = setting.Gold;
        SpawnCooldown = setting.SpawnCoolDown;
    }

    public void FeedBackCombination(CombinationHandler CombinationPlayer, bool reset = false)
    {
        if (!reset)
        {
            int count = 0;
            foreach (CombinationHandler.Button currentButton in CombinationPlayer.GetCurrentCombination())
            {
                ButtonsEnemy[count].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
                count++;
            }
        }
        else
        {
            for (int i = 0; i < ButtonsEnemy.Count; i++)
                ButtonsEnemy[i].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
    }
}
