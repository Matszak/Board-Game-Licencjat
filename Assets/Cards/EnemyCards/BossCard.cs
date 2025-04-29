using System;

namespace Cards.EnemyCards
{
    public class BossCard : EnemyCard
    {
        public void OnEnable()
        {
            FightSystem. EndEnemyFight += WinGame;         
        }

        private void WinGame(bool win, Player player, EnemyCard enemyCard)
        {
            if (win)
            {
                GameManager.Instance.WinGame(player);
            }
        }
    }
}