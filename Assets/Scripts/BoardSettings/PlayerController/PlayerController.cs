using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
 

public class PlayerController : MonoBehaviour
{
    public GameManager GameManager;

    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private DiceRoll diceRoll;

    private Player player;

    private void OnEnable()
    {
        GameManager.TurnStarted += OnTurnStarted;
        DiceRoll.DiceRolled += OnDiceRolled;
    }

 
    private void Start()
    {
        GameManager = GameManager.Instance;
        GameManager.TurnStarted += OnTurnStarted;
        _playerMovement = GetComponent<PlayerMovement>();
    }

    public void InitializePlayer(Player _player)
    {
        player = _player;
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
        if (GameManager.currentPlayer != player.ID) return;
        _playerMovement.MovePlayer(rollResult, player);
    }
    

    private void OnDestroy()
    {
        GameManager.TurnStarted -= OnTurnStarted;
        DiceRoll.DiceRolled -= OnDiceRolled;
    }

 

}

