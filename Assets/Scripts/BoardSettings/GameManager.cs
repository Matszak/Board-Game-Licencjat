using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private List<Player> _players = new List<Player>();
    
    public int currentPlayer = 0;

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
        NextTurn();
        
    }

    public event Action<TurnStatedData> TurnStarted;
    public event Action<Player> OnCardTriggered;

    public void CardTriggered(Player player)
    {
        OnCardTriggered?.Invoke(player);
    }
    
    [ContextMenu("Next Turn")]
    public void NextTurn()  
    {
        currentTurn++;
        currentPlayer++;
        
        if (currentPlayer >= _players.Count)
        {
            currentPlayer = 0;
        }
        
        TurnStarted?.Invoke(new TurnStatedData{Turn = currentTurn, Player = _players[currentPlayer]});
    }
    
    public class TurnStatedData
    {
        public int Turn;
        public Player Player;
    }
}

[Serializable]
public class Player
{
    public string Name;
    public GameObject PlayerObject;
    public int TileIndex;
 
}  