using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class Rocket : BasicEnnemy
{
    BossBlackBishop boss;
    public float size;

    public void Explode()
    {
        Die();
    }

    protected new void Start()
    {
        base.Start();
        size = GetComponent<BoxCollider>().size.x;

        boss = GameObject.Find("BlackBishop(Clone)").GetComponent<BossBlackBishop>();
        Position = boss.addRocket(this);
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
