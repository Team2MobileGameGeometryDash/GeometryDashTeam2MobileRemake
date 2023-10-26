using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    //public SceneAsset Level;
    private float ScoreProgress;
    public TextMeshProUGUI ScoreText;
    public Slider ScoreSlider;
    public Image[] MissCoins;

    private void Start()
    {
        //ScoreProgress = PlayerPrefs.GetFloat(Level.name);
        //if (ScoreProgress > 0) ScoreSlider.value = ScoreProgress;
        //else ScoreSlider.value = 0;
        //ScoreText.text = (ScoreSlider.value * 100).ToString() + "%" ;
        //for (int i = 0; i < MissCoins.Length; i++)
        //{
        //    if (PlayerPrefs.GetInt(Level.name + i) == 1)
        //        MissCoins[i].enabled = false;
        //    else MissCoins[i].enabled = true;
        //}
    }

    /// <summary>
    ///  for PLAY button
    /// </summary>
    /// <param name="sceneName"></param>
    public void LoadLevelScene()
    {
        SceneManager.LoadScene("StereoMadnessLvL1");
        Time.timeScale = 1;
    }
}
