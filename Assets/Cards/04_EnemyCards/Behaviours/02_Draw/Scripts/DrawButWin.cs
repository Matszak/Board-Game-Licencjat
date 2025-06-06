using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cards.EnemyCards.DrawBehaviour
{
    [CreateAssetMenu(fileName = "DrawBehaviour: draw_but_win",
        menuName = "Behaviours/Enemy/DrawBehaviour/DrawButWin")]

    public class DrawButWinDrawBehaviour : EnemyDrawBehaviour
    {
        public override void EnemyDraw()
        {
            Player.CurrentPlayer.Controller.playerState = PlayerState.None;
            GameManager.Instance.TurnEnded();
        }
    }
}
