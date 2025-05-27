using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BonusValueToRoll", menuName = "Card/BonusCards/BonusValueToRoll", order = 1)]
public class BonusValueToRoll : Card
{
    public int bonusToDiceRoll;
    public override void TriggerCard(Player currentPlayer)
    {
        currentPlayer.PlayerObject.GetComponent<PlayerController>().SetBonusDiceRoll(bonusToDiceRoll);
    }
}
