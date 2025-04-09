using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject[] PlayerPrefab;
    public Transform[] PlayerSpawnPoints;
    
    public List<Player> playersList = new List<Player>();


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
            playersList.Add(player);
        }
    }
}
