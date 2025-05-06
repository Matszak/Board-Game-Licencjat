using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuPanel;
    public GameObject resumeButton;
    public GameObject backButton;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            pauseMenuPanel.SetActive(true);
        }
    }
    
    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenuPanel.SetActive(true);
        resumeButton.SetActive(false);
    }
    
    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenuPanel.SetActive(false);
        resumeButton.SetActive(true);
    }
    
    public void BackToMainMenu()
    {
        LoadingSceneManager.sceneToLoad = "MainMenu";
        SceneManager.LoadScene("LoadingScreen");
    }
}
