using Spriter2UnityDX;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class Rocket : BasicEnnemy
{
    BossBlackBishop boss;
    public float size;
    EntityRenderer Rend;

    public void Explode()
    {
        Die();
    }

    protected new void Start()
    {
        base.Start();
        size = GetComponent<BoxCollider>().size.x;
        Rend = GetComponent<EntityRenderer>();

        boss = GameObject.Find("BlackBishop(Clone)").GetComponent<BossBlackBishop>();
        Position = boss.addRocket(this);
        StartCoroutine("Wait");
    }

    protected IEnumerator Wait()
    {
        foreach (GameObject b in ButtonsEnemy)
        {
            b.SetActive(false);
        }
        EnemyLifeObj.SetActive(false);
        Rend.enabled = false;
        float spd = Speed;
        Speed = 0;
        float TimeUntilSummon = 1.0f;
        while (TimeUntilSummon > 0)
        {
            TimeUntilSummon -= Time.deltaTime;
            yield return null;
        }
        Speed = spd;
        foreach (GameObject b in ButtonsEnemy)
        {
            b.SetActive(true);
        }
        EnemyLifeObj.SetActive(true);
        Rend.enabled = true;
    }

    public override void Die()
    {
        boss.RocketDeath(this);
        base.Die();
    }

    protected override void Attack()
    {
        boss.RocketDeath(this);
        base.Attack();
    }
}
