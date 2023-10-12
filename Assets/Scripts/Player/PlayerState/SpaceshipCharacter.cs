using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipCharacter : State<PlayerState>
{
    PlayerController _playerController;
    PlayerStateManager _playerStateManager;

    public SpaceshipCharacter(PlayerState playerState, StatesMachine<PlayerState> stateManager = null) : base(playerState, stateManager)
    {
        _playerStateManager = (PlayerStateManager)m_stateMachine;
        
    }


    public override void OnEnter()
    {
        base.OnEnter();
        if (_playerController == null) _playerController = _playerStateManager._playerController;
        _playerController.ChangeCharacter(true, 1);
        //Debug.Log(" SECOND STATE ON ENTER _PlayerController : " + _playerController);
        //Debug.Log(_playerController);

    }


    public override void OnUpdate()
    {
        base.OnUpdate();
       





    }




    public override void OnExit()
    {
        base.OnExit();
        _playerController.ChangeCharacter(false, 1);

    }





}