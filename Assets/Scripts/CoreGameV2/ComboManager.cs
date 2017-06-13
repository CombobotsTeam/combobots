using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    GameManager gm;
    public bool BossMode = false;
    public BasicEnnemy lockEnemy = null;
    public bool immunity = false;
    int error = 1;
    List<CombinationHandler.Button> oldCombi = null;
    public int ChargeByCombo = 20;

    private SoundManager soundManager;
    protected enum Result
    {
        Failed,
        Partial,
        Complete
    };

    private void Start()
    {
        gm = GameManager.instance;
        soundManager = SoundManager.instance;
    }

    protected void failCombination(BasicEnnemy enemy)
    {
        if (!immunity || error == 0)
            enemy.FeedBackCombination(gm.Combination, true);
    }

    protected void successCombination(BasicEnnemy enemy)
    {
        enemy.DecreaseLifePoint(1);
        error = 1;
        soundManager.Play("RightCombo", false);

		// Add score
		float y = enemy.gameObject.GetComponent<BoxCollider>().size.y * enemy.gameObject.transform.localScale.y;
		Vector3 enemyTopPosition = enemy.gameObject.transform.position;
		enemyTopPosition.y += (y / 2);

		gm.AddScore(10, enemyTopPosition);

		gm.AddComboPoint(1, enemy.transform.position);
        gm.Combination.Reset();
        enemy.FeedBackCombination(gm.Combination, true);
        if (gm.powerUp)
            gm.powerUp.ChargePowerUp(ChargeByCombo);
    }

    protected void incompleteCombination(BasicEnnemy enemy)
    {
        enemy.FeedBackCombination(gm.Combination);
    }

    protected Result checkCombination(BasicEnnemy enemy)
    {
        Result ret = Result.Failed;
        if (gm.Combination.CompareCombination(enemy.getCombination()))
        {
            if (gm.Combination.isSameCombination(enemy.getCombination()))
            {
                successCombination(enemy);
                ret = Result.Complete;
            }
            else
            {
                incompleteCombination(enemy);
                ret = Result.Partial;
            }
        }
        else
        {
            failCombination(enemy);
            ret = Result.Failed;
        }
        return ret;
    }

    public BasicEnnemy getNearestEnnemy()
    {
        BasicEnnemy Ennemy = gm.EnemiesOnScreen[0].GetComponent<BasicEnnemy>();
        foreach (GameObject e in gm.EnemiesOnScreen)
        {
            if (Ennemy.transform.position.y > e.transform.position.y)
                Ennemy = e.GetComponent<BasicEnnemy>();
        }
        return Ennemy;
    }

    public List<BasicEnnemy> getNearestEnnemy(int nb)
    {
        List<BasicEnnemy> l = new List<BasicEnnemy>();
        foreach (GameObject e in gm.EnemiesOnScreen)
        {
            l.Add(e.GetComponent<BasicEnnemy>());
        }
        l = l.OrderBy(BasicEnnemy => BasicEnnemy.Position.y).ToList();
        return l.GetRange(0, nb > l.Count ? l.Count : nb);
    }

    protected void checkComboNormal()
    {
        if (lockEnemy == null)
        {
            lockEnemy = getNearestEnnemy();
        }
        if (checkCombination(lockEnemy) == Result.Failed)
        {
            soundManager.Play("WrongCombo", false);
            if (immunity && error != 0)
            {
                error -= 1;
                gm.Combination.PopButton();
            }
            else
            {
                error = 1;
                gm.Combination.Reset();
                gm.ResetComboPoint();
            }
        }
    }

    protected void clearCombination()
    {
        foreach (GameObject genemy in gm.EnemiesOnScreen)
        {
            BasicEnnemy enemy = genemy.GetComponent<BasicEnnemy>();
            enemy.FeedBackCombination(gm.Combination, true);
        }
    }

    protected void checkComboBoss()
    {
        bool isUsed = false;

        foreach (GameObject genemy in gm.EnemiesOnScreen)
        {
            BasicEnnemy enemy = genemy.GetComponent<BasicEnnemy>();
            Result res = checkCombination(enemy);
            if (res == Result.Complete)
            {
                clearCombination();
                isUsed = true;
                break;
            }
            else if (res == Result.Partial)
            {
                isUsed = true;
            }
        }
        if (!isUsed)
        {
            soundManager.Play("WrongCombo", false);
            if (immunity && error != 0)
            {
                error -= 1;
                gm.Combination.PopButton();
            }
            else
            {
                error = 1;
                gm.Combination.Reset();
                gm.ResetComboPoint();
            }
        }
    }

    public void checkCombo()
    {
        if (gm.EnemiesOnScreen.Count > 0 && gm.Combination.GetCurrentCombination().Count > 0)
        {
            if (oldCombi != null && oldCombi.SequenceEqual(gm.Combination.GetCurrentCombination()))
                return;
            oldCombi = new List<CombinationHandler.Button>(gm.Combination.GetCurrentCombination());
            if (BossMode)
            {
                checkComboBoss();
            }
            else
            {
                checkComboNormal();
            }
        }
        else if (gm.EnemiesOnScreen.Count > 0)
            gm.Combination.Reset();
    }
}
