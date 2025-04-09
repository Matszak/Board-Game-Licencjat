using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{ 
    public List<Player> playersList = new List<Player>();
    
    public GameObject playerPrefab;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        
    }

    public void SpawnPlayer(int playerNumber)
    {
        for (int i = 0; i < playerNumber; i++)
        {
            Player player = new();
            player.Name = $"Player{i}";
            player.PlayerObject = playerPrefab;
            playersList.Add(player);
        }
    }
}
