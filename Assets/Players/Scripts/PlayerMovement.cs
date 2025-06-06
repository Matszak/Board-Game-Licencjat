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

    public event Action OnEndMovePlayerMove;
    public Player Player { get; set; }

    public void MovePlayerBack(int steps)
    {
        Player.Controller.playerState = PlayerState.Walking;
        int targetTileIndex = Math.Max(Player.TileIndex - steps, 0);

        Sequence sequence = DOTween.Sequence();

        for (int i = Player.TileIndex - 1; i >= targetTileIndex; i--)
        {
            Vector3 movePosition = new Vector3(
                tiles[i].position.x,
                Player.PlayerObject.transform.position.y,
                tiles[i].position.z);
            sequence.AppendCallback(() => AudioManager.instance.PlayJumpSound());
            sequence.Append(Player.PlayerObject.transform.DOJump(movePosition, 6f, 1, 0.5f).SetEase(Ease.InOutSine));
        }

        sequence.OnComplete(() =>
        {
            Player.TileIndex = targetTileIndex;
            //GameManager.Instance.NextTurn();

            OnEndMovePlayerMove?.Invoke();
        });
        sequence.Play();
    }

    public void MovePlayer(int steps)
    {


        if (Player.Controller.playerState == PlayerState.Stunned)
        {
            Player.Controller.playerState = PlayerState.Stunned;
        }
        else
        {
            Player.Controller.playerState = PlayerState.Walking;
        }

        int targetTileIndex = Math.Min(Player.TileIndex + steps, tiles.Length - 1);

        Sequence sequence = DOTween.Sequence();

        for (int i = Player.TileIndex + 1; i <= targetTileIndex; i++)
        {
            Vector3 movePosition = new Vector3(
                tiles[i].position.x,
                Player.PlayerObject.transform.position.y,
                tiles[i].position.z);

            Collider[] playerOnTile = Physics.OverlapSphere(movePosition, 1f, playerLayer);

            Vector3 center = movePosition;
            float radius = 0.5f;

            for (int j = 0; j < playerOnTile.Length; j++)
            {
                float angle = j * Mathf.PI * 2f / playerOnTile.Length;
                Vector3 offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
                Vector3 targetPosition = center + offset;

                playerOnTile[j].transform.position = targetPosition;

            }
            sequence.AppendCallback(() => AudioManager.instance.PlayJumpSound());
            sequence.Append(Player.PlayerObject.transform.DOJump(movePosition, 6f, 0, 0.5f).SetEase(Ease.OutSine));

            int currentTileIndex = i;
            sequence.AppendCallback(() =>
            {
       
                if (IsEnemyOnTile(Player.PlayerObject.transform.position) && Player.Controller.playerState != PlayerState.FightWin)
                {
                    Player.Controller.playerState = PlayerState.FightStarted;
                    Player.TileIndex = currentTileIndex;
                    OnEndMovePlayerMove?.Invoke();
                    sequence.Kill();
                }


            });

        }
        sequence.OnComplete(() =>
        {
            Player.TileIndex = targetTileIndex;
            //GameManager.Instance.NextTurn();
            OnEndMovePlayerMove?.Invoke();
        });
        sequence.Play();

    }

    private bool IsEnemyOnTile(Vector3 playerPosition)
    {
        RaycastHit hit;
        if (Physics.Raycast(playerPosition, Vector3.down, out hit, Mathf.Infinity))
        {
            PlayerState playerState = Player.Controller.playerState;
            if (hit.collider.GetComponent<BattleTile>())
            {
                return true;
            }


        }
        return false;
    }
}