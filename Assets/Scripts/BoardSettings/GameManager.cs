using System;
using System.Collections;
using System.Collections.Generic;
using Cards;
using DefaultNamespace;
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
        foreach (var player in _players)
        {
            PlayerController controller = player.PlayerObject.GetComponent<PlayerController>();
            if (controller != null)
            {
                controller.SetPlayer(player);  // Ensure correct player assignment
            }
        }
        TurnStarted?.Invoke(new TurnStatedData{Turn = currentTurn, Player = _players[currentPlayer]});
        
    }

    public event Action<TurnStatedData> TurnStarted;
    public event Action<Player> OnTurnEnded;
    
    public event Action<Player, AdventureTile> OnCardTriggered;
    
    public event Action<Player> OnInvokeSelection;
    public event Action<Player> OnCardPlayerSelected;
    
    public event Action<Player, Enemy> OnFightStarted;
    
    public void TurnEnded(Player player)
    {
        OnTurnEnded?.Invoke(player);
    }

    public void PlayerIsSelected(Player  selectedPlayer)
    {
        OnCardPlayerSelected?.Invoke(selectedPlayer);
    }
    
    public void InvokeSelection(Player player)
    {
        OnInvokeSelection?.Invoke(player);
    }

    public void StartFight(Player player, Enemy enemy)
    {
        OnFightStarted?.Invoke(player, enemy);
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

 