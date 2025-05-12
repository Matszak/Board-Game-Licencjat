using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cards;
using Cards.EnemyCards;

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
    public Player currentPlayerObj;
    [SerializeField] private int currentTurn = 0;
    private int avaialblePlayerIndex;
    public List<Player> _playersRank { get; private set; }
    [SerializeField] private PlayerState currentPlayerState;
    
    public PlayerSpawner playerSpawner;
    
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
            //currentPlayerState = currentPlayerObj.PlayerObject.GetComponent<PlayerController>().playerState;
        }
         
    }
    
    
    public void Start()
    {
        playerSpawner = FindObjectOfType<PlayerSpawner>();
        _players = playerSpawner.playersList;
        _playersRank = _players;
        for (int i = 0; i < _players.Count; i++)
        {
        
            GameObject gameObject = Instantiate(_players[i].PlayerObject, spawnPoint.position, Quaternion.identity);
            _players[i].PlayerObject = gameObject;
            PlayerController controller =  _players[i].PlayerObject.GetComponent<PlayerController>();
            if (controller != null)
            {
                controller.SetPlayer(_players[i]);  // Ensure correct player assignment
            }
    
    
            int count = tileParent.transform.childCount;
            gameObject.GetComponent<PlayerMovement>().tiles = new Transform[count];
            for (int j= 0; j < count; j++)
            {
                GameObject tileObject = tileParent.transform.GetChild(j).gameObject;
                gameObject.GetComponent<PlayerMovement>().tiles[j] = tileObject.transform;
            }
        }
 
        TurnStarted?.Invoke(new TurnStatedData{Turn = currentTurn, Player = _players[currentPlayer]});
        
    }

    public event Action<TurnStatedData> TurnStarted;
    public event Action<Player> OnTurnEnded;
    
    public event Action<Player, AdventureTile> OnCardTriggered;
    
    public event Action<Player> OnInvokeSelection;
    public event Action<Player> OnCardPlayerSelected;
    
    public event Action<Player, EnemyCard> OnFightStarted;
    public event Action<Player, EnemyCard> OnEnemyAttacksEnded;

    public event Action<Player> OnWinGame; 
    
    public void TurnEnded(Player player)
    {
        OnTurnEnded?.Invoke(player);
    }

    public void WinGame(Player player)
    {
        OnWinGame?.Invoke(player);
    }

    public void PlayerIsSelected(Player  selectedPlayer)
    {
        OnCardPlayerSelected?.Invoke(selectedPlayer);
    }
    
    public void InvokeSelection(Player player)
    {
        OnInvokeSelection?.Invoke(player);
    }

    public void StartFight(Player player, EnemyCard enemyCard)
    {
        OnFightStarted?.Invoke(player, enemyCard);
    }

    public void EndEnemyAttack(Player player, EnemyCard enemyCard)
    {
        OnEnemyAttacksEnded?.Invoke(player, enemyCard);
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
    
        currentPlayerObj = _players[currentPlayer];
       
        TurnStarted?.Invoke(new TurnStatedData
        {
            Turn = currentTurn, Player = currentPlayerObj, BonusTurn = bonusTurn
        });
    }
    
    public class TurnStatedData
    {
        public int Turn;
        public Player Player;
        public bool BonusTurn;
    }
 
 
}

 