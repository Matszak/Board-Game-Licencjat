using UnityEngine;

namespace Cards.EnemyCards.DefeatBehaviours
{
     [CreateAssetMenu(fileName = "OneDice", menuName = "Behaviours/Enemy/DefeatBehaviours/WinGame")]
    public class WinGame : EnemyDefeatedBehaviour
    {
        public override void EnemyDefeated(Player player)
        {
            GameManager.Instance.WinGame(player);
        }
    }
}