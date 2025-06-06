using UnityEngine;

namespace Cards.PlayerCards
{
    [CreateAssetMenu(fileName = "StunCard", menuName = "Card/PickupCards/StunCard")]
    public class PlayerStunCard : PickUpCard
    {
        public override void TriggerCard(Player currentPlayer)
        {
            GameManager.Instance.OnCardPlayerSelected += ApplyEffect;
            GameManager.Instance.InvokeSelection(false);
        }

        private void ApplyEffect(Player target)
        {            
            target.Controller.StunPlayer(1);
            GameManager.Instance.OnCardPlayerSelected -= ApplyEffect;            
        }

        private void OnDestroy()
        {
            GameManager.Instance.OnCardPlayerSelected -= ApplyEffect;
        }
    }
}
