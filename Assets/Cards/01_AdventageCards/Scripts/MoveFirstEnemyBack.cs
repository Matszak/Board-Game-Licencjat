using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Cards.CardsInGame
{
    [CreateAssetMenu(fileName = "PlayerInFrontBack", menuName = "Card/PickupCards/PlayerInFrontBack")]
    public class MoveFirstEnemyBack : PickUpCard
    {
        public int stepsBack;
        private Player _currentPlayer;
        private List<Player> _playersRanking;
        
        public override void TriggerCard(Player currentPlayer)
        {
            _currentPlayer = currentPlayer;
            _playersRanking = GameManager.Instance._playersRank;

            int minTreshold = 0;
               
            for (int i = 0; i < _playersRanking.Count; i++)
            {
                int maxTreshold = _playersRanking[_playersRanking.Count - 1].TileIndex;
                if (_playersRanking[i] == _currentPlayer)
                {
                    minTreshold = _playersRanking[i].TileIndex;
                    break;
                }
            }
            
            Player playerInFront = _playersRanking.Where(n => n.TileIndex > minTreshold).OrderBy(n => n.TileIndex).FirstOrDefault();

            if (playerInFront == null) return;
            playerInFront.Movement.MovePlayerBack(stepsBack);

            Debug.Log(playerInFront.Name);
        }
 
 
     
    }
}