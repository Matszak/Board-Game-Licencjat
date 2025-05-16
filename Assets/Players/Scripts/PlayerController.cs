using System;
using System.Collections;
using System.Collections.Generic;
using Cards.EnemyCards;
using CardsAndTilesScripts.adventureTiles;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;


public enum PlayerState
{
    None,
    Walking,
    CardPickedUp,
    FightStarted,
    Fighting,
    FightEnded,
    FightWin,
    FightLose,
    Stunned,
}

public class PlayerController : MonoBehaviour
{
    private static readonly int OutLineBool = Shader.PropertyToID("_TurnOn");
    private static readonly int OutLineColor = Shader.PropertyToID("_outLineColor");
 
    
    private PlayerMovement _playerMovement;
    private AdventureCardsChecker _adventureCardsChecker;
    private PlayerSelector _playerSelector;
    [SerializeField] private DiceRoll diceRoll;
    
    public PlayerState playerState;
 
    
    public Player CurrentPlayer { get; private set; }
    
    
    
    private void OnEnable()
    {
        playerState = PlayerState.None;
        FightSystem.EndEnemyFight += OnFightEnded;
        GameManager.Instance.OnFightStarted += ChangeStateToFight;
        GameManager.Instance.TurnStarted += OnTurnStarted;
        _playerMovement.OnEndMovePlayerMove += CheckIfOnCard;
 
    }
 
    private void OnFightEnded(bool win, Player fightingPlayer, EnemyCard enemyCard)
    {
        if(CurrentPlayer != fightingPlayer || enemyCard is BossCard) return;
        Debug.Log($"player {fightingPlayer}, {win}");
         
        if (win)
        {
            playerState = PlayerState.FightWin;
            enemyCard.enemyDefeatedBehaviour.EnemyDefeated(fightingPlayer);
            //CheckIfOnCard(fightingPlayer);
        }
        else
        {
            playerState = PlayerState.FightLose;
            GameManager.Instance.TurnEnded(CurrentPlayer);
        }
    }

    private void ChangeStateToFight(Player player, EnemyCard enemyCard)
    {
        if(player != CurrentPlayer) return;
       playerState = PlayerState.FightStarted;
    }


    private void CheckIfOnCard(Player player)
    {   
        if(player != CurrentPlayer) return;
        //if(playerState is  PlayerState.FightLose or PlayerState.FightStarted) return;
        if (!_adventureCardsChecker.CheckIfStayOnCard(CurrentPlayer))
        {
            GameManager.Instance.TurnEnded(CurrentPlayer);
        }
        else
        {
            
            AdventureTile adventureTile = _adventureCardsChecker.GetTile(CurrentPlayer);
            GameManager.Instance.CardTriggered(CurrentPlayer,adventureTile);
            
        }

    }

    [SerializeField] private int stunnedFor;
    [ContextMenu("StunPlayer")]
    public void StunPlayer(int numberOfTurns, Player player)
    {
        if(player != CurrentPlayer) return;
        playerState = PlayerState.Stunned;
        stunnedFor = numberOfTurns;
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
        
        Material[] materials = gameObject.GetComponentInChildren<Renderer>().materials;
        materials[1].SetFloat(OutLineBool, 0);
        materials[1].SetColor(OutLineColor, Color.white);
        
        if (data.Player != CurrentPlayer) return;
        if (data.Player.currentEnemyCard == null && playerState is PlayerState.None or PlayerState.FightWin or PlayerState.CardPickedUp)
        {
            playerState = PlayerState.Walking;       
        }
        else if(playerState == PlayerState.FightLose)
        {
            data.Player.currentEnemyCard.TriggerCard(CurrentPlayer);
        }
        
        materials[1].SetColor(OutLineColor, Color.white);
        materials[1].SetFloat(OutLineBool, 1);
        
        switch (playerState)
        {
            case PlayerState.None:
                break;
            case PlayerState.FightStarted:
                data.Player.currentEnemyCard.TriggerCard(data.Player);
                break;
            case PlayerState.Walking:
                MovePlayer(data.Player);
                break;
            case PlayerState.Stunned:
                _playerMovement.MovePlayer(0,data.Player);
                if (stunnedFor > 0)
                {
                    stunnedFor--;
                }
                else
                {
                    playerState = PlayerState.Walking;
                }
                //GameManager.Instance.TurnEnded(CurrentPlayer);
                break;
        }
    }

    public void MovePlayer(Player player)
    {
        if (playerState == PlayerState.None) return;
        
        diceRoll.RequestDiceRoll(false, result =>
        {
            DebugConsole.Log($"{CurrentPlayer.Name} rolled = {result}");
            _playerMovement.MovePlayer(result, player);
        });
    
    }

    private void OnDisable()
    {
        GameManager.Instance.TurnStarted -= OnTurnStarted;
        _playerMovement.OnEndMovePlayerMove -= CheckIfOnCard;
    }

    public int Attack(int rollResult)
    {
        return rollResult;
    }

    public void SetPlayer(Player player)
    {
        CurrentPlayer = player;
    }
}

