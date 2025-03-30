using System;
using UnityEngine;

namespace Cards
{
    [CreateAssetMenu(fileName = "EmptyPickUpCard", menuName = "Card/PickupCards")]
    public class PickUpCard : Card
    {
        public void Awake()
        {
            GameManager.Instance.OnCardPlayerSelected += ApplyEffect;
        }

        private void ApplyEffect(PlayerController obj)
        {
            Debug.Log($"{obj._player.Name} has been selected");
        }

        public override void TriggerCard(Player player)
        {
           
            Debug.Log($"Card picked up by {player.Name}");
            GameManager.Instance.InvokeSelection(player);
   
        }
 
    } 
}