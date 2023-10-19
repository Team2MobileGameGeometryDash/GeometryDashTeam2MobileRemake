using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    #region VariablesInInspector

    public Image MainMenuScreen;
    public Image CreditScreen;
    public Image OptionsScreen;

    #endregion


    #region ButtonsFunctions

    /// <summary>
    ///  for PLAY button
    /// </summary>
    /// <param name="sceneName"></param>
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    /// <summary>
    /// for QUIT button
    /// </summary>
    public void ExitApplication()
    {
        Application.Quit();
    }

    /// <summary>
    /// Credits panel
    /// </summary>
    public void ToggleCredits()
    {
        ToggleUIScreen(CreditScreen, MainMenuScreen);
    }

    /// <summary>
    /// Options screen handler
    /// </summary>
    public void ToggleOptions()
    {
        ToggleUIScreen(OptionsScreen, MainMenuScreen);
    }

    /// <summary>
    /// Used to switch panel
    /// </summary>
    /// <param name="selectedScreen">the panel you want to active or disactive</param>
    /// <param name="previousScreen">the previous panel</param>
    public void ToggleUIScreen(Image selectedScreen, Image previousScreen)
    {
        if (!selectedScreen.gameObject.activeInHierarchy)
        {
            selectedScreen.gameObject.SetActive(true);
            previousScreen.gameObject.SetActive(false);
            return;
        }
        else
        {
            previousScreen.gameObject.SetActive(true);
            selectedScreen.gameObject.SetActive(false);
            return;
        }
    }

    #endregion
}
