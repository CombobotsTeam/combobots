using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnnemy : MonoBehaviour {

    public List<CombinationHandler.Button> Combination = new List<CombinationHandler.Button> { CombinationHandler.Button.BLUE };
    public int Life = 3;
    public float Speed = 0.1f;
    public bool IsMoving = true;
    public int NbrGold = 0;
    public Vector3 Position;
    public float SpawnCooldown = 1000.0f;
    GameManager Gm;

    void Start()
    {
        Position = GetComponent<Transform>().position;
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
            GameObject buttonRef;
            switch (b)
            {
                case CombinationHandler.Button.BLUE:
                    buttonRef = GameObject.Find("RobotBlueButton");
                    break;
                case CombinationHandler.Button.GREEN:
                    buttonRef = GameObject.Find("RobotGreenButton");
                    break;
                case CombinationHandler.Button.RED:
                    buttonRef = GameObject.Find("RobotRedButton");
                    break;
                case CombinationHandler.Button.YELLOW:
                    buttonRef = GameObject.Find("RobotYellowButton");
                    break;
                default:
                    return;
            }
            GameObject button = Instantiate(buttonRef, Position, Quaternion.identity);
            Vector3 buttonPosition = Position;
            buttonPosition.y += GetComponent<BoxCollider>().size.y * 0.75f;
            float sizeRef = button.GetComponent<RectTransform>().sizeDelta.x * Combination.Count;
            buttonPosition.x = sizeRef * count / Combination.Count - sizeRef / 2 + button.GetComponent<RectTransform>().sizeDelta.x / 2;
            button.transform.SetParent(gameObject.transform);
            button.GetComponent<RectTransform>().localPosition = buttonPosition;
            count += 1;
        }
    }

    // Update is called once per frame
    void Update () {
        Move();
	}
}
