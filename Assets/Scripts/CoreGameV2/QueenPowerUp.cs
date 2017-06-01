using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class QueenPowerUp : IPowerUp 
{
    protected void Start()
    {
        gm = GameManager.instance;
        ChargeMax = Level >= 3 ? 50 : Level >= 1 ? 75 : 100;
    }

    override public void activate()
    {
    /*    if (Charge < ChargeMax)
        {
            Debug.Log("Not full");
            return;
        }*/
        Charge = 0;
        foreach (GameObject g in gm.EnemiesOnScreen)
        {
            BasicEnnemy e = g.GetComponent<BasicEnnemy>();
            e.DecreaseLifePoint(Level > 1 ? 3 : 1);
            if (e.Died)
                gm.ComboCount += 1;
        }
    }
}
