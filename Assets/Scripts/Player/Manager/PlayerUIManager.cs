using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
public class PlayerUIManager : MonoBehaviour
{
    
    public Transform EndMap;
    public Slider Slider;
    public TextMeshProUGUI DeathCount;
    PlayerController _playerController;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
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
        Slider.maxValue = distance;
    }

    private void UpdateSlider()
    {
        if(Slider.value == Slider.maxValue)
        {
            Slider.value = Slider.maxValue;
            return;
        }
        Slider.value = transform.position.x;
        
    }

    private void UpdateDeathCount(object[] action = null)
    {

        _playerController.PlayerData.Death += 1;
        DeathCount.text = _playerController.PlayerData.Death.ToString();
    }


}
