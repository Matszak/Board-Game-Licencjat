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
 

    public Player Player { get; private set; } 

    private void OnEnable()
    {
        GameManager.Instance.TurnStarted += OnTurnStarted;
        DiceRoll.DiceRolled += OnDiceRolled;
        _playerMovement.OnEndMovePlayerMove += CheckIfOnCard;
        
    }
    
 

    private void CheckIfOnCard(Player player)
    {
        if(player != Player) return;
        if (!_adventureCardsChecker.CheckIfStayOnCard(Player))
        {
            GameManager.Instance.TurnEnded(Player);
        }

        AdventureTile adventureTile = _adventureCardsChecker.GetTile(Player);
        GameManager.Instance.CardTriggered(Player,adventureTile);
          
    }

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _adventureCardsChecker = GetComponent<AdventureCardsChecker>();
        _playerSelector = GetComponent<PlayerSelector>();
    }
 
    
    private void OnTurnStarted(GameManager.TurnStatedData data)
    {
        if (data.Player != Player) return;
        diceRoll.RequestDiceRoll(Player);
     
    }
    
    private void OnDiceRolled(int rollResult, Player player)
    {
        if (player != Player) return;
        _playerMovement.MovePlayer(rollResult, Player);
    }

    private void OnDestroy()
    {
        GameManager.Instance.TurnStarted -= OnTurnStarted;
        DiceRoll.DiceRolled -= OnDiceRolled;
        _playerMovement.OnEndMovePlayerMove -= CheckIfOnCard;
    }

    public void SetPlayer(Player player)
    {
        Player = player;
    }

}

