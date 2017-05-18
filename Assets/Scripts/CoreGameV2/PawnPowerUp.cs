using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class PawnPowerUp : IPowerUp
{
    protected void Start()
    {
        gm = GameManager.instance;
        ChargeMax = Level >= 3 ? 50 : Level >= 1 ? 75 : 100;
    }

    protected IEnumerator desactivate(float delay)
    {
        yield return gm.WaitFor(delay);

        foreach (GameObject g in gm.EnemiesOnScreen)
        {
            BasicEnnemy e = g.GetComponent<BasicEnnemy>();
            e.IsMoving = true;
            if (Level >= 4)
                e.slow *= 0.75f;
        }
    }

    override public void activate()
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
