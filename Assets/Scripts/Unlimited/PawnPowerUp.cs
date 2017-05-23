using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Unlimited
{
    public class PawnPowerUp : IPowerUp
    {
        GameManager gm;

        protected void Start()
        {
            gm = GameManager.instance;
            ChargeMax = Level >= 3 ? 50 : Level >= 1 ? 75 : 100;
        }

        protected IEnumerator desactivate(float delay)
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

        public override void activate()
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
            StartCoroutine("desactivate", Level >= 2 ? 5 : 3);
        }
    }
}