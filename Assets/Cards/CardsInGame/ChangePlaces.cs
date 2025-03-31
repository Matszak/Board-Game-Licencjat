using UnityEngine;

namespace Cards.CardsInGame
{   
    [CreateAssetMenu(fileName = "ChangePlaces", menuName = "Card/PickupCards/ChangePlaces")]
    public class ChangePlaces : PickUpCard
    {
        

        public override void TriggerCard(Player player)
        {
           
            GameManager.Instance.OnCardPlayerSelected += ApplyEffect;
            GameManager.Instance.InvokeSelection(player);
        }

        private void ApplyEffect(PlayerController obj)
        {
            GameManager.Instance.OnCardPlayerSelected -= ApplyEffect;
        }
        
        
        public void OnDisable()
        {
            GameManager.Instance.OnCardPlayerSelected -= ApplyEffect;
        }
        public void OnDestroy()
        {
            GameManager.Instance.OnCardPlayerSelected -= ApplyEffect;
        }
    }
}