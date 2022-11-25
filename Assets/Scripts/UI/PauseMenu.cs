using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;

    void Start()
    {
        PlayerVariables.isPaused = false;
    }

    void Update()
    {
        GetPauseInput();
    }

    // Pause Logic

    public void GetPauseInput()
    {
        if (Input.GetButtonDown("Start") && !PlayerVariables.isPaused)
        {
            Pause();
        }
        else if (Input.GetButtonDown("Start") && PlayerVariables.isPaused)
        {
            Resume();
        }
    }

    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0f;

        PlayerVariables.isPaused = true;
        pauseMenuUI.SetActive(true);
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Time.timeScale = 1f;

        PlayerVariables.isPaused = false;
        pauseMenuUI.SetActive(false);
    }

    public void ResumeButton()
    {
        Resume();
    }

    public void MenuButton()
    {
        Resume();
        SceneManager.LoadScene("MainMenu");
    }
}
