using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] PlayerSpawner playerSpawner;
    
    public void ButtonOne()
    {
        playerSpawner.SpawnPlayer(1);
        SceneManager.LoadScene(1);
    }

    public void ButtonTwo()
    {
        playerSpawner.SpawnPlayer(2);
        SceneManager.LoadScene(1);
    }

    public void ButtonThree()
    {
        SceneManager.LoadScene(1);
    }

    public void ButtonFour()
    {
        playerSpawner.SpawnPlayer(4);
        SceneManager.LoadScene(1);
    }
}
