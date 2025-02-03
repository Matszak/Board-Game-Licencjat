using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement Controller")]
    public Transform[] tiles;
    public Transform player;
    
    private int currentTileIndex = 0;
    private int dicePenalty = 0;

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

            sequence.Append(player.DOJump(movePosition, 6f, 1, 0.5f).SetEase(Ease.OutQuad));
        }

        sequence.Play();
        currentTileIndex = targetTileIndex;
    }
}
