using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
 

public class PlayerController : MonoBehaviour
{
    public DiceRoll DiceRoll;

    public Player player;
    public GameManager GameManager;
    [SerializeField] private PlayerMovement _playerMovement;
     
    [SerializeField] private DiceRoll diceRoll;


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
    
    private void OnTurnStarted(GameManager.TurnStatedData data)
    {
        if (data.Player.PlayerObcject == gameObject)
        {
            diceRoll.RequestDiceRoll();
        }
    }
    
    private void OnDiceRolled(int rollResult)
    {
        _playerMovement.MovePlayer(rollResult, player);
    }
    
    

    private void OnDestroy()
    {
        GameManager.TurnStarted -= OnTurnStarted;
        DiceRoll.DiceRolled -= OnDiceRolled;
    }

 

}

