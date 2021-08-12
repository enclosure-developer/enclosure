using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void startGame()
    {
        SceneManager.LoadScene("GameOnPlay");
    }
    public void multiPlay()
    {
        SceneManager.LoadScene("GameOnPlay_multi");
    }
    public void gotoMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void gotoSettingMenu()
    {
        SceneManager.LoadScene("SettingMenu");
        //SceneManager.UnloadScene("MainMenu");
    }
    public void quitGame()
    {
        Application.Quit();
    }
}
