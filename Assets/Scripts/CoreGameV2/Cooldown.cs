using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cooldown : MonoBehaviour
{
    private Sprite cooldown;
    public bool coolingDown;
    public float waitTime = 30.0f;
    private Vector3 originalPosition;

    void Start()
    {
        cooldown = GetComponent<SpriteRenderer>().sprite;
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (coolingDown == true)
        {
            //Reduce fill amount over 30 seconds
            // cooldown.fillAmount -= 1.0f / waitTime * Time.deltaTime;
            //cooldown;
            Vector3 t = transform.localScale;
            t.x += 0.1f;
            transform.localScale = t;
            transform.position = originalPosition;
        }
    }
}
