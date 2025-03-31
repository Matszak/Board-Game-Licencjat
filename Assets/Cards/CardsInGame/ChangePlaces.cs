using UnityEngine;

namespace Cards.CardsInGame
{   
    [CreateAssetMenu(fileName = "ChangePlaces", menuName = "Card/PickupCards/ChangePlaces")]
    public class ChangePlaces : PickUpCard
    {
        
      
        
        private Player _currentPlayer;
        public override void TriggerCard(Player currentPlayer)
        {
            _currentPlayer = currentPlayer;
            GameManager.Instance.OnCardPlayerSelected += ApplyEffect;
            GameManager.Instance.InvokeSelection(currentPlayer);
        }

        private void ApplyEffect(Player  selectedPlayer)
        {
            var tempPosition = _currentPlayer.PlayerObject.transform.position;
            
            _currentPlayer.PlayerObject.transform.position = selectedPlayer.PlayerObject.transform.position;
            selectedPlayer.PlayerObject.transform.position = tempPosition;
            
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