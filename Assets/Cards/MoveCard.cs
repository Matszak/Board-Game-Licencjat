using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveCard", menuName = "Card", order = 1)]
public class MoveCard : Card
{
    public int movePlayerBy;
    public override void TriggerCard(Player player)
    {
        player.PlayerObject.GetComponent<PlayerMovement>().MovePlayer(movePlayerBy, player);
        
    }
}
