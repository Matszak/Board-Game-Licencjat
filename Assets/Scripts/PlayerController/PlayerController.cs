using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;


public class PlayerController : MonoBehaviour
{
 
 
    private PlayerMovement _playerMovement;
    private AdventureCardsChecker _adventureCardsChecker;
    [SerializeField] private DiceRoll diceRoll;

    private Player _player;

    private void OnEnable()
    {
        GameManager.Instance.TurnStarted += OnTurnStarted;
        DiceRoll.DiceRolled += OnDiceRolled;
        _playerMovement.OnEndMovePlayerMove += CheckIfOnCard;
        
    }

    private void CheckIfOnCard()
    {
        if (GameManager.Instance.currentPlayer != _player.ID) return;
        if (!_adventureCardsChecker.CheckIfStayOnCard(_player))
        {
            GameManager.Instance.NextTurn();
        }
    }

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _adventureCardsChecker = GetComponent<AdventureCardsChecker>();
    }
    
    public void InitializePlayer(Player player)
    {
        _player = player;
    }
    
    private void OnTurnStarted(GameManager.TurnStatedData data)
    {
        if (data.Player.PlayerObject == gameObject)
        {
            diceRoll.RequestDiceRoll();
        }
    }
    
    private void OnDiceRolled(int rollResult)
    {
        if (GameManager.Instance.currentPlayer != _player.ID) return;
        _playerMovement.MovePlayer(rollResult, _player);
    }

    private void OnDestroy()
    {
        GameManager.Instance.TurnStarted -= OnTurnStarted;
        DiceRoll.DiceRolled -= OnDiceRolled;
        _playerMovement.OnEndMovePlayerMove -= CheckIfOnCard;
    }
    

}

