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
    Walking,
    Fighting
}

public class PlayerController : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private AdventureCardsChecker _adventureCardsChecker;
    private PlayerSelector _playerSelector;
    [SerializeField] private DiceRoll diceRoll;
    public PlayerState playerState;
    public Player Player { get; private set; } 

    private void OnEnable()
    {
        FightSystem.endFight += FightSystemOnendFight;
        playerState = PlayerState.Walking;
        GameManager.Instance.OnFightStarted += ChangeStateToFight;
        GameManager.Instance.TurnStarted += OnTurnStarted;
        _playerMovement.OnEndMovePlayerMove += CheckIfOnCard;
    }

    private void FightSystemOnendFight(bool win, Player fightingPlayer)
    {
        if(Player != fightingPlayer) return;
        Debug.Log(win);
        if (win)
        {
            playerState = PlayerState.Walking;
            GameManager.Instance.TurnEnded(Player);
        }
        else
        {
            playerState = PlayerState.Fighting;
            GameManager.Instance.TurnEnded(Player);
        }
    }

    private void ChangeStateToFight(Player player, EnemyCard enemyCard)
    {
        if(player != Player) return;
       playerState = PlayerState.Fighting;
    }


    private void CheckIfOnCard(Player player)
    {
        if(player != Player) return;
        if(playerState == PlayerState.Fighting) return;
        
        if (!_adventureCardsChecker.CheckIfStayOnCard(Player))
        {
            GameManager.Instance.TurnEnded(Player);
        }

        AdventureTile adventureTile = _adventureCardsChecker.GetTile(Player);
        GameManager.Instance.CardTriggered(Player,adventureTile);
    }

    private void Awake()
    {
        diceRoll = FindObjectOfType<DiceRoll>();
        _playerMovement = GetComponent<PlayerMovement>();
        _adventureCardsChecker = GetComponent<AdventureCardsChecker>();
        _playerSelector = GetComponent<PlayerSelector>();
    }
    
    private void OnTurnStarted(GameManager.TurnStatedData data)
    {
        if (data.Player != Player) return;
        
        switch (playerState)
        {
            case PlayerState.Fighting:
                data.Player.currentEnemyCard.TriggerCard(data.Player);
                break;
            case PlayerState.Walking:
                MovePlayer(data.Player);
                break;
        }
    }

    public void MovePlayer(Player player)
    {
       diceRoll.RequestDiceRoll(false, result =>
       {
           _playerMovement.MovePlayer(result, player);
       });
    }

    private void OnDestroy()
    {
        GameManager.Instance.TurnStarted -= OnTurnStarted;
        _playerMovement.OnEndMovePlayerMove -= CheckIfOnCard;
    }

    public int Attack(int rollResult)
    {
        return rollResult;
    }

    public void SetPlayer(Player player)
    {
        Player = player;
    }
}

