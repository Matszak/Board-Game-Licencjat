using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.EventSystems;

public class BoardGameManager : MonoBehaviour
{
    
    [Header("Player Movement Controller")]
    public Transform[] tiles;
    public Transform player;
    
    private int currentTileIndex = 0;
    private int dicePenalty = 0;
    
    public Transform[] penaltyTiles;

    
    private void OnEnable()
    {
        DiceRoll.OnDiceRolled += MovePlayer;
    }
    private void OnDisable()
    {
        DiceRoll.OnDiceRolled -= MovePlayer;
    }
    
   

    public void MovePlayer(int steps)
    {
        int targetTileIndex = Math.Min(currentTileIndex + steps, tiles.Length - 1);

        currentTileIndex = targetTileIndex;
        
        player.DOMove(tiles[currentTileIndex].position, 1f).SetEase(Ease.OutQuad).OnComplete(StartActionOnTile);

        if (IsPenaltyTile(tiles[currentTileIndex]))
        {
            dicePenalty = -1;
        }
        else
        {
            dicePenalty = 0;
        }
    }

    private void StartActionOnTile()
    {
        StartCoroutine(TileActionAfterDelay());
    }

    private IEnumerator TileActionAfterDelay()
    {
        yield return new WaitForSeconds(1);
        if (Physics.Raycast(player.transform.position + Vector3.up * 5, Vector3.down, out RaycastHit hit, 10, LayerMask.GetMask("Tile")))
        { 
            AdventureTile tile = hit.transform.GetComponent<AdventureTile>();
            if (tile)
            {
                tile.TileAction();
            }
        }

        yield break;
    }
    private bool IsPenaltyTile(Transform tile)
    {
        foreach (Transform penaltyTiles in penaltyTiles)
        {
            if (penaltyTiles == tile)
            {
                return true;
            }
        }

        return false;
    }
}
