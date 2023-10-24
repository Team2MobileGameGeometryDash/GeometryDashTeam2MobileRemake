using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : State<PlayerState>
{
    PlayerController _playerController;
    PlayerStateManager _playerStateManager;

    public DeathState(PlayerState playerState, StatesMachine<PlayerState> stateManager = null) : base(playerState, stateManager)
    {
        _playerStateManager = (PlayerStateManager)m_stateMachine;

    }


    public override void OnEnter()
    {
        base.OnEnter();
        if (_playerController == null) _playerController = _playerStateManager.PlayerController;
        
        _playerController.transform.position = _playerController.InitialPosition;
        _playerStateManager.ChangeState(PlayerState.DefaultCharacter);
        
        
    }



    public override void OnExit()
    {
        base.OnExit();
        _playerController.PlayerRigidBody2D.gravityScale = _playerController.DefaultCharacterData.GravityScale;
        _playerController.PlayerRigidBody2D.velocity = Vector2.zero;
        GameManager.Instance.ObserverPatternGame.TriggerEvent(GameEventEnum.GameEvent.Death);



    }


}