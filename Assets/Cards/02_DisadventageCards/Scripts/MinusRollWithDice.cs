using UnityEngine;

namespace Cards._02_DisadventageCards.Scripts
{
    [CreateAssetMenu(fileName = "MinusMoveRoll", menuName = "Card/Disadvantage/MinusMoveRoll")]
    public class MinusRollWithDice : Card
    {
        public override void TriggerCard(Player currentPlayer)
        {
            GameManager.Instance.diceRoll.RequestDiceRoll(false, i =>
            {
                currentPlayer.Movement.MovePlayerBack(i);
            });
            currentPlayer.Movement.OnEndMovePlayerMove += CompleteCard;
        }
    }
}