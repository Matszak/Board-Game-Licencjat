using System;
using UnityEngine;

namespace Cards
{
    [CreateAssetMenu(fileName = "EmptyPickUpCard", menuName = "Card/PickupCards")]
    public class PickUpCard : Card
    {
  
        private void ApplyEffect(PlayerController obj)
        {
            Debug.Log($"{obj.CurrentPlayer.Name} has been selected");
        }

        public override void TriggerCard(Player currentPlayer)
        {
            DebugConsole.Log($"{currentPlayer.Name} picked up card {cardPrefab.name}");
            Debug.Log($"Card picked up by {currentPlayer.Name}");
            GameManager.Instance.InvokeSelection(currentPlayer);
   
        }
 
    } 
}