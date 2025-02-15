using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventureCardsChecker : MonoBehaviour
{
     private AdventureTile adventureTile;
     public bool CheckIfStayOnCard(Player player)
     {
          if (player.PlayerObject != gameObject) return false;

          if (!Physics.Raycast(player.PlayerObject.transform.position, Vector3.down, out var hit, Mathf.Infinity)) return false;
          if (!hit.collider.gameObject.GetComponent<AdventureTile>()) return false;
          adventureTile = hit.collider.gameObject.GetComponent<AdventureTile>();
          return true;
     }

     public AdventureTile GetTile(Player player)
     {
          if (player.PlayerObject != gameObject) return null;
          
          return adventureTile;
     }
}
