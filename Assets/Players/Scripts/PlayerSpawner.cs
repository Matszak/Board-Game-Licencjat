using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{ 
    public List<Player> playersList = new List<Player>();
    
    Color[] _colors = new Color[] { Color.red, Color.green, Color.blue, Color.yellow };
    public GameObject[] playerPrefabs;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

 

    public void SpawnPlayer(int playerNumber)
    {
        for (int i = 0; i < playerNumber; i++)
        {
            Player player = new();
            player.Name = $"Player{i + 1}";

            player.PlayerObject = playerPrefabs[i];
            //player.PlayerObject.GetComponentInChildren<Renderer>().material.color =  UnityEngine.Random.ColorHSV(0f, 1f, 0.5f, 1f, 0.5f, 1f);
            playersList.Add(player);
        }
    }
}
