using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cards;
using Cards.EnemyCards;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] public List<Player> _players;

    [SerializeField] private GameObject tileParent;
    public Transform spawnPoint;
    public DiceRoll diceRoll;
    
    public int currentPlayer = 0;
    public int currentTurn = 0;
    public List<Player> _playersRank { get; private set; }
    
    public PlayerSpawner playerSpawner;

    [SerializeField] private TextMeshProUGUI cosnoleText;
    
    
    private void Awake()
    {
        if (Instance != null && Instance != this )
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        
   
    }

    private void Update()
    {
        {
            _playersRank = _playersRank.OrderByDescending(p => p.TileIndex).ToList();
            
        }
         
    }
    
    
    public void Start()
    {        
        DebugConsole.LogCentered("== Game Started ==");
        
        playerSpawner = FindObjectOfType<PlayerSpawner>();
        _players = playerSpawner.playersList;
        _playersRank = _players;
        if (_players.Count == 0)
            return;
        for (int i = 0; i < _players.Count; i++)
        {        
            GameObject gameObject = Instantiate(_players[i].PlayerObject, spawnPoint.position, Quaternion.identity);
            _players[i].PlayerObject = gameObject;    
    
            int count = tileParent.transform.childCount;
            _players[i].Movement.tiles = new Transform[count];
            for (int j= 0; j < count; j++)
            {
                GameObject tileObject = tileParent.transform.GetChild(j).gameObject;
                _players[i].Movement.tiles[j] = tileObject.transform;
            }
        }
        Player.SetPlayer(_players[0]);

        TurnStarted?.Invoke();
        
    }

    public event Action TurnStarted;
    public event Action OnTurnEnded;
    
    public event Action<Player, AdventureTile> OnCardTriggered;
    
    public event Action<Color> OnInvokeSelection;
    public event Action<Player> OnCardPlayerSelected;
    
    public event Action OnFightStarted;
    public event Action OnEnemyAttacksEnded;

    public event Action OnWinGame; 
    
    public void TurnEnded()
    {
        OnTurnEnded?.Invoke();
    }

    public void WinGame()
    {
        OnWinGame?.Invoke();
    }

    public void PlayerIsSelected(Player target)
    {
        OnCardPlayerSelected?.Invoke(target);
        foreach (var player in _players.Where(x => x != Player.CurrentPlayer && x.Selector.isActiveAndEnabled))
        {
            player.Selector.TurnSelectionOff();
        }
    }
    
    public void InvokeSelection(bool playersAhead)
    {
        //OnInvokeSelection?.Invoke(Color.red);
        foreach (var player in _players.Where(x => x != Player.CurrentPlayer && (!playersAhead || playersAhead && x.TileIndex > Player.CurrentPlayer.TileIndex)))
        {
            player.Selector.IsActive = true;
            player.Selector.TurnSelectionOn(Color.red);
        }
    }

    public void StartFight()
    {
        AudioManager.instance.PlayFightSound();
        OnFightStarted?.Invoke();
    }

    public void EndEnemyAttack()
    {
        OnEnemyAttacksEnded?.Invoke();
    }
    
    
    public void CardTriggered(Player player, AdventureTile adventureTile)
    {   
        OnCardTriggered?.Invoke(player, adventureTile);
    }
    
    [ContextMenu("Next Turn (No Bonus)")]
    public void NextTurnNoBonus()
    {
        NextTurn(false);
    }
    
    [ContextMenu("Next Turn (Bonus turn)")]
    public void NextTurnBonus()
    {
        NextTurn(true);
    }
    
    public void NextTurn(bool bonusTurn)  
    {
        if (!bonusTurn)
        {
            currentPlayer++;
        }
        
 
        if (currentPlayer >= _players.Count)
        {
            currentPlayer = 0;
            currentTurn++;
            
        }
 
        Player.SetPlayer(_players[currentPlayer]);
        TurnStarted?.Invoke();
    }
}

 