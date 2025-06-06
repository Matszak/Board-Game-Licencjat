using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace Cards.EnemyCards.WinBehaviour
{
    [CreateAssetMenu(fileName = "WinBehaviour: move_back",
        menuName = "Behaviours/Enemy/WinBehaviours/MoveBack")]

    public class MoveBackWinBehaviour : EnemyWinBehaviour
    {
        public int steps;

        public override void EnemyWin()
        {
            Player.CurrentPlayer.Controller.playerState = PlayerState.FightLose;
            Player.CurrentPlayer.Movement.MovePlayerBack(steps);
            GameManager.Instance.TurnEnded();
        }
    }

}
