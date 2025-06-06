using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MinusMove", menuName = "Card/Disadvantage/MinusMove")]
public class MinusMove  : Card
{
    public int movePlayerBack;
     
    
    public override void TriggerCard(Player currentPlayer)
    {         
        currentPlayer.Movement.MovePlayerBack(movePlayerBack);
        currentPlayer.Movement.OnEndMovePlayerMove += CompleteCard;
    }
}
