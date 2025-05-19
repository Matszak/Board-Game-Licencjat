using UnityEngine;

namespace Cards.PlayerCards
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
            var playerPosition = selectedPlayer.PlayerObject.transform.position;
            var tempIndex = _currentPlayer.TileIndex;
            var tempPosition = _currentPlayer.PlayerObject.transform.position;

            _currentPlayer.PlayerObject.transform.position = playerPosition;
            selectedPlayer.PlayerObject.transform.position = tempPosition;
            
            _currentPlayer.TileIndex = selectedPlayer.TileIndex;
            selectedPlayer.TileIndex = tempIndex;
            
                
            
            GameManager.Instance.OnCardPlayerSelected -= ApplyEffect;
            GameManager.Instance.TurnEnded(_currentPlayer);
        }
 
        public void OnDestroy()
        {
            GameManager.Instance.OnCardPlayerSelected -= ApplyEffect;
        }
    }
}