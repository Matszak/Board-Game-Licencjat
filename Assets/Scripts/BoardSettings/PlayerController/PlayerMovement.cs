using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement Controller")]
    public Transform[] tiles;
    
     
    private int dicePenalty = 0;
    

    public void MovePlayer(int steps, Player player)
    {
        int targetTileIndex = Math.Min(player.TileIndex + steps, tiles.Length - 1);

        
        Sequence sequence = DOTween.Sequence();
        
        
        for (int i = player.TileIndex; i <= targetTileIndex; i++)
        {
            Vector3 movePosition = new Vector3(
                tiles[i].position.x,
                player.PlayerObcject.transform.position.y,
                tiles[i].position.z);

            sequence.Append(player.PlayerObcject.transform.DOJump(movePosition, 6f, 1, 0.5f).SetEase(Ease.OutQuad));
        }
        sequence.OnComplete(() => player.TileIndex = targetTileIndex);
        sequence.Play();
    }
    
}
