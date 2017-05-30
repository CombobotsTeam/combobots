using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cooldown : MonoBehaviour
{
    private Material cooldown;
    private GameManager gameManager;
    public bool enabledPower = true;
    private float currentStatus = 0f;
    private float status = 0f;
    private Vector3 originalPosition;


    private void Awake()
    {
        originalPosition = transform.position;
    }

    void Start()
    {
        if (GetComponent<SpriteRenderer>())
            cooldown = GetComponent<SpriteRenderer>().material;
        gameManager = GameManager.instance;
        if (enabledPower) {
            cooldown.SetFloat("_Fill", 0);
        }
    }

    public bool IsCharged()
    {
        return currentStatus >= 1;    
    }

    // Update is called once per frame
    void Update()
    {
        /*if (transform.position != originalPosition)
            transform.position = originalPosition;*/
        if (enabledPower)
        {
            status = (float)gameManager.powerUp.Charge / (float)gameManager.powerUp.ChargeMax;

            if (status != currentStatus)
            {
                if (currentStatus < status)
                    currentStatus += Time.deltaTime * 0.2f;
                if (currentStatus > status)
                    currentStatus = status;
                cooldown.SetFloat("_Fill", currentStatus);
            }
        }
    }
}
