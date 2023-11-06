using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public string LevelName;
    private float ScoreProgress;
    public TextMeshProUGUI ScoreText;
    public Slider ScoreSlider;
    public Image[] MissCoins;

    private void Start()
    {
        ScoreProgress = PlayerPrefs.GetFloat(LevelName);
        if (ScoreProgress > 0) ScoreSlider.value = ScoreProgress;
        else ScoreSlider.value = 0;
        ScoreText.text = (ScoreSlider.value * 100).ToString() + "%";
        for (int i = 0; i < MissCoins.Length; i++)
        {
            if (PlayerPrefs.GetInt(LevelName + i) == 1)
                MissCoins[i].enabled = false;
            else MissCoins[i].enabled = true;
        }
    }

    /// <summary>
    ///  for PLAY button
    /// </summary>
    /// <param name="sceneName"></param>
    public void LoadLevelScene()
    {
        SceneManager.LoadScene(LevelName);
        Time.timeScale = 1;
    }
}
