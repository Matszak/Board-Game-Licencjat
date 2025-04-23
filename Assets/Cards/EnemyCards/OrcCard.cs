using UnityEngine;

namespace Cards.EnemyCards
{
    [CreateAssetMenu(fileName = "EnemyCard", menuName = "Card/EnemyCard")]
    public class EnemyCard :Card
    {
        public EnemyBehaviour enemyBehaviour;
        
        public override void TriggerCard(Player currentPlayer)
        {
            GameManager.Instance.StartFight(currentPlayer, this);
        }
    }
}