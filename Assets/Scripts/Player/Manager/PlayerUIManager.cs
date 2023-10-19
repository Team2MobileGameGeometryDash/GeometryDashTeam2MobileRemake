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
    public GameObject winButton;


    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        DistanceToEnd();
    }

    private void OnEnable()
    {
        GameManager.Instance.ObserverPatternGame.Register(GameEventEnum.GameEvent.Death, UpdateDeathCount);
        GameManager.Instance.ObserverPatternGame.Register(GameEventEnum.GameEvent.win, LoadWinScene);
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
        Slider.maxValue = Mathf.Abs(distance);
        
    }

    private void UpdateSlider()
    {

        if(Mathf.Approximately(Slider.maxValue ,Slider.value))
        {
            //Debug.Log("sono uguale");
            Slider.value = Slider.maxValue;
            _playerController.PlayerStateManager.ChangeState(PlayerState.Win);
            return;
        }
        Slider.value = transform.position.x;
        
    }

    private void UpdateDeathCount(object[] action = null)
    {

        _playerController.PlayerData.Death += 1;
        DeathCount.text = _playerController.PlayerData.Death.ToString();
    }



    private void LoadWinScene(object[] s=null)
    {
        winButton.SetActive(true);
        
    }

}
