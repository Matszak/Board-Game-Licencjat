using System;
using UnityEngine;

namespace Cards.EnemyCards
{
    [CreateAssetMenu(fileName = "EnemyCard: boss", menuName = "Card/EnemyCard/Boss")]
    public class BossCard : EnemyCard
    {
        public void OnEnable()
        {
            FightSystem. EndEnemyFight += WinGame;         
        }

        private void WinGame(FightSystem.FightResult fightResult)
        {
            if (fightResult == FightSystem.FightResult.Win)
            {
                GameManager.Instance.WinGame();
            }
        }
    }
}