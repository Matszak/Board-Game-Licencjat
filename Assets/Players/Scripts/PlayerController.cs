using System;
using System.Collections;
using System.Collections.Generic;
using Cards.EnemyCards;
using CardsAndTilesScripts.adventureTiles;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;


public enum PlayerState
{
    None,
    Walking,
    Fighting,
    Stunned,
    EndTurn,
}

public class PlayerController : MonoBehaviour
{
    public Player CurrentPlayer { get; private set; }

    private PlayerMovement _playerMovement;
    private AdventureCardsChecker _adventureCardsChecker;
    private PlayerSelector _playerSelector;
    [SerializeField] private DiceRoll diceRoll;
    
    public PlayerState playerState;
    
    
    private void OnEnable()
    {
        playerState = PlayerState.None;
        FightSystem.EndEnemyFight += OnFightEnded;
        GameManager.Instance.OnFightStarted += ChangeStateToFight;
        GameManager.Instance.TurnStarted += OnTurnStarted;
        _playerMovement.OnEndMovePlayerMove += CheckIfOnCard;
    }
 
    private void Awake()
    {
        diceRoll = FindObjectOfType<DiceRoll>();
        _playerMovement = GetComponent<PlayerMovement>();
        _adventureCardsChecker = GetComponent<AdventureCardsChecker>();
        _playerSelector = GetComponent<PlayerSelector>();
    }
    private void OnFightEnded(bool win, Player fightingPlayer, EnemyCard enemyCard)
    {
        if(CurrentPlayer != fightingPlayer || enemyCard is BossCard) return;
        
        if (win)
        {
            enemyCard.enemyDefeatedBehaviour.EnemyDefeated(fightingPlayer);
            Destroy(enemyCard);
            ChangePlayerState(CheckForPlayerState(CurrentPlayer));
        }
        else
        {
            playerState = PlayerState.Fighting;
            GameManager.Instance.TurnEnded(CurrentPlayer);
        }
    }

    private void ChangeStateToFight(Player player, EnemyCard enemyCard)
    {
        if(player != CurrentPlayer) return;
       playerState = PlayerState.Fighting;
    }


 
    
    private void OnTurnStarted(GameManager.TurnStatedData data)
    {
        if (data.Player != CurrentPlayer) return;
        
        ChangePlayerState(CheckForPlayerState(CurrentPlayer));
    }

    public void ChangePlayerState(PlayerState playerState)
    {
        switch (playerState)
        {
            case PlayerState.None:
                CheckForPlayerState(CurrentPlayer);
                break;
            case PlayerState.Fighting:
                TriggerCurrentEnemy();
                break;
            case PlayerState.Walking:
                MovePlayer();
                break;
            case PlayerState.Stunned:
                PlayerStun();
                //GameManager.Instance.TurnEnded(CurrentPlayer);
                break;
            case PlayerState.EndTurn:
                GameManager.Instance.TurnEnded(CurrentPlayer);
                break;
        }
    }

    public PlayerState CheckForPlayerState(Player player)
    {
  
        if (_adventureCardsChecker.GetTile(CurrentPlayer) is BattleTile )
        {
                playerState = PlayerState.Fighting;
        }
        else
        {
            playerState = PlayerState.EndTurn;
        }
        
        return playerState;
    }
    [SerializeField] private int stunnedFor;
    [ContextMenu("StunPlayer")]
    public void StunPlayer(int numberOfTurns, Player player)
    {
        if(player != CurrentPlayer) return;
        playerState = PlayerState.Stunned;
        stunnedFor = numberOfTurns;
    }

    public void PlayerStun()
    {
        _playerMovement.MovePlayer(0, CurrentPlayer);
        if (stunnedFor > 0)
        {
            stunnedFor--;
        }
        else
        {
            playerState = PlayerState.Walking;
        }
    }

    public void TriggerCurrentEnemy()
    {
        CurrentPlayer.currentEnemyCard.TriggerCard(CurrentPlayer);
    }
    
    public void MovePlayer()
    {
        if (playerState == PlayerState.None) return;
        
        diceRoll.RequestDiceRoll(false, result =>
        {
            _playerMovement.MovePlayer(result, CurrentPlayer);
        });
    }
    
    public int Attack(int rollResult)
    {
        return rollResult;
    }

    public void SetPlayer(Player player)
    {
        CurrentPlayer = player;
    }
    
    


    private void OnDisable()
    {
        GameManager.Instance.TurnStarted -= OnTurnStarted;
        _playerMovement.OnEndMovePlayerMove -= CheckIfOnCard;
    }

}

