using System;
using System.Collections;
using System.Collections.Generic;
using Cards;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] public List<Player> _players = new List<Player>();
 
    
    
    public int currentPlayer = 0;
    public Player currentPlayerObj;
    private int currentTurn = 0;
    private int avaialblePlayerIndex;
    
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

    public void Start()
    {
        TurnStarted?.Invoke(new TurnStatedData{Turn = currentTurn, Player = _players[currentPlayer]});
        
    }

    public event Action<TurnStatedData> TurnStarted;
    public event Action<Player, AdventureTile> OnCardTriggered;
    public event Action<Player> OnSelectedCardOn;
    public event Action<PlayerController> OnCardPlayerSelected;
    public event Action<Player> OnTurnEnded;
    
    public void TurnEnded(Player player)
    {
        OnTurnEnded?.Invoke(player);
    }

    public void PlayerIsSelected(PlayerController playerController)
    {
        OnCardPlayerSelected?.Invoke(playerController);
    }
    public void InvokeSelection(Player player)
    {
        OnSelectedCardOn?.Invoke(player);
    }
 
    
    public void CardTriggered(Player player, AdventureTile adventureTile)
    {   
        OnCardTriggered?.Invoke(player, adventureTile);
    }
    
    [ContextMenu("Next Turn")]
    public void NextTurn()  
    {
        currentPlayer++;
        
        if (currentPlayer >= _players.Count)
        {
            currentPlayer = 0;
            currentTurn++;
        }
        
        currentPlayerObj = _players[currentPlayer];
        TurnStarted?.Invoke(new TurnStatedData{Turn = currentTurn, Player = currentPlayerObj});
    }
    
    public class TurnStatedData
    {
        public int Turn;
        public Player Player;
    }

 
     
}

 