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
    [FormerlySerializedAs("_gameManager")] public GameManager gameManager;

    // zmiana nazwy na poprawną 
    private PlayerMovement _playerMovement;
    private AdventureCardsChecker _adventureCardsChecker;
    [SerializeField] private DiceRoll diceRoll;

    private Player _player;

    private void OnEnable()
    {
        gameManager.TurnStarted += OnTurnStarted;
        DiceRoll.DiceRolled += OnDiceRolled;
        _playerMovement.OnEndMovePlayerMove += CheckIfOnCard;
    }

    private void CheckIfOnCard()
    {
        if (gameManager.currentPlayer != _player.ID) return;
        if (!_adventureCardsChecker.CheckIfStayOnCard(_player))
        {
            gameManager.NextTurn();
        }
 
    }

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _adventureCardsChecker = GetComponent<AdventureCardsChecker>();
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        
    }

    public void InitializePlayer(Player player)
    {
        //zmienłem nazwe bo _ jest do prywatnych
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
        if (gameManager.currentPlayer != _player.ID) return;
        _playerMovement.MovePlayer(rollResult, _player);
    }
    

    private void OnDestroy()
    {
        gameManager.TurnStarted -= OnTurnStarted;
        DiceRoll.DiceRolled -= OnDiceRolled;
        _playerMovement.OnEndMovePlayerMove -= CheckIfOnCard;
    }

 

}

