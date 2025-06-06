using UnityEngine;

namespace Cards._02_DisadventageCards.Scripts
{
    [CreateAssetMenu(fileName = "MinusToRoll", menuName = "Card/Disadvantage/MinusToDiceRoll")]
    public class MinusToDiceRoll : Card
    {   
        public int minusToDiceValue;
        public override void TriggerCard(Player currentPlayer)
        {
            currentPlayer.Controller.SetMinusDiceRoll(minusToDiceValue);
            CompleteCard();
        }
    }
}
