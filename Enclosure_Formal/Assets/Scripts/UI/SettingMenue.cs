using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingMenue : MonoBehaviour
{
    public void gotoMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        //SceneManager.UnloadScene("SettingMenu");
    }
    public void gotoBasicSetting()
    {
        SceneManager.LoadScene("BasicSetting");
        //SceneManager.UnloadScene("SettingMenu");
    }
}
