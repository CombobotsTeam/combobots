using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class KnightPowerUp : IPowerUp
{ 
    protected void Start()
    {
        gm = GameManager.instance;
        ChargeMax = Level >= 3 ? 50 : Level >= 1 ? 75 : 100;
    }

    protected IEnumerator desactivateReverse(float delay)
    {
        yield return gm.WaitFor(delay);

        foreach (GameObject g in gm.EnemiesOnScreen)
        {
            BasicEnnemy e = g.GetComponent<BasicEnnemy>();
            e.reverse = false;
        }
    }

    protected IEnumerator desactivate(float delay)
    {
        yield return gm.WaitFor(delay);

        foreach (GameObject g in gm.EnemiesOnScreen)
        {
            BasicEnnemy e = g.GetComponent<BasicEnnemy>();
            e.slow = 1;
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
            BasicEnnemy e = g.GetComponent<BasicEnnemy>();
            if (Level >= 4)
                e.reverse = true;
            e.slow *= Level >= 2 ? 0.3f : 0.6f;
        }
        if (Level >= 4)
            StartCoroutine(desactivateReverse(2));
        StartCoroutine(desactivate(5));
    }
}