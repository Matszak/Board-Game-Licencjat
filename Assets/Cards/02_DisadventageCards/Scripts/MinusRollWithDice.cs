using UnityEngine;

namespace Cards.DisadventageCards
{
    [CreateAssetMenu(fileName = "MinusMoveRoll", menuName = "Card/Disadventage/MinusMoveRoll")]
    public class MinusRollWithDice : Card
    {
        public override void TriggerCard(Player currentPlayer)
        {
            GameManager.Instance.diceRoll.RequestDiceRoll(false, i =>
            {
                currentPlayer.PlayerObject.GetComponent<PlayerMovement>().MovePlayerBack(i, currentPlayer);
            });
        }
    }
}