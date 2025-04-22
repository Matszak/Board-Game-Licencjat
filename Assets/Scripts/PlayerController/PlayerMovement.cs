using System;
using System.Collections;
using System.Collections.Generic;
using CardsAndTilesScripts.adventureTiles;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using Sequence = DG.Tweening.Sequence;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement Controller")]
    public Transform[] tiles;
    
    public event Action<Player> OnEndMovePlayerMove;
   // private int dicePenalty = 0;

   public void MovePlayerBack(int steps, Player player)
   {
       int targetTileIndex = Math.Max(player.TileIndex - steps, 0);
        
       Sequence sequence = DOTween.Sequence();
        
       for (int i = player.TileIndex; i >= targetTileIndex; i--)
       {
           Vector3 movePosition = new Vector3(
               tiles[i].position.x,
               player.PlayerObject.transform.position.y,
               tiles[i].position.z);

           sequence.Append(player.PlayerObject.transform.DOJump(movePosition, 6f, 1, 0.5f).SetEase(Ease.OutQuad));
       }

       sequence.OnComplete(() =>
       {
           player.TileIndex = targetTileIndex;
           //GameManager.Instance.NextTurn();
           OnEndMovePlayerMove?.Invoke(player);
       });
       sequence.Play();
   }
   
    public void MovePlayer(int steps, Player player)
    {
        int targetTileIndex = Math.Min(player.TileIndex + steps, tiles.Length - 1);
        
        Sequence sequence = DOTween.Sequence();
        
        for (int i = player.TileIndex; i <= targetTileIndex; i++)
        {
            Vector3 movePosition = new Vector3(
                tiles[i].position.x,
                player.PlayerObject.transform.position.y,
                tiles[i].position.z);
            sequence.Append(player.PlayerObject.transform.DOJump(movePosition, 6f, 1, 0.5f).SetEase(Ease.OutQuad));

            int currentTileIndex = i;
            sequence.AppendCallback(() =>
            {
                if (IsEnemyOnTile(player.PlayerObject.transform.position) && player.PlayerObject.GetComponent<PlayerController>().playerState != PlayerState.Fighting)
                {
                    player.TileIndex = currentTileIndex;
                    OnEndMovePlayerMove?.Invoke(player);
                    sequence.Kill();
                }
            });
        }
        

        sequence.OnComplete(() =>
        {
            player.TileIndex = targetTileIndex;
            //GameManager.Instance.NextTurn();
            OnEndMovePlayerMove?.Invoke(player);
        });
        sequence.Play();
 
    }

    private bool IsEnemyOnTile(Vector3 playerPosition)
    {
        RaycastHit hit;
        if (Physics.Raycast(playerPosition, Vector3.down, out hit, Mathf.Infinity))
        {
            if (hit.collider.GetComponent<BattleTile>())
            {
                return true;
            }
        }
        return false;
    }
}