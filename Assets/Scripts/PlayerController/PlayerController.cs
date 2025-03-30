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
 
    public Dice dice;
    private PlayerMovement _playerMovement;
    private AdventureCardsChecker _adventureCardsChecker;
    private PlayerSelector _playerSelector;
    [SerializeField] private DiceRoll diceRoll;
 

    public Player _player;

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

        AdventureTile adventureTile = _adventureCardsChecker.GetTile(_player);
        GameManager.Instance.CardTriggered(_player,adventureTile);
          
    }

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _adventureCardsChecker = GetComponent<AdventureCardsChecker>();
        _playerSelector = GetComponent<PlayerSelector>();
    }
 
    
    private void OnTurnStarted(GameManager.TurnStatedData data)
    {
        if (data.Player.PlayerObject == gameObject)
        {
            _player = data.Player;
            diceRoll.RequestDiceRoll(_player);
            
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

