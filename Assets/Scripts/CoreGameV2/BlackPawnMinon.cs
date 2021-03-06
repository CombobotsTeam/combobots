﻿using Spriter2UnityDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;  
using UnityEngine;

public class BlackPawnMinon : BasicEnnemy
{
    public Vector3 RotatePosition;
    float stopPosition = 0;
    float verticalSpeed = 1f;
    float angle = 0;
    bool rotate = false;
    float startAttack = -1;
    bool spawn = true;
    BossBlackPawn boss;
    float rotateConst = 0f;
    float totalSize = 0f;
    EntityRenderer r;
    bool attack = false;

    protected new void Start()
    {
        base.Start();
        r = gameObject.GetComponent<EntityRenderer>();
        Speed = 0;
      //  RectTransform Width = GameObject.Find("Canvas/BackgroundImage").GetComponent<RectTransform>();
        RectTransform Canvas = GameObject.Find("Canvas").GetComponent<RectTransform>();
        totalSize = Screen.width * Canvas.localScale.x;
        updateRotateConst();
        float d = 0;
        for (int i = 0; i * verticalSpeed < 180; i++)
        {
            d += Mathf.Sin(Mathf.Deg2Rad * i * verticalSpeed);
        }
        boss = GameObject.Find("BlackPawn(Clone)").GetComponent<BossBlackPawn>();
        stopPosition = boss.stopPosition + d / 2 * rotateConst;
        boss.addMinion(this);
    }

    protected void updateRotateConst()
    {
        float d = 0;
        for (int i = 0; i * verticalSpeed < 180; i++)
        {
            d += Mathf.Sin(Mathf.Deg2Rad * i * verticalSpeed);
        }
        rotateConst = totalSize / d * 0.7f;
    }

    public void launchAttack()
    {
        GetComponent<Animator>().SetTrigger("Prepare");
        startAttack = 0;
    }

    protected override void Move()
    {
        if (IsMoving)
        {
            if (rotate)
            {
                angle += verticalSpeed;
                if (spawn && angle == 90)// BE Careful of the egality if you change the Vertical speed(1) before the spawn
                {
                    spawn = false;
                    boss.Spawn();
                }
                if (startAttack != -1)
                    startAttack += verticalSpeed * (reverse ? -1 : 1);
                if (angle > 360)
                    angle -= 360;
                RotatePosition.y -= Mathf.Sin(Mathf.Deg2Rad * angle) * rotateConst * (reverse ? -1 : 1);
                RotatePosition.x -= Mathf.Cos(Mathf.Deg2Rad * angle) * rotateConst * (reverse ? -1 : 1);
            }
            else
            {
                Position.y = stopPosition;
                rotate = true;
                RotatePosition = Position;
            }
            if (!attack)
                Position = RotatePosition;
            base.Move();
        }
    }

    protected override void Update()
    {
        base.Update();
        if (startAttack > 360)
        {
            GetComponent<Animator>().SetTrigger("Attack");
            attack = true;
            Speed = verticalSpeed / 3;
            startAttack = -1;
        }
    }

    public void addSpeed(float speed)
    {
        verticalSpeed += speed;
        updateRotateConst();
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "EndObject")
        {
            Attack();
        }
    }

    public override int DecreaseLifePoint(int lp)
    {
        Position = RotatePosition;
        if (startAttack >= 0 || attack == true)
        {
            boss.notifyAttack();
        }
        if (attack == true)
            GetComponent<Animator>().SetTrigger("Spawn");
        else
            GetComponent<Animator>().SetTrigger("Damage");
        startAttack = -1;
        Speed = 0;
        attack = false;
        return base.DecreaseLifePoint(lp);
    }

    protected override void Attack()
    {
        GetComponent<Animator>().SetTrigger("Spawn");
        Position = RotatePosition;
        Speed = 0;
        attack = false;
        Gm.RemoveLife(1);
        boss.notifyAttack();
    }

    public override void Die()
    {
        if (Speed > 0 || startAttack != -1)
            boss.notifyAttack();
        boss.MinionDeath(this);
        base.Die();
    }
}
