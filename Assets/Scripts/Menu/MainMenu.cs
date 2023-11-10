using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    #region VariablesInInspector

    public Image MainMenuScreen;
    public Image CreditScreen;
    public Image OptionsScreen;
    public Image LevelsScreen;
    private AudioListener[] audioListeners;
    private int PressCount;

    #endregion

    private void Start()
    {
        PressCount = 0;
    }

    #region ButtonsFunctions

    /// <summary>
    ///  for PLAY button
    /// </summary>
    /// <param name="sceneName"></param>
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        Time.timeScale = 1;
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
    /// Levels screen handler
    /// </summary>
    public void ToggleLevels()
    {
        ToggleUIScreen(LevelsScreen, MainMenuScreen);
    }

    /// <summary>
    /// Used to switch panel
    /// </summary>
    /// <param name="selectedScreen">the panel you want to active or disactive</param>
    /// <param name="previousScreen">the previous panel</param>
    private void ToggleUIScreen(Image selectedScreen, Image previousScreen)
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

    public void EasterEgg(string sceneName)
    {
        if (PressCount < 4) PressCount++;
        if (PressCount == 4)
        {
            PressCount = 0;
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
    }

    public void ResetData()
    {
        PlayerPrefs.DeleteAll();
    }

    #endregion


}
