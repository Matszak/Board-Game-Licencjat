using System.Linq;
using UnityEngine;

namespace Cards.PlayerCards
{   
    [CreateAssetMenu(fileName = "ChangePlaces", menuName = "Card/PickupCards/ChangePlaces")]
    public class ChangePlaces : PickUpCard
    {
        private Player owner;
        public override void TriggerCard(Player owner)
        {
            this.owner = owner;
            GameManager.Instance.OnCardPlayerSelected += ApplyEffect;
            GameManager.Instance.InvokeSelection(true);
            
        }

        private void ApplyEffect(Player target)
        {
            var playerPosition = target.PlayerObject.transform.position;
            var tempIndex = owner.TileIndex;
            var tempPosition = owner.PlayerObject.transform.position;

            owner.PlayerObject.transform.position = playerPosition;
            target.PlayerObject.transform.position = tempPosition;

            owner.TileIndex = target.TileIndex;
            target.TileIndex = tempIndex;

            GameManager.Instance.OnCardPlayerSelected -= ApplyEffect;
            GameManager.Instance.TurnEnded();
        }

        public void Awake()
        {
            
        }

        public void OnEnable()
        {
            
        }

        public void OnDestroy()
        {
            GameManager.Instance.OnCardPlayerSelected -= ApplyEffect;
        }

        public override bool CanUse => GameManager.Instance._players.Any(x => x != Player.CurrentPlayer && x.TileIndex < Player.CurrentPlayer.TileIndex);
    }
}