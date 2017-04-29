using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class PawnPowerUp : MonoBehaviour
{
    public int Level = 0;
    public int Charge = 0;
    public int ChargeMax = 100;
    protected GameManager gm;

    protected void Start()
    {
        gm = GameManager.instance;
        ChargeMax = Level >= 3 ? 50 : Level >= 1 ? 75 : 100;
    }

    public void ChargePowerUp(int charge)
    {
        Charge += charge;
    }

    public IEnumerator desactivate(float delay)
    {
        float TimeUntilSummon = delay;

        while (TimeUntilSummon > 0)
        {
            TimeUntilSummon -= Time.deltaTime;
            yield return null;
        }

        foreach (GameObject g in gm.EnemiesOnScreen)
        {
            BasicEnnemy e = g.GetComponent<BasicEnnemy>();
            e.IsMoving = true;
            if (Level >= 4)
                e.Speed *= 0.75f;
        }
    }

    public void activate()
    {
        if (Charge < ChargeMax)
        {
            Debug.Log("Not full");
            return;
        }
        Debug.Log("Activate");
        Charge = 0;
        foreach (GameObject g in gm.EnemiesOnScreen)
        {
            g.GetComponent<BasicEnnemy>().IsMoving = false;
        }
        StartCoroutine("desactivate", Level >= 2 ? 5: 3);
    }
}
