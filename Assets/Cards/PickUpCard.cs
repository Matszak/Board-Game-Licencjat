using UnityEngine;

namespace Cards
{
    [CreateAssetMenu(fileName = "EmptyPickUpCard", menuName = "Card/PickupCards")]
    public class PickUpCard : Card
    {
        public override void TriggerCard(Player player)
        {
            throw new System.NotImplementedException();
        }
    }
}