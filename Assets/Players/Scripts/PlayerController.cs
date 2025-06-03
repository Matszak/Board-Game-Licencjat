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
    [SerializeField] private int _minusRollValue;

    [SerializeField] private bool _bonusToRoll = false;
    [SerializeField] private int _bonusRollValue;
    
    [FormerlySerializedAs("_magicShield")] public bool magicShield = false;
    
    private void OnEnable()
    {
        playerState = PlayerState.None;
        FightSystem.EndEnemyFight += OnFightEnded;
        GameManager.Instance.OnFightStarted += ChangeStateToFight;
        GameManager.Instance.TurnStarted += OnTurnStarted;
        _playerMovement.OnEndMovePlayerMove += CheckIfOnCard;
        CardsOnStart.OnStartCardSelect += OnStartCardSelect;
 
    }

    private void OnStartCardSelect(Card magicCard, Player player)
    {
        if(player != CurrentPlayer) return;
        CurrentPlayer.playerCards.Add(magicCard);
        CardsOnStart.OnStartCardSelect -= OnStartCardSelect;
    }

    private void OnFightEnded(FightSystem.FightResult fightResult, Player fightingPlayer, EnemyCard enemyCard)
    {
        if(CurrentPlayer != fightingPlayer || enemyCard is BossCard) return;

        //Debug.Log($"player {fightingPlayer}, {fightResult}");
        switch (fightResult)
        {
            case FightSystem.FightResult.Win: 
                playerState = PlayerState.FightWin;
                DebugConsole.Log($"{CurrentPlayer.Name} Wins");
                enemyCard.enemyDefeatedBehaviour.EnemyDefeated(fightingPlayer);
                //CheckIfOnCard(fightingPlayer);
                break;
            case FightSystem.FightResult.Draw:
                DebugConsole.Log($"{CurrentPlayer.Name} Draw");
                enemyCard.enemyDrawBehaviour.EnemyDraw(fightingPlayer); 
                break;
            case FightSystem.FightResult.Lose:
                playerState = PlayerState.FightLose;
                DebugConsole.Log($"{CurrentPlayer.Name} Loses");
                enemyCard.enemyWinBehaviour.EnemyWin(fightingPlayer);
                GameManager.Instance.TurnEnded(CurrentPlayer);
                break;
        }
        
    }

    private void ChangeStateToFight(Player player, EnemyCard enemyCard)
    {
        if(player != CurrentPlayer) return;
        DebugConsole.Log($"{CurrentPlayer.Name} is attacked by {enemyCard.name}");
        playerState = PlayerState.FightStarted;
    }


    private void CheckIfOnCard(Player player)
    {   
        if(player != CurrentPlayer) return;
        recentPlayerState = playerState;
 
        if (!_adventureCardsChecker.CheckIfStayOnCard(CurrentPlayer) || playerState == PlayerState.Stunned)
        {
            if (playerState == PlayerState.Walking)
            {
                playerState = PlayerState.None;
            }
            if (playerState == PlayerState.Stunned)
            {
                GameManager.Instance.TurnEnded(CurrentPlayer);
            }
            else
            {
                playerState = PlayerState.None;
               
            }
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
            int totalMovement = CalculateModifiedRoll(result);
            DebugConsole.Log($"{CurrentPlayer.Name} rolled = {totalMovement}");

            if (totalMovement < 0)
            {
                _playerMovement.MovePlayerBack(Mathf.Abs(totalMovement), player);
            }
            else
            {
                _playerMovement.MovePlayer(totalMovement, player);
            }

            ResetModifiers();
        });
    }

    private int CalculateModifiedRoll(int baseResult)
    {
        int total = baseResult;

        if (_bonusToRoll)
        {
            total += _bonusRollValue;
        }

        if (minusToRoll)
        {
            total -= _minusRollValue;
        }

        return total;
    }

    private void ResetModifiers()
    {
        _bonusToRoll = false;
        minusToRoll = false;
        _bonusRollValue = 0;
        _minusRollValue = 0;
    }

     
 

    private void OnDisable()
    {
        GameManager.Instance.TurnStarted -= OnTurnStarted;
        _playerMovement.OnEndMovePlayerMove -= CheckIfOnCard;
        CardsOnStart.OnStartCardSelect -= OnStartCardSelect;
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

