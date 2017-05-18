using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

abstract public class IPowerUp : MonoBehaviour
{
    public int Level = 0;
    public int Charge = 0;
    public int ChargeMax = 100;
    protected GameManager gm;

    public void ChargePowerUp(int charge)
    {
        Charge += charge;
    }

    abstract public void activate();
}

