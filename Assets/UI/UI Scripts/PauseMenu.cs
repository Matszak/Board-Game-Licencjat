using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject SettingsMenu;
    public GameObject QuitMenu;
    
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            SettingsMenu.SetActive(false);
        }
    }
    // public void PauseButton()
    // {
    //     Time.timeScale = 0;
    //     pauseMenu.SetActive(true);
    //     SettingsMenu.SetActive(false);
    // }
    public void SettingsButton()
    {
        pauseMenu.SetActive(false);
        SettingsMenu.SetActive(true);
    }
    public void ResumeButton()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    public void CloseMenuButton()
    {
        SettingsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }
    public void BackToMainMenuButton()
    {
        LoadingSceneManager.sceneToLoad = "MainMenu";
        SceneManager.LoadScene("LoadingScreen");
    }
}
