using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
public class PlayerUIManager : MonoBehaviour
{
    
    public Transform EndMap;
    Slider _slider;
    public TextMeshProUGUI DeathCount;
    PlayerController _playerController;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _slider = GetComponentInChildren<Slider>();
        DistanceToEnd();
    }

    private void OnEnable()
    {
        GameManager.Instance.ObserverPatternGame.Register(GameEventEnum.GameEvent.Death, UpdateDeathCount);
    }

    private void OnDisable()
    {
        GameManager.Instance.ObserverPatternGame.Unregister(GameEventEnum.GameEvent.Death, UpdateDeathCount);
    }
    private void Update()
    {
        UpdateSlider();
    }

    private void DistanceToEnd()
    {
        float distance = Vector2.Distance(transform.position, EndMap.position);
        _slider.maxValue = distance;
    }

    private void UpdateSlider()
    {
        if(_slider.value == _slider.maxValue)
        {
            _slider.value = _slider.maxValue;
            return;
        }
        _slider.value = transform.position.x;
        
    }

    private void UpdateDeathCount(object[] action = null)
    {

        _playerController.Data.Death += 1;
        DeathCount.text = _playerController.Data.Death.ToString();
    }


}
