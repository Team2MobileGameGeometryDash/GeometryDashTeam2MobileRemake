using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
public class PlayerUIManager : MonoBehaviour
{
    
    public Transform EndMap;
    Slider _slider;

    private void Awake()
    {
        _slider = GetComponentInChildren<Slider>();
        DistanceToEnd();
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


}
