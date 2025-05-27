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
    public PlayerState recentPlayerState;
    
    public Player CurrentPlayer { get; private set; }

    // for minus dice roll
    [SerializeField] private bool minusToRoll = false;
    private int _minusRollValue;

    [SerializeField] private bool _bonusToRoll = false;
    private int _bonusRollValue;
    
    [FormerlySerializedAs("_magicShield")] public bool magicShield = false;
    
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
        recentPlayerState = playerState;
 
        if (!_adventureCardsChecker.CheckIfStayOnCard(CurrentPlayer) || playerState == PlayerState.Stunned)
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
        
        materials[1].SetColor(OutLineColor, Color.white);
        materials[1].SetFloat(OutLineBool, 1);

        
        switch (playerState)
        {
            case PlayerState.None:
                MovePlayer(data.Player);
                break;
            case PlayerState.FightStarted:
                data.Player.currentEnemyCard.TriggerCard(data.Player);
                break;
            case PlayerState.FightLose:
                data.Player.currentEnemyCard.TriggerCard(CurrentPlayer);
                break;
            case PlayerState.FightWin:
            case PlayerState.Walking:
            case PlayerState.CardPickedUp:
                MovePlayer(data.Player);
                break;
            case PlayerState.Stunned:
                if (stunnedFor > 0)
                {
                    stunnedFor--;
                    _playerMovement.MovePlayer(0,data.Player);
                }
                else
                {
                    MovePlayer(data.Player);
                }
                break;
        }
         
    }

    public void MovePlayer(Player player)
    {
        playerState = PlayerState.Walking;
        diceRoll.RequestDiceRoll(false, result =>
        {
            if (minusToRoll && _bonusToRoll)
            {
                var resultBoth = _bonusRollValue + _minusRollValue;
                DebugConsole.Log($"{CurrentPlayer.Name} rolled = {result}");
                switch (resultBoth)
                {
                    case > 0:
                        _playerMovement.MovePlayer(resultBoth, player);
                        minusToRoll = false;
                        _bonusToRoll = false;
                        _minusRollValue = 0;
                        _bonusRollValue = 0;
                      
                        return;
                    case < 0:
                        _playerMovement.MovePlayerBack(Mathf.Abs(resultBoth), player);
                        minusToRoll = false;
                        _bonusToRoll = false;
                        _minusRollValue = 0;
                        _bonusRollValue = 0;
                       
                        return;
                }

                resultBoth = 0;
            }
            else if(_bonusToRoll)
            {
                 int bonusResult = result + _bonusRollValue;
                DebugConsole.Log($"{CurrentPlayer.Name} rolled = {bonusResult}");
                
                _playerMovement.MovePlayer(bonusResult, player);
                
                if (bonusResult < 0)
                {
                    _playerMovement.MovePlayerBack(Mathf.Abs(result), player);
                }
                _bonusToRoll = false;
                _bonusRollValue = 0;
                bonusResult = 0;

            }
            else if(minusToRoll)
            {
                var minusResult = result + _minusRollValue;
                DebugConsole.Log($"{CurrentPlayer.Name} rolled = {minusResult}");
                if (minusResult < 0)
                {
                    _playerMovement.MovePlayerBack(Mathf.Abs(minusResult), player);
                }
                minusToRoll = false;
                _minusRollValue = 0;
                minusResult = 0;
            }
            else
            {
                _playerMovement.MovePlayer(result,player);
            }
            
             

           
        });
    
    }

    private void OnDisable()
    {
        GameManager.Instance.TurnStarted -= OnTurnStarted;
        _playerMovement.OnEndMovePlayerMove -= CheckIfOnCard;
    }

    public int Attack(int rollResult)
    {
        int attackValue = rollResult;
        if (_bonusToRoll)
        {
            attackValue = rollResult + _bonusRollValue;
            _bonusToRoll = false;
            _bonusRollValue = 0;
            
        }
        else if(minusToRoll)
        {
            attackValue = rollResult - _minusRollValue;
            minusToRoll = false;
            _minusRollValue = 0;
        }

        return attackValue;
    }

    public void SetPlayer(Player player)
    {
        CurrentPlayer = player;
    }

    public void SetMinusDiceRoll(int minusDiceRoll)
    {
        minusToRoll = true;
        _minusRollValue = minusDiceRoll;
    }

    public void SetBonusDiceRoll(int bonusDiceRoll)
    {
        _bonusToRoll = true;
        _bonusRollValue = bonusDiceRoll;
    }

    public void SetMagicShield(bool magicShield)
    {
        this.magicShield = magicShield;
    }
}

