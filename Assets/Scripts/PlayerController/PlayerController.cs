using System;
using System.Collections;
using System.Collections.Generic;
using CardsAndTilesScripts.adventureTiles;
using DefaultNamespace;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;


public enum PlayerState
{
    Walking,
    Fighting,
}

public class PlayerController : MonoBehaviour
{
 
    public Dice dice;
    private PlayerMovement _playerMovement;
    private AdventureCardsChecker _adventureCardsChecker;
    private PlayerSelector _playerSelector;
    [SerializeField] private DiceRoll diceRoll;
    public PlayerState playerState;
    public Player Player { get; private set; } 

    private void OnEnable()
    {
        playerState = PlayerState.Walking;
        GameManager.Instance.OnFightStarted += ChangeStateToFight;
        GameManager.Instance.TurnStarted += OnTurnStarted;
        DiceRoll.OnPlayerRolled += OnOnPlayerRolled;
        _playerMovement.OnEndMovePlayerMove += CheckIfOnCard;
 

    }

    private void ChangeStateToFight(Player player, Enemy enemy)
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
        playerState = PlayerState.Walking;
        diceRoll.RequestDiceRoll(Player);
     
    }
    
    private void OnOnPlayerRolled(int rollResult, Player player)
    {
        if (player != Player) return;
        if(playerState == PlayerState.Fighting) return;
        _playerMovement.MovePlayer(rollResult, Player);
    }

    private void OnDestroy()
    {
        GameManager.Instance.TurnStarted -= OnTurnStarted;
        DiceRoll.OnPlayerRolled -= OnOnPlayerRolled;
        _playerMovement.OnEndMovePlayerMove -= CheckIfOnCard;
    }

    public void SetPlayer(Player player)
    {
        Player = player;
    }

}

