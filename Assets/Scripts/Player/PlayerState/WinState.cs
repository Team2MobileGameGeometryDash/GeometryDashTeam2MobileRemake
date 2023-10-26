using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinState : State<PlayerState>
{
    PlayerController _playerController;
    PlayerStateManager _playerStateManager;

    public WinState(PlayerState playerState, StatesMachine<PlayerState> stateManager = null) : base(playerState, stateManager)
    {
        _playerStateManager = (PlayerStateManager)m_stateMachine;

    }


    public override void OnEnter()
    {
        base.OnEnter();
        if (_playerController == null) _playerController = _playerStateManager.PlayerController;
        //Debug.Log("winn");
        PlayerUIManager.OnUpdateScoreProgress?.Invoke();
        LoadScene();

    }


    public void LoadScene()
    {
        SceneManager.LoadScene("TestMenu", LoadSceneMode.Single);
        
    }


}