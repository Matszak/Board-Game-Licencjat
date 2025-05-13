using UnityEngine;

namespace Cards.BonusCards
{
    [CreateAssetMenu(fileName = "BonusTurn", menuName = "Card/BonusCards/BonusTurn")]
    public class BonusTurn : Card
    {
        public override void TriggerCard(Player currentPlayer)
        {
            GameManager.Instance.NextTurn(true);
        }
    }
}