using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Cards.CardsInGame
{
    [CreateAssetMenu(fileName = "ChangePlacesCard")]
    public class ChangePlacesCard : Card
    {
        public override void TriggerCard(Player player)
        {
            var playerWithCard = player;
        }

        void OnMouseOver()
        {
            
        }

        IEnumerator WaitAndShowMouseOver()
        {
            yield return new WaitForSeconds(5f); // Waits for 5 seconds
            Debug.Log("Mouse over GameObject after 5 seconds");
        }
    }
}