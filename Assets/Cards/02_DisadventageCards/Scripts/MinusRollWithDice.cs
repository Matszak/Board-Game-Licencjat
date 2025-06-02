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
                currentPlayer.PlayerObject.GetComponent<PlayerMovement>().MovePlayerBack(i, currentPlayer);
            });
            currentPlayer.PlayerObject.GetComponent<PlayerMovement>().OnEndMovePlayerMove += CompleteCard;
        }
    }
}