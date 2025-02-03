using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
 

public class PlayerController : MonoBehaviour
{
    public GameManager GameManager;
    [SerializeField] private PlayerMovement _playerMovement;
     
    [SerializeField] private DiceRoll diceRoll;
    private int rollResult = 0;
    
   
 
    
    private void OnEnable()
    {
        
        GameManager.TurnStarted += OnTurnStarted;
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
            int rollResult = 5;
            
            _playerMovement.MovePlayer(rollResult,data.Player);
        }
    }
    
    

    private void OnDestroy()
    {
        GameManager.TurnStarted -= OnTurnStarted;
    }

 

}

