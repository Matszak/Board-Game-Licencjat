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
    [FormerlySerializedAs("_playerMovement")] [SerializeField] private PlayerMovement playerMovement;
    [FormerlySerializedAs("checkIfOnCard")] [SerializeField] private AdventureCardsChecker adventureCardsChecker;
    [SerializeField] private DiceRoll diceRoll;

    private Player _player;

    private void OnEnable()
    {
        gameManager.TurnStarted += OnTurnStarted;
        DiceRoll.DiceRolled += OnDiceRolled;
   
        playerMovement.OnEndMovePlayerMove += CheckIfOnCard;
    }

    private void CheckIfOnCard()
    {
        if (gameManager.currentPlayer != _player.ID) return;
        if (!adventureCardsChecker.CheckIfStayOnCard(_player))
        {
            gameManager.NextTurn();
        }
 
    }


    private void Start()
    {
        gameManager = GameManager.Instance;
        
        //Jest już w on enabled, chyba działa.
        gameManager.TurnStarted += OnTurnStarted;
        playerMovement.OnEndMovePlayerMove += CheckIfOnCard;
        playerMovement = GetComponent<PlayerMovement>();
        adventureCardsChecker = GetComponent<AdventureCardsChecker>();
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
        playerMovement.MovePlayer(rollResult, _player);
    }
    

    private void OnDestroy()
    {
        gameManager.TurnStarted -= OnTurnStarted;
        DiceRoll.DiceRolled -= OnDiceRolled;
        playerMovement.OnEndMovePlayerMove -= CheckIfOnCard;
    }

 

}

