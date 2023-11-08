using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    [Header("Level Menu")]
    public Image PauseScreen;
    public Image WinScreen;
    public Slider sliderProgress;
    private int currentSceneIndex;

    [Header("Win Panel - Coins Panel")]
    [Tooltip("Add sorted by number")]
    public Image[] MissCoins;

    private void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void TogglePause()
    {
        PlayerInputManager.IsTouchEnded = false;
        PlayerInputManager.IsTouchStationary = false;
        ToggleUIScreen(PauseScreen);
    }

    private void ToggleUIScreen(Image selectedScreen)
    {
        if (!selectedScreen.gameObject.activeInHierarchy)
        {
            selectedScreen.gameObject.SetActive(true);
            GameManager.Instance.PauseAudio();
            Time.timeScale = 0.0f;
            return;
        }
        else
        {
            selectedScreen.gameObject.SetActive(false);
            GameManager.Instance.ResumeAudio();
            Time.timeScale = 1.0f;
            return;
        }
    }

    public void ReloadScene()
    {
        int thisIndex = currentSceneIndex;
        SceneManager.LoadScene(thisIndex);
    }

    public void WinPanelCoins()
    {
        for (int i = 0; i < MissCoins.Length; i++)
        {
            if (PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + i) == 1)
                MissCoins[i].enabled = false;
            else MissCoins[i].enabled = true;
        }
    }

    public void ReturnToTitle()
    {
        SceneManager.LoadScene(0);
    }
}
