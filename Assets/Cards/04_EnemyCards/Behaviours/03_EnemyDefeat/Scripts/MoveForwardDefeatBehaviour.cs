using UnityEngine;

namespace Cards.EnemyCards.DefeatBehaviours
{
    [CreateAssetMenu(fileName = "DefeatBehaviour: move_forward", 
        menuName = "Behaviours/Enemy/DefeatBehaviours/MoveForward")]
    public class MoveForwardDefeatBehaviour : EnemyDefeatedBehaviour
    {
        public int steps;
        public override void EnemyDefeated(Player player)
        {
            player.PlayerObject.GetComponent<PlayerMovement>().MovePlayer(steps, player);
        }
    }
}