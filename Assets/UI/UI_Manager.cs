using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] PlayerSpawner playerSpawner;
    private int map = 1;

    public void PressChangeMap1()
    {
        map = 1;
    }

    public void PressChangeMap2()
    {
        map = 2;
    }
    
    public void ButtonOne()
    {
        playerSpawner.SpawnPlayer(2);
        SceneManager.LoadScene(map);
    }

    public void ButtonTwo()
    {
        playerSpawner.SpawnPlayer(2);
        SceneManager.LoadScene(map);
    }

    public void ButtonThree()
    {
        playerSpawner.SpawnPlayer(3);
        SceneManager.LoadScene(map);
    }

    public void ButtonFour()
    {
        playerSpawner.SpawnPlayer(4);
        SceneManager.LoadScene(map);
    }
}
