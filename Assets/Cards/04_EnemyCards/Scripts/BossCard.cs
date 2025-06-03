using System;

namespace Cards.EnemyCards
{
    public class BossCard : EnemyCard
    {
        public void OnEnable()
        {
            FightSystem. EndEnemyFight += WinGame;         
        }

        private void WinGame(FightSystem.FightResult fightResult, Player player, EnemyCard enemyCard)
        {
            if (fightResult == FightSystem.FightResult.Win)
            {
                GameManager.Instance.WinGame(player);
            }
        }
    }
}