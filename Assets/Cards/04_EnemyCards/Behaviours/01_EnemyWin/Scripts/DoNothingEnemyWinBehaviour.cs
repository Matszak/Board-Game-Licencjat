using Cards.EnemyCards.WinBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "WinBehaviour: do_nothing",
        menuName = "Behaviours/Enemy/WinBehaviours/DoNothing")]
public class DoNothingEnemyWinBehaviour : EnemyWinBehaviour
{
    public override void EnemyWin()
    {
        Player.CurrentPlayer.Controller.playerState = PlayerState.FightLose;
        GameManager.Instance.TurnEnded();
    }
}
