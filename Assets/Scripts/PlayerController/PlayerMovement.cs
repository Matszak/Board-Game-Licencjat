using System;
using System.Collections;
using System.Collections.Generic;
using Cards.EnemyCards;
using CardsAndTilesScripts.adventureTiles;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using Sequence = DG.Tweening.Sequence;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement Controller")]
    public Transform[] tiles;
    
    public LayerMask playerLayer;

    public event Action<Player> OnEndMovePlayerMove;
   // private int dicePenalty = 0;
   

   public void MovePlayerBack(int steps, Player player)
   {
       _player = player;
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
        _player = player;
        int targetTileIndex = Math.Min(player.TileIndex + steps, tiles.Length - 1);
        
        Sequence sequence = DOTween.Sequence();
        
        for (int i = player.TileIndex; i <= targetTileIndex; i++)
        {
            Vector3 movePosition = new Vector3(
                tiles[i].position.x,
                player.PlayerObject.transform.position.y,
                tiles[i].position.z);
            
            Collider[] playerOnTile = Physics.OverlapSphere(movePosition, 1f, playerLayer);

            for (int j = 0; j < playerOnTile.Length; j++)
            {
                    Rigidbody rb = playerOnTile[j].GetComponent<Rigidbody>();
                    rb.isKinematic = true;
                    Vector3 offset = movePosition + Vector3.right * 100f * j;
                    playerOnTile[j].transform.position = offset;
                    rb.isKinematic = false;
                    Debug.Log("Wykryto graczy: " + playerOnTile.Length);
            }
            
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
            if (hit.collider.GetComponent<BattleTile>() && _player.PlayerObject.gameObject.GetComponent<PlayerController>().playerState != PlayerState.Fighting)
            {
                return true;
            }
        }
        return false;
    }
}