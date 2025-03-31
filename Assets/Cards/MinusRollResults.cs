using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MinusRollResults", menuName = "Card/MinusRoll", order = 2)]
public class MinusRollResults : Card
{
    public int movePlayerBack;
     
    
    public override void TriggerCard(Player currentPlayer)
    {
         
        currentPlayer.PlayerObject.TryGetComponent(out PlayerMovement playerMovement);
        playerMovement.MovePlayerBack(movePlayerBack, currentPlayer);
        playerMovement.OnEndMovePlayerMove += CompleteCard;

    }
}
