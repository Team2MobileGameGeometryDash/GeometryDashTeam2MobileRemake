using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    public Image PauseScreen;
    public Image WinScreen;
    public Slider sliderProgress;
    private int currentSceneIndex;

    private void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
    public void TogglePause()
    {
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

    public void LoadNextScene()
    {
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    public void ReturnToTitle()
    {
        SceneManager.LoadScene(0);
    }
}
