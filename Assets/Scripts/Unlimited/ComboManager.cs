using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace Unlimited
{
    public class ComboManager : MonoBehaviour
    {
        GameManager gm;
        public bool BossMode = false;
        public BasicEnnemy lockEnemy = null;
        protected enum Result
        {
            Failed,
            Partial,
            Complete
        };

        private void Start()
        {
            gm = GameManager.instance;
        }

        protected void failCombination(BasicEnnemy enemy)
        {
            enemy.FeedBackCombination(gm.Combination, true);
        }

        protected void successCombination(BasicEnnemy enemy)
        {
            enemy.DecreaseLifePoint(1);
            gm.AddScore(10,Vector3.zero);//?
            gm.AddComboPoint(1,Vector3.zero);//?
            gm.Combination.Reset();
            enemy.FeedBackCombination(gm.Combination, true);
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

        protected void checkComboNormal()
        {
            if (lockEnemy == null)
            {
                lockEnemy = gm.EnemiesOnScreen[0].GetComponent<BasicEnnemy>();
                foreach (GameObject enemy in gm.EnemiesOnScreen)
                {
                    if (lockEnemy.transform.position.y > enemy.transform.position.y)
                        lockEnemy = enemy.GetComponent<BasicEnnemy>();
                }
            }
            if (checkCombination(lockEnemy) == Result.Failed)
            {
                gm.Combination.Reset();
                gm.ResetComboPoint();
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
                gm.Combination.Reset();
                gm.ResetComboPoint();
            }
        }

        public void checkCombo()
        {
            if (gm.EnemiesOnScreen.Count > 0 && gm.Combination.GetCurrentCombination().Count > 0)
            {
                if (BossMode)
                {
                    checkComboBoss();
                }
                else
                {
                    checkComboNormal();
                }
            }
        }
    }
}