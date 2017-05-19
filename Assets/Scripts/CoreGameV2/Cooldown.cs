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
        cooldown = GetComponent<SpriteRenderer>().material;
        gameManager = GameManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (enabledPower)
        {
            if (transform.position != originalPosition)
                transform.position = originalPosition;
            status = (float)gameManager.powerUp.Charge / (float)gameManager.powerUp.ChargeMax;
            if (currentStatus < status)
                currentStatus += Time.deltaTime;
            if (currentStatus > status)
                currentStatus = status;
            cooldown.SetFloat("_Fill", currentStatus);
        }
    }
}
