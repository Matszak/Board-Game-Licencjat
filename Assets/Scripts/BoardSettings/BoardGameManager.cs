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
    [SerializeField] private TMP_Text diceResultText;
    [SerializeField] private Button rollDiceButton;
    
    [Header("Player Movement Controller")]
    public Transform[] tiles;
    public Transform player;
    
    private int currentTileIndex = 0;

    private int dicePenalty = 0;

    public Transform[] penaltyTiles;


    private void Start()
    {
        if (rollDiceButton != null)
        {
            rollDiceButton.onClick.AddListener(RollDiceAndMove);
        }
    }

    private void RollDiceAndMove()
    {
        int diceResult = UnityEngine.Random.Range(1, 7);
        diceResultText.text = diceResult.ToString();

        diceResult += dicePenalty;
        
        int targetPosition = diceResult + currentTileIndex;

        if (targetPosition >= tiles.Length)
        {
            targetPosition = tiles.Length - 1;
        }

        MovePlayer(diceResult);

        currentTileIndex = targetPosition;
    }


    public void MovePlayer(int steps)
    {
        int targetTileIndex = Math.Min(currentTileIndex + steps, tiles.Length - 1);

        currentTileIndex = targetTileIndex;
        
        player.DOMove(tiles[currentTileIndex].position + new Vector3(0,player.position.y,0), 1f).SetEase(Ease.OutQuad);

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
