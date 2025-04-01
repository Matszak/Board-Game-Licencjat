using DefaultNamespace;
using UnityEngine;

namespace Cards.EnemyCards
{
    [CreateAssetMenu(fileName = "Orc", menuName = "Card/PickupCards/Enemies/Orc")]
    public class OrcCard :Card
    {
        public Enemy enemy;
        public override void TriggerCard(Player currentPlayer)
        {
            GameManager.Instance.StartFight(currentPlayer, enemy);
        }
    }
}