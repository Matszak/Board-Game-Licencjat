using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject[] PlayerPrefab;
    public Transform[] PlayerSpawnPoints;
    
    public List<Player> playersList;
    
    private void Start()
    {
        playersList = new List<Player>();
        
        int numberOfPlayers = PlayerPrefs.GetInt("NumberOfPlayers", 1);

        for (int i = 0; i < numberOfPlayers; i++)
        {
            GameObject player = Instantiate(PlayerPrefab[i], PlayerSpawnPoints[i].position, Quaternion.identity);
            playersList.Add(player.GetComponent<Player>());
        }
        GameManager.Instance._players = playersList;
    }
}
