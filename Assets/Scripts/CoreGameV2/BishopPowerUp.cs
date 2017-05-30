using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class BishopPowerUp : IPowerUp
{

    protected void Start()
    {
        gm = GameManager.instance;
        ChargeMax = Level >= 3 ? 50 : Level >= 1 ? 75 : 100;
    }
    
    override public void activate()
    {
        if (Charge < ChargeMax)
        {
            Debug.Log("Not full");
            return;
        }
        Debug.Log("Activate");
        List<BasicEnnemy> l = GameManager.instance.cm.getNearestEnnemy(Level >= 4 ? 3 : 1);
        int life = Level >= 2 ? 5 : 2;
        foreach (BasicEnnemy e in l)
        {
            life -= e.DecreaseLifePoint(life);
        }
        Charge = 0;
    }
}