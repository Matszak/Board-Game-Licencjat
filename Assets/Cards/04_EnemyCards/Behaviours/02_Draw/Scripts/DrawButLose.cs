using Cards.EnemyCards.DrawBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[CreateAssetMenu(fileName = "DrawBehaviour: draw_but_lose",
    menuName = "Behaviours/Enemy/DrawBehaviour/DrawButLose")]
public class DrawButLose : EnemyDrawBehaviour
{
    public override void EnemyDraw()
    {
        Player.CurrentPlayer.Controller.playerState = PlayerState.FightLose;
        GameManager.Instance.TurnEnded();
    }
}
