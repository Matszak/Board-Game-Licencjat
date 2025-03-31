using System;
using UnityEngine;

namespace Cards
{
    [CreateAssetMenu(fileName = "EmptyPickUpCard", menuName = "Card/PickupCards")]
    public class PickUpCard : Card
    {
  
        private void ApplyEffect(PlayerController obj)
        {
            Debug.Log($"{obj.Player.Name} has been selected");
        }

        public override void TriggerCard(Player player)
        {
           
            Debug.Log($"Card picked up by {player.Name}");
            GameManager.Instance.InvokeSelection(player);
   
        }
 
    } 
}