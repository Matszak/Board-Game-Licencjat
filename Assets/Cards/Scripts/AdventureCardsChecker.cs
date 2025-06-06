using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventureCardsChecker : MonoBehaviour
{
    private void Awake()
    {

    }

    private AdventureTile adventureTile;

    public Player Player { get; set; }

    public bool CheckIfStayOnCard()
    {
        if (!Physics.Raycast(Player.PlayerObject.transform.position, Vector3.down, out var hit, Mathf.Infinity)) return false;
        if (!hit.collider.gameObject.GetComponent<AdventureTile>()) return false;
        adventureTile = hit.collider.gameObject.GetComponent<AdventureTile>();
        return true;
    }

    public AdventureTile GetTile()
    {
        return adventureTile;
    }
}
