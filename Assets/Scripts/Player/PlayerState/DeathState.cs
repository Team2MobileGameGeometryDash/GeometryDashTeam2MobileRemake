using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : State<PlayerState>
{
    PlayerController _playerController;
    PlayerStateManager _playerStateManager;
    float _time = 0.4f;



    public DeathState(PlayerState playerState, StatesMachine<PlayerState> stateManager = null) : base(playerState, stateManager)
    {
        _playerStateManager = (PlayerStateManager)m_stateMachine;

    }


    public override void OnEnter()
    {
        base.OnEnter();
        if (_playerController == null) _playerController = _playerStateManager.PlayerController;
        PlayerUIManager.OnUpdateScoreProgress?.Invoke();
        PlayerUIManager.OnDeath?.Invoke();


        
 
    }


    public override void OnUpdate()
    {
        base.OnUpdate();
        Timer();
        if (_time <= 0)
        {
            _playerController.transform.position = _playerController.InitialPosition;
            _playerStateManager.ChangeState(PlayerState.DefaultCharacter);
        }
    }


    public override void OnExit()
    {
        base.OnExit();
        _playerController.PlayerRigidBody2D.gravityScale = _playerController.DefaultCharacterData.GravityScale;
        _playerController.PlayerRigidBody2D.velocity = Vector2.zero;
        AnimationController.OnDisableVFX?.Invoke();
        CameraFollow.OnResetCamera?.Invoke();
        _time = 0.4f;


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