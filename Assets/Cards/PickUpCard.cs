using System;
using UnityEngine;

namespace Cards
{
    [CreateAssetMenu(fileName = "EmptyPickUpCard", menuName = "Card/PickupCards")]
    public class PickUpCard : Card
    {   
        
        public override void TriggerCard(Player player)
        {
            Debug.Log($"Card picked up by {player.Name}");
        }

        private void OnEnable()
        {
            if(cardPrefab.TryGetComponent(out CardUsage card))
            {
                card.pickUpCard = this;
            };
        }
    } 
}