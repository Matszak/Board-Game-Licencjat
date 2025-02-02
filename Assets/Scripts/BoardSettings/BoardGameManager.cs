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

    private void Start()
    {
        Vector3 startPosition = new Vector3(tiles[currentTileIndex].position.x, player.position.y, tiles[currentTileIndex].position.z);
        player.transform.position = startPosition;
    }

    public void MovePlayer(int steps)
    {
        int targetTileIndex = Math.Min(currentTileIndex + steps, tiles.Length - 1);
        
        
        
        Sequence sequence = DOTween.Sequence();
        
        for (int i = currentTileIndex; i <= targetTileIndex; i++)
        {
            Vector3 movePosition = new Vector3(
                tiles[i].position.x,
                player.position.y,
                tiles[i].position.z);
 
            sequence.Append(player.DOJump(movePosition,6f,1, 0.5f).SetEase(Ease.OutQuad));
        }
        sequence.Play();
        currentTileIndex = targetTileIndex;
        
        
       

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
