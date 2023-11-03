using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Transform Player;
    public Transform EndMap;
    private Coins[] _coinList;
    private AudioSource[] _audioSources;
    float _startPosition;
    int _deathCount;
    float _sliderValue => UpdatedSliderValue();

    private void Start()
    {
        _startPosition = transform.position.x;
        _coinList = FindObjectsOfType<Coins>();
        _audioSources = FindObjectsOfType<AudioSource>();
        _deathCount = 0;
    }


    private void Update()
    {
        UpdatedSliderValue();
    }

    public float UpdatedSliderValue()
    {
        return (transform.position.x - _startPosition) / (EndMap.position.x - _startPosition);
    }

    private void SaveSliderValue()
    {
        float savedValue = PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name);
        if (_sliderValue > savedValue) PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name, _sliderValue);
    }

    private void UpdateDeathCount()
    {
        _deathCount += 1;
    }

    private void CoinsDetectionWin()
    {
        foreach (Coins coin in _coinList)
        {
            coin.SaveCoins();
        }
    }

    private void CoinsDetectionDeath()
    {
        foreach (Coins coin in _coinList)
        {
            coin.ResetCoins();
        }
    }

    public void PauseAudio()
    {
        foreach (AudioSource audioSource in _audioSources)
        {
            audioSource.Pause();
        }
    }

    public void ResumeAudio()
    {
        foreach (AudioSource audioSource in _audioSources)
        {
            audioSource.UnPause();
        }
    }
}
