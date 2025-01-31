using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

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
        
        Vector3 movePosition = new Vector3(tiles[currentTileIndex].position.x, player.position.y, tiles[currentTileIndex].position.z);
        player.DOMove(movePosition, 1f).SetEase(Ease.OutQuad);

        if (IsPenaltyTile(tiles[currentTileIndex]))
        {
            dicePenalty = -1;
        }
        else
        {
            dicePenalty = 0;
        }
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
