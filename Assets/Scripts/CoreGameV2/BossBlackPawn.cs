using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class BossBlackPawn : BasicEnnemy
{
    public List<BlackPawnMinon> minions;
    public float stopPosition = 0;
    protected bool haveStop = false;
    protected float oldSpeed;

    protected new void Start()
    {
        base.Start();
        RectTransform TopPos = GameObject.Find("Canvas/TopBackground").GetComponent<RectTransform>();
        RectTransform BottomPos = GameObject.Find("Canvas/ButtonBackground").GetComponent<RectTransform>();
        stopPosition = TopPos.position.y + BottomPos.position.y / 2 - boxCollider.size.y * 0.9f;
        foreach (GameObject b in ButtonsEnemy)
        {
            b.SetActive(false);
        }
    }

    public override List<CombinationHandler.Button> getCombination()
    {
        if (Speed != 0f && haveStop == true)
            return Combination;
        return new List<CombinationHandler.Button>();
    }

    public void Spawn()
    {
        if (minions.Count == 4)
            return;
        ConfigurationEnemy m = new ConfigurationEnemy();
        m.Speed = 0;
        m.t = ConfigurationEnemy.Type.Boss;
        m.SpawnCoolDown = 0;
        m.name = "PawnMinion";
        m.Gold = 0;
        m.CombinationSize = 4;
        m.prefab = ConfigurationGame.instance.GenerateEnemy("PawnMinion");
        Gm.WaveManager.Spawn(m);
    }

    protected override void Move()
    {
        if (!haveStop && (Position.y < stopPosition + Speed && Position.y > stopPosition - Speed))
        {
            oldSpeed = Speed;
            Speed = 0;
            haveStop = true;
            Spawn();
        }
        base.Move();
    }

    protected IEnumerator chooseAttack(float delay)
    {
        float TimeUntilSummon = delay;

        while (TimeUntilSummon > 0)
        {
            TimeUntilSummon -= Time.deltaTime;
            yield return null;
        }

        foreach (BlackPawnMinon m in minions)
        {
            if (m.Position.y < Position.y)
            {
                m.launchAttack();
                yield break;
            }
        }
        yield return null;
    }

    public void notifyAttack()
    {
        StartCoroutine("chooseAttack", 3);
    }

    public void addMinion(BlackPawnMinon m)
    {
        minions.Add(m);
        if (minions.Count == 4)
            StartCoroutine("chooseAttack", 3);
    }

    public void MinionDeath(BlackPawnMinon m)
    {
        minions.Remove(m);
        for (int i = 0; i < minions.Count; ++i)
        {
            minions[i].addSpeed(1);
        }
        if (minions.Count == 0)
        {
            Speed = oldSpeed / 3;
            foreach (GameObject b in ButtonsEnemy)
            {
                b.SetActive(true);
            }
        }
    }
}
