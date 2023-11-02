using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinState : State<PlayerState>
{
    PlayerController _playerController;
    PlayerStateManager _playerStateManager;
    float _time = 1f;
    public WinState(PlayerState playerState, StatesMachine<PlayerState> stateManager = null) : base(playerState, stateManager)
    {
        _playerStateManager = (PlayerStateManager)m_stateMachine;

    }


    public override void OnEnter()
    {
        base.OnEnter();
        if (_playerController == null) _playerController = _playerStateManager.PlayerController;
        Debug.Log("winn");
        ActionManager.OnUpdateScoreProgress?.Invoke();
        _playerController.PlayerRigidBody2D.velocity = new Vector2(0f, _playerController.PlayerRigidBody2D.velocity.y);

    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        ActionManager.OnWin?.Invoke();
        Timer();
        if (_time <= 0)
        {
            LoadScene();
        }
    }


    public override void OnExit()
    {
        base.OnExit();
        _time = 1f;
    }

    public void LoadScene()
    {
        SceneManager.LoadScene("TestMenu", LoadSceneMode.Single);

    }



    private float Timer()
    {
        if (_time > 0)
        {
            _time -= Time.deltaTime;

            return _time;
        }
        else return _time;

    }
}