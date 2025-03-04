using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] private GameObject onePlayer;
    [SerializeField] private GameObject twoPlayers;
    [SerializeField] private GameObject threePlayers;
    [SerializeField] private GameObject fourPlayers;
    
    
    private int _numberOfPlayers;


    private void NewPlayer()
    {
        Player one = new Player("Mateusz", onePlayer);
        GameManager.Instance._players.Add(one);
    }
    
    public void ButtonOne()
    {
        NewPlayer();
        onePlayer.SetActive(true);
        _numberOfPlayers = 1;
        SceneManager.LoadScene(1);
    }

    public void ButtonTwo()
    {
        twoPlayers.SetActive(true);
        _numberOfPlayers = 2;
        SceneManager.LoadScene(1);
    }

    public void ButtonThree()
    {
        threePlayers.SetActive(true);
        SceneManager.LoadScene(1);
    }

    public void ButtonFour()
    {
        fourPlayers.SetActive(true);
        SceneManager.LoadScene(1);
    }
}
