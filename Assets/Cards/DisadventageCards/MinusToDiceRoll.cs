using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "MinusToRoll", menuName = "Card/Disadventage/MinusToRoll")]
public class MinusToDiceRoll : Card
{   
    public int minusToDiceValue;
    public override void TriggerCard(Player currentPlayer)
    {
        currentPlayer.PlayerObject.GetComponent<PlayerController>().SetMinusDiceRoll(minusToDiceValue);
        CompleteCard(currentPlayer);
    }
}
