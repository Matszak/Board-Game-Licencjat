using UnityEngine;

namespace Cards._02_DisadventageCards.Scripts
{   
    [CreateAssetMenu(fileName = "Trap", menuName = "Card/Disadvantage/Trap")]
    public class Trap : Card
    {
        public int numberOfTurns;
        public override void TriggerCard(Player currentPlayer)
        {
            currentPlayer.PlayerObject.GetComponent<PlayerController>().StunPlayer(numberOfTurns, currentPlayer);
            CompleteCard(currentPlayer);
        }
    }
}
