using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] private GameObject createOnePlayerButton;
    [SerializeField] private GameObject createTwoPlayersButton;
    [SerializeField] private GameObject createThreePlayersButton;
    [SerializeField] private GameObject createFourPlayers;

    [SerializeField] private GameObject defaultPawn;
   // [SerializeField] private GameObject showCreatePlayerWindow;
    private List<Player> _players;
    private int _numberOfPlayers;
    
    [SerializeField] private PlayerListObject playerListObject;
    
 
    public void ButtonOne()
    {
        createOnePlayerButton.SetActive(false);
        _numberOfPlayers = 1;
        _players = CreatePlayers(_numberOfPlayers);
        playerListObject.players = _players; 
        SceneManager.LoadScene("SampleScene");
    }

    public void ButtonTwo()
    {
        createTwoPlayersButton.SetActive(true);
        _numberOfPlayers = 2;
        _players = CreatePlayers(_numberOfPlayers);
        playerListObject.players = _players; 
        SceneManager.LoadScene("SampleScene");
    }
    

    public void ButtonThree()
    {
        createThreePlayersButton.SetActive(true);
        _numberOfPlayers = 3;
        CreatePlayers(_numberOfPlayers);
    }

    public void ButtonFour()
    {
        createFourPlayers.SetActive(true);
        _numberOfPlayers = 4;
        CreatePlayers(_numberOfPlayers);
    }

    private List<Player> CreatePlayers(int numberOfPlayers)
    {
        _players = new List<Player>();
        for (int i = 0; i < numberOfPlayers ; i++)
        {
            Player player = new Player("Player" + i + 1 ,defaultPawn);
           _players.Add(player);
        }
        
        return _players;
    }
 
}
