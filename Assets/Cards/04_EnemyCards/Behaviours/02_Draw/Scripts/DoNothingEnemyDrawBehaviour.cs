using UnityEngine;

namespace Cards.EnemyCards.DrawBehaviour
{[CreateAssetMenu(fileName = "DrawBehaviour: draw_do_nothing",
        menuName = "Behaviours/Enemy/DrawBehaviour/DoNothing")]
    public class DoNothingEnemyDrawBehaviour : EnemyDrawBehaviour
    {
        public override void EnemyDraw()
        {
            Player.CurrentPlayer.Controller.playerState = PlayerState.FightLose;
            GameManager.Instance.TurnEnded();
        }
    }
}