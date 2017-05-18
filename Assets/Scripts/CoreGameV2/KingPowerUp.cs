using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

class KingPowerUp : IPowerUp
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
        Charge = 0;
    }
}
