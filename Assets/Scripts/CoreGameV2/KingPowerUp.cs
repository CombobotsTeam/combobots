using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

class KingPowerUp : MonoBehaviour, IPowerUp
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

    public void activate()
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
