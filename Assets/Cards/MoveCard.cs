using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveCard", menuName = "Card", order = 1)]
public class MoveCard : Card
{
    public int movePlayerBy;
    public override void TriggerCard(Player currentPlayer)
    {
        currentPlayer.PlayerObject.TryGetComponent(out PlayerMovement playerMovement);
        playerMovement.MovePlayer(movePlayerBy, currentPlayer);
        playerMovement.OnEndMovePlayerMove += CompleteCard;
    }
}
