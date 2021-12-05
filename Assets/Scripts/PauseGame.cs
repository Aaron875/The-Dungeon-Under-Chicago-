using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    public bool gamePaused = false;

    public GameObject pauseMenu;

    public GameObject howtoPlayScreen;

    private void Start()
    {
        pauseMenu.SetActive(false);
        howtoPlayScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!gamePaused)
            {
                Pause();
            }
        }
    }

    //Pause the game
    void Pause()
    {
        gamePaused = true;
        Time.timeScale = 0.0f;
        pauseMenu.SetActive(true);
    }

    //Resume the game
    public void Resume()
    {
        gamePaused = false;
        Time.timeScale = 1.0f;
        pauseMenu.SetActive(false);
    }

    //Return to main menu
    public void QuitToMainMenu()
    {
        Resume();
        SceneManager.LoadScene("MainMenu");
    }

    public void HowToPlay()
    {
        pauseMenu.SetActive(false);
        howtoPlayScreen.SetActive(true);
    }

    public void Back()
    {
        pauseMenu.SetActive(true);
        howtoPlayScreen.SetActive(false);
    }
}
