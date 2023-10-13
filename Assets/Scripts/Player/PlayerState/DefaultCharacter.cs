using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultCharacter : State<PlayerState>
{
    PlayerController _playerController;
    PlayerStateManager _playerStateManager;
    public Vector2 UpAxis = Vector2.up;
    public DefaultCharacter(PlayerState playerState, StatesMachine<PlayerState> stateManager = null) : base(playerState, stateManager)
    {
        _playerStateManager = (PlayerStateManager)m_stateMachine;
        
    }


    public override void OnEnter()
    {
        base.OnEnter();
        
        if(_playerController==null) _playerController = _playerStateManager._playerController;
        _playerController.ChangeCharacter(true,0);

    }


    public override void OnUpdate()
    {
        base.OnUpdate();

        if (_playerController.isGrounded())
        {
            _playerController.PlayerMouvement.RotationWhenGrounded(_playerController.Ships[0]); 
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (!_playerController.Data.isGravityChange)
                    GameManager.Instance.observerPattern.TriggerEvent(GameEventEnum.GameEvent.GravitySwitch, 1f);
                else
                    GameManager.Instance.observerPattern.TriggerEvent(GameEventEnum.GameEvent.GravitySwitch, -1f);
            }
        }
        else _playerController.PlayerMouvement.RotationNotGrounded(_playerController.Ships[0], !_playerController.Data.isGravityChange);
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        _playerController.PlayerMouvement.HandleMovement();
    }

    public override void OnExit()
    {
        base.OnExit();
        _playerController.ChangeCharacter(false, 0);
    }







    

    
}