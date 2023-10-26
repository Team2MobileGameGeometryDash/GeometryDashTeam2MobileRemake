using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using System;

public class PlayerUIManager : MonoBehaviour
{
    public static Action OnDeath;
    public static Action OnUpdateScoreProgress;



    public Transform EndMap;
    public Slider Slider;
    public TextMeshProUGUI DeathCount;
    PlayerController _playerController;
    float _saveScore;


    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        DistanceToEnd();
    }

    private void OnEnable()
    {
        OnDeath += UpdateDeathCount;
        OnUpdateScoreProgress += SaveSliderValue;
    }

    private void Update()
    {
        UpdateSlider();
    }

    private void DistanceToEnd()
    {
        float distance = Vector2.Distance(transform.position, EndMap.position);
        Slider.maxValue = Mathf.Abs(distance);
        
    }

  


    private void UpdateSlider()
    {
        if(Mathf.Approximately(Slider.maxValue ,Slider.value))
        {
            Slider.value = Slider.maxValue;
            _playerController.PlayerStateManager.ChangeState(PlayerState.Win);
            return;
        }
        Slider.value = transform.position.x;
        
    }




    private void UpdateDeathCount()
    {

        _playerController.PlayerData.Death += 1;
        DeathCount.text = _playerController.PlayerData.Death.ToString();
    }


    private void SaveSliderValue()
    {
        if (Slider.value > _saveScore) _saveScore = Slider.value;
    }

    private void OnDisable()
    {
        OnDeath -= UpdateDeathCount;
        OnUpdateScoreProgress -= SaveSliderValue;
    }



  
}
