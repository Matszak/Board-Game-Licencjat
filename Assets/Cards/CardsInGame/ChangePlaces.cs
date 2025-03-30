using UnityEngine;

namespace Cards.CardsInGame
{   
    [CreateAssetMenu(fileName = "ChangePlaces", menuName = "Card/PickupCards/ChangePlaces")]
    public class ChangePlaces : PickUpCard
    {
        
        private void ApplyEffect(PlayerController obj)
        {
            Debug.Log($"{obj._player.Name} has been selected");
            obj.GetComponent<PlayerMovement>().MovePlayer(3,obj._player);
        }

        public override void TriggerCard(Player player)
        {
           
            GameManager.Instance.OnCardPlayerSelected += ApplyEffect;
            Debug.Log($"Card picked up by {player.Name}");
            GameManager.Instance.InvokeSelection(player);
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