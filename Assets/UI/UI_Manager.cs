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

 
    
    public void ButtonOne()
    {
        PlayerPrefs.SetInt("NumberOfPlayers", 1);
        SceneManager.LoadScene(1);
    }

    public void ButtonTwo()
    {
        PlayerPrefs.SetInt("NumberOfPlayers", 2);
        SceneManager.LoadScene(1);
    }

    public void ButtonThree()
    {
        SceneManager.LoadScene(1);
    }

    public void ButtonFour()
    {
        SceneManager.LoadScene(1);
    }
}
