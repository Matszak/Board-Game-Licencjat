using Cards.EnemyCards.DefeatBehaviours;
using Cards.EnemyCards.DrawBehaviour;
using Cards.EnemyCards.WinBehaviour;
using Enemy.Behaviours.Scripts;
using UnityEngine;
using UnityEngine.Serialization;

namespace Cards.EnemyCards
{
    [CreateAssetMenu(fileName = "EnemyCard", menuName = "Card/EnemyCard")]
    public class EnemyCard :Card
    {
        public EnemyAttackBehaviour enemyAttackAttackBehaviour;
        public EnemyDefeatedBehaviour enemyDefeatedBehaviour;
        public EnemyWinBehaviour enemyWinBehaviour;
        public EnemyDrawBehaviour enemyDrawBehaviour;
        
        public override void TriggerCard(Player currentPlayer)
        {
            GameManager.Instance.StartFight(currentPlayer, this);
        }
        
        
    }
}