using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] PlayerSpawner playerSpawner;
    
    public GameObject mainMenuPanel;
    public GameObject playerMenuPanel;
    public GameObject settingsPanel;
    public GameObject creditsPanel;
    public GameObject close;

    
    
    public void StartGame()
    {
        playerMenuPanel.SetActive(true);
        settingsPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }



    public void settingsButton()
    {
        settingsPanel.SetActive(true);
        creditsPanel.SetActive(false);
        playerMenuPanel.SetActive(false);
    }

    public void CreditsButton()
    {
        creditsPanel.SetActive(true);
        playerMenuPanel.SetActive(false);
        settingsPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void closeButton()
    {
        playerMenuPanel.SetActive(false);
        creditsPanel.SetActive(false);
        settingsPanel.SetActive(false);

    }
    

    public void ButtonTwo()
    {
        playerSpawner.SpawnPlayer(2);
        LoadingSceneManager.sceneToLoad = "SampleScene";
        SceneManager.LoadScene("LoadingScreen");
    }

    public void ButtonThree()
    {
        playerSpawner.SpawnPlayer(3);
        LoadingSceneManager.sceneToLoad = "SampleScene";
        SceneManager.LoadScene("LoadingScreen");
    }

    public void ButtonFour()
    {
        playerSpawner.SpawnPlayer(4);
        LoadingSceneManager.sceneToLoad = "SampleScene";
        SceneManager.LoadScene("LoadingScreen");
    }
}
