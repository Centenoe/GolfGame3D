using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

//Chazong Lor

public class Menu : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool isPaused;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
            {
                ResumeGame();
            }
            else
            {
                isPaused = true;
                pauseMenu.SetActive(true);
                Time.timeScale = 0;
            }
        }       
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }   

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GoSettingsMenu()
    {
        SceneManager.LoadScene("SettingsMenu");
    }

    public void GoColorChangeMenu()
    {
        SceneManager.LoadScene("ColorChangeMenu");
    }

    public void GoMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }

    public void GoLeaderBoards()
    {
        SceneManager.LoadScene("Leaderboard");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
    }
        
    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
