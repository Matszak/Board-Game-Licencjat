using UnityEngine;

namespace Cards.PlayerCards
{
    [CreateAssetMenu(fileName = "StunCard", menuName = "Card/PickupCards/StunCard")]
    public class PlayerStunCard : PickUpCard
    {
        public override void TriggerCard(Player currentPlayer)
        {
            GameManager.Instance.OnCardPlayerSelected += ApplyEffect;
            GameManager.Instance.InvokeSelection(currentPlayer);
        }

        private void ApplyEffect(Player selectedPlayer)
        {
            selectedPlayer.PlayerObject.GetComponent<PlayerController>().StunPlayer(1,selectedPlayer);
            GameManager.Instance.OnCardPlayerSelected -= ApplyEffect;
            
        }

        private void OnDestroy()
        {
            GameManager.Instance.OnCardPlayerSelected -= ApplyEffect;
        }
    }
}
