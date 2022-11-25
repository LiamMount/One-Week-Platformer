using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject infoMenu;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        mainMenu.SetActive(true);
        infoMenu.SetActive(false);
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("LevelOne");
    }

    public void InfoButton()
    {
        mainMenu.SetActive(false);
        infoMenu.SetActive(true);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void MenuButton()
    {
        infoMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void OpenLink(string link)
    {
        Application.OpenURL(link);
    }

    public void ReturnToMainButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
