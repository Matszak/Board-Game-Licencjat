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

    private void CheckIfOnCard(Player player)
    {
        if(player != _player) return;
        if (!_adventureCardsChecker.CheckIfStayOnCard(_player))
        {
            GameManager.Instance.TurnEnded(_player);
        }
        
        GameManager.Instance.CardTriggered(_player);
         
    }

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _adventureCardsChecker = GetComponent<AdventureCardsChecker>();
    }
    
    private void OnTurnStarted(GameManager.TurnStatedData data)
    {
        if (data.Player.PlayerObject == gameObject)
        {
            _player = data.Player;
            diceRoll.RequestDiceRoll(data.Player);
            
        }
    }
    
    private void OnDiceRolled(int rollResult, Player player)
    {
         
        _playerMovement.MovePlayer(rollResult, player);
    }

    private void OnDestroy()
    {
        GameManager.Instance.TurnStarted -= OnTurnStarted;
        DiceRoll.DiceRolled -= OnDiceRolled;
        _playerMovement.OnEndMovePlayerMove -= CheckIfOnCard;
    }
    

}

