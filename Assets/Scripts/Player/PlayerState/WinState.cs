using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        GameManager.Instance.ObserverPatternGame.TriggerEvent(GameEventEnum.GameEvent.win);

    }


    public override void OnUpdate()
    {
        base.OnUpdate();


    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();

    }


    public override void OnExit()
    {
        base.OnExit();



    }


}