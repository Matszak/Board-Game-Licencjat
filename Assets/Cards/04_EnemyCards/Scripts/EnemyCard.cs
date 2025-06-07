using Cards.EnemyCards.DefeatBehaviours;
using Cards.EnemyCards.DrawBehaviour;
using Cards.EnemyCards.WinBehaviour;
using Enemy.Behaviours.Scripts;
using UnityEngine;
using UnityEngine.Serialization;

namespace Cards.EnemyCards
{
    [CreateAssetMenu(fileName = "EnemyCard:enemy", menuName = "Card/EnemyCard/Enemy")]
    public class EnemyCard :Card
    {
        public EnemyAttackBehaviour enemyAttackAttackBehaviour;
        public EnemyDefeatedBehaviour enemyDefeatedBehaviour;
        public EnemyWinBehaviour enemyWinBehaviour;
        public EnemyDrawBehaviour enemyDrawBehaviour;
        
        public override void TriggerCard(Player currentPlayer)
        {
            GameManager.Instance.StartFight();
        }
        
        
    }
}