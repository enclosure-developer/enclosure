using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BasicSettingMenu : MonoBehaviour
{
    public void gotoSettingMenu()
    {
        SceneManager.LoadScene("SettingMenu");
        //SceneManager.UnloadScene("BasicSettingMenu");
    }
    
}
