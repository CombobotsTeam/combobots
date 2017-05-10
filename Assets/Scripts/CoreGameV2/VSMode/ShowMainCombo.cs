using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMainCombo : MonoBehaviour
{
    public int pos = 0;
    public int percentofsizeheight = 25;

    public bool player1;
    public bool main;
    public bool player2;
    public GameObject gameManager;
    private Vector3 Position = new Vector3(0.0f, 0.0f, 0.0f);//value of setting position

    private CombiManager combiManager = null;
    private VSCombinationHandler VSHandler = null;
    private int count;
    void Start()
    {
        combiManager = gameManager.GetComponent<CombiManager>();
        VSHandler = gameManager.GetComponent<VSCombinationHandler>();

        if (main)
        {
            GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / combiManager.Size, Screen.width / combiManager.Size);
            Position.x = Screen.width / combiManager.Size * pos - Screen.width / 2;
            Position.y = 0 - (Screen.width / combiManager.Size) / 2;
        }

        if (player1)
        {
            GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / combiManager.Size / 2, Screen.width / combiManager.Size / 2);
            Position.x = Screen.width / combiManager.Size * pos - Screen.width / 2 + (Screen.width / combiManager.Size / 4);
            Position.y = 0 - (Screen.width / combiManager.Size);
        }

        if (player2)
        {
            GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / combiManager.Size / 2, Screen.width / combiManager.Size / 2);
            Position.x = Screen.width / combiManager.Size * (combiManager.Size - 1 - pos) - Screen.width / 2 + (Screen.width / combiManager.Size / 4);
            Position.y = 0 + (Screen.width / combiManager.Size) / 2;
        }

        GetComponent<RectTransform>().localPosition = Position;
    }

    private void Update()
    {
        count = 0;
        if (main)
        {
            if (combiManager.GetCombination()[pos] == VSCombinationHandler.Button.RED)
                this.GetComponentInChildren<SpriteRenderer>().sprite = combiManager.red;
            if (combiManager.GetCombination()[pos] == VSCombinationHandler.Button.BLUE)
                this.GetComponentInChildren<SpriteRenderer>().sprite = combiManager.blue;
            if (combiManager.GetCombination()[pos] == VSCombinationHandler.Button.YELLOW)
                this.GetComponentInChildren<SpriteRenderer>().sprite = combiManager.yellow;
            if (combiManager.GetCombination()[pos] == VSCombinationHandler.Button.GREEN)
                this.GetComponentInChildren<SpriteRenderer>().sprite = combiManager.green;
        }

        if (this.GetComponentInChildren<SpriteRenderer>().enabled == false)
            this.GetComponentInChildren<SpriteRenderer>().enabled = true;
        if (player1)
        {
            if (VSHandler.GetCurrentCombinationPlayer(1).Count - 1 < pos)
            {
                this.GetComponentInChildren<SpriteRenderer>().enabled = false;
            }
            else
            {
                foreach (CombinationHandler.Button x in VSHandler.GetCurrentCombinationPlayer(1))
                {
                    if (count == pos)
                    {
                        if (x == CombinationHandler.Button.RED)
                            this.GetComponentInChildren<SpriteRenderer>().sprite = combiManager.red;
                        if (x == CombinationHandler.Button.BLUE)
                            this.GetComponentInChildren<SpriteRenderer>().sprite = combiManager.blue;
                        if (x == CombinationHandler.Button.YELLOW)
                            this.GetComponentInChildren<SpriteRenderer>().sprite = combiManager.yellow;
                        if (x == CombinationHandler.Button.GREEN)
                            this.GetComponentInChildren<SpriteRenderer>().sprite = combiManager.green;
                    }
                    count++;
                }
            }
        }
        if (player2)
        {
            if (VSHandler.GetCurrentCombinationPlayer(2).Count - 1 < pos)
            {
                this.GetComponentInChildren<SpriteRenderer>().enabled = false;
            }
            else
            {
                foreach (CombinationHandler.Button x in VSHandler.GetCurrentCombinationPlayer(2))
                {
                    if (count == pos)
                    {
                        if (x == CombinationHandler.Button.RED)
                            this.GetComponentInChildren<SpriteRenderer>().sprite = combiManager.red;
                        if (x == CombinationHandler.Button.BLUE)
                            this.GetComponentInChildren<SpriteRenderer>().sprite = combiManager.blue;
                        if (x == CombinationHandler.Button.YELLOW)
                            this.GetComponentInChildren<SpriteRenderer>().sprite = combiManager.yellow;
                        if (x == CombinationHandler.Button.GREEN)
                            this.GetComponentInChildren<SpriteRenderer>().sprite = combiManager.green;
                    }
                    count++;
                }
            }
        }
    }
}
