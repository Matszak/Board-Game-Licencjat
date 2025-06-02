using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cards.EnemyCards.WinBehaviour
{
    [CreateAssetMenu(fileName = "WinBehaviour: move_back",
        menuName = "Behaviours/Enemy/WinBehaviours/MoveBack")]

    public class MoveBackWinBehaviour : EnemyWinBehaviour
    {
        public int steps;

        public override void EnemyWin(Player player)
        {
            player.PlayerObject.GetComponent<PlayerMovement>().MovePlayer(-steps, player);
        }
    }

}
