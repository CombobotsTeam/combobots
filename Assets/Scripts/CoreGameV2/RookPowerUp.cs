using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class RookPowerUp : IPowerUp
{

    protected void Start()
    {
        gm = GameManager.instance;
        ChargeMax = Level >= 3 ? 50 : Level >= 1 ? 75 : 100;
    }

    protected IEnumerator desactivate(float delay)
    {
        yield return gm.WaitFor(delay);
        gm.cm.immunity = false;
        gm.immunity = false;
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
        gm.cm.immunity = true;
        gm.immunity = true;
        StartCoroutine(desactivate(Level >= 2 ? 8 : 5));
        if (Level >= 4)
            gm.AddLife(1);
    }
}
