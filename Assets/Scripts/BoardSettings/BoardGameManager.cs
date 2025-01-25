using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class BoardGameManager : MonoBehaviour
{
    [Header("Dice Controller")]
    [SerializeField] private DiceRoll diceRoll;
    
    
    [Header("Player Movement Controller")]
    public Transform[] tiles;
    public Transform player;
    
    private int currentTileIndex = 0;
    private int dicePenalty = 0;
    public Transform[] penaltyTiles;
    

    private void Start()
    {
  
        diceRoll.OnDiceRolled += MovePlayer;
        diceRoll.RequestDiceRoll(1,6);
    }

 

    public void MovePlayer(int steps)
    {
        int targetTileIndex = Math.Min(currentTileIndex + steps, tiles.Length - 1);

        currentTileIndex = targetTileIndex;
        
        player.DOMove(tiles[currentTileIndex].position, 1f).SetEase(Ease.OutQuad);

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
