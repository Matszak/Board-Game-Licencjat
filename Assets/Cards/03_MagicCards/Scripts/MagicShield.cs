using System.Collections;
using System.Collections.Generic;
using Cards;
using UnityEngine;

[CreateAssetMenu(fileName = "MagicShield", menuName = "Card/PickupCards/magicShield")]
public class MagicShield : PickUpCard
{
    public override void TriggerCard(Player currentPlayer)
    {
        currentPlayer.Controller.SetMagicShield(true);
   
    }
}
