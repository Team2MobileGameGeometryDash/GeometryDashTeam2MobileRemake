using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : State<PlayerState>
{
    PlayerController _playerController;
    PlayerStateManager _playerStateManager;
    float _time = 0.5f;



    public DeathState(PlayerState playerState, StatesMachine<PlayerState> stateManager = null) : base(playerState, stateManager)
    {
        _playerStateManager = (PlayerStateManager)m_stateMachine;

    }


    public override void OnEnter()
    {
        base.OnEnter();
        if (_playerController == null) _playerController = _playerStateManager.PlayerController;
        ActionManager.OnUpdateScoreProgress?.Invoke();
        ActionManager.OnDeath?.Invoke();
        _playerController.PlayerSOBaseData.Death(_playerController, false);

        
 
    }


    public override void OnUpdate()
    {
        base.OnUpdate();
        Timer();
        if (_time <= 0)
        {
            _playerController.transform.position = _playerController.InitialPosition;
            _playerStateManager.ChangeState(PlayerState.CubeCharacter);
        }
    }


    public override void OnExit()
    {
        base.OnExit();
        _playerController.PlayerRigidBody2D.velocity = Vector2.zero;
        ActionManager.OnDisableVFX?.Invoke();
        ActionManager.OnResetCamera?.Invoke();
        _time = 0.5f;
        PlayerInputManager.IsTouchEnded = false;
        PlayerInputManager.IsTouchStationary = false;
        _playerController.PlayerSOBaseData.Death(_playerController, true);
        _playerController.PlayerSOBaseData.WalkingSpeed = _playerController.PlayerSOBaseData.DefaultWalkingSpeed;

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