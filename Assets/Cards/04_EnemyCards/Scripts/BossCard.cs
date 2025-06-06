using System;

namespace Cards.EnemyCards
{
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