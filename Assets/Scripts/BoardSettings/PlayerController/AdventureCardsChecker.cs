using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventureCardsChecker : MonoBehaviour
{
     public bool CheckIfStayOnCard(Player player)
     {
          if (GameManager.Instance.currentPlayer != player.ID) return false;

          if (!Physics.Raycast(player.PlayerObject.transform.position, Vector3.down, out var hit, Mathf.Infinity)) return false;
          if (!hit.collider.gameObject.GetComponent<AdventureTile>()) return false;
          return true;
     }
}
