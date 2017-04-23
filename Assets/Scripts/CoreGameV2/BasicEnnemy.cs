using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnnemy : MonoBehaviour {


    public GameObject[] buttonRef;

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

    private void Awake()
    {
        for (int i = 0; i < buttonRef.Length; i++)
        {
            if (!buttonRef[i].GetComponent<ButtonScript>())
                Debug.LogError("INVALID BUTTON PREF (BASIC ENEMY.CS)");
            ObjectToInstantiate[buttonRef[i].GetComponent<ButtonScript>().Type] = buttonRef[i];
        }
        boxCollider = GetComponent<BoxCollider>();
        Position = GetComponent<Transform>().position;
    }

    void Start()
    {
        Gm = GameManager.instance;  
    }

    // Use this for initialization
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
        int count = 0;
        foreach (CombinationHandler.Button b in Combination)
        {
            GameObject button = Instantiate(ObjectToInstantiate[b], Position, Quaternion.identity);

            button.GetComponent<SpriteRenderer>().enabled = true;
            Vector3 buttonPosition = Position;
            buttonPosition.y += boxCollider.size.y * 0.75f;
            float buttonSizeX = button.GetComponent<SpriteRenderer>().sprite.bounds.size.x * button.transform.localScale.x;
            float sizeRef = buttonSizeX  * CombinationSize;
            buttonPosition.x = sizeRef * count / CombinationSize - sizeRef / 2 + buttonSizeX / 2;
            button.transform.SetParent(gameObject.transform);
            button.transform.position = buttonPosition;
            count += 1;
        }
    }

    // Update is called once per frame
    void Update () {
        Move();
	}
}
