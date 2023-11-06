using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public Transform Player;
    public Transform EndMap;
    private Coins[] _coinList;
    private AudioSource[] _audioSources;
    private UIManager _uiManager;
    public BackgroundChange BackgroundChange;
    public LevelMenu levelMenu;
    float _startPosition;
    int _attemptCount;
    float _sliderValue => UpdatedSliderValue();

    private void Start()
    {
        _audioSources = FindObjectsOfType<AudioSource>();
        _uiManager = GetComponentInChildren<UIManager>();
        _coinList = FindObjectsOfType<Coins>();
        _startPosition = transform.position.x;
        _attemptCount = 0;
        UpdateAttemptCount();
    }


    private void Update()
    {
        _uiManager.SetSliderValue(_sliderValue);
    }

    public void onDeath()
    {
        SaveSliderValue();
        UpdateAttemptCount();        
        CoinsDetectionDeath();
        BackgroundChange.ResetBackGround();
    }

    public void onWin()
    {
        SaveSliderValue();
        CoinsDetectionWin();
        _uiManager.ActiveWinPanel();
    }

    #region Generic void

    public float UpdatedSliderValue()
    {
        return (transform.position.x - _startPosition) / (EndMap.position.x - _startPosition);
    }

    private void SaveSliderValue()
    {
        float savedValue = PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name);
        if (_sliderValue > savedValue) PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name, _sliderValue);
    }

    private void UpdateAttemptCount()
    {
        _attemptCount += 1;
        _uiManager.UpdateAttempt(_attemptCount);
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
    #endregion

}
