using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance { get; private set; }

    public List<Player> players;
    [SerializeField] PlayerListObject playerListObject;
    public int currentPlayer = 0;
    private int currentTurn = 0;
    private int avaialblePlayerIndex;
    [SerializeField] private List<GameObject> playerSpawnPoint = new List<GameObject>();
    private void Awake()
    {
        if (Instance != null && Instance != this )
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        
        }
    }

    public void Start()
    {
        players = playerListObject.players; 
        for (int i = 0; i < players.Count; i++)
        {
            Instantiate(players[i].PlayerObject,playerSpawnPoint[i].transform.position,Quaternion.identity);
        }
        
        TurnStarted?.Invoke(new TurnStatedData{Turn = currentTurn, Player = players[currentPlayer]});
        
    }

    public event Action<TurnStatedData> TurnStarted;
    public event Action<Player> OnCardTriggered;
    public event Action<Player> OnTurnEnded;
    
    public void TurnEnded(Player player)
    {
        OnTurnEnded?.Invoke(player);
    }
    
    public void CardTriggered(Player player)
    {   
        OnCardTriggered?.Invoke(player);
    }
    
    [ContextMenu("Next Turn")]
    public void NextTurn()  
    {
        currentPlayer++;
        
        if (currentPlayer >= players.Count)
        {
            currentPlayer = 0;
            currentTurn++;
        }
        
        TurnStarted?.Invoke(new TurnStatedData{Turn = currentTurn, Player = players[currentPlayer]});
    }
    
    public class TurnStatedData
    {
        public int Turn;
        public Player Player;
    }
}

[System.Serializable]
public class Player
{
    public Player(string playerName, GameObject playerPrefab)
    {
        Name = playerName;
        PlayerObject = playerPrefab;
    }
    
    public string Name;
    public GameObject PlayerObject;
    public int TileIndex;
 
}  