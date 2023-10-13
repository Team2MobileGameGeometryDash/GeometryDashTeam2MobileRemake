using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultCharacter : State<PlayerState>
{
    PlayerController _playerController;
    PlayerStateManager _playerStateManager;


    public DefaultCharacter(PlayerState playerState, StatesMachine<PlayerState> stateManager = null) : base(playerState, stateManager)
    {
        _playerStateManager = (PlayerStateManager)m_stateMachine;
        
    }


    public override void OnEnter()
    {
        base.OnEnter();
        
        if(_playerController==null) _playerController = _playerStateManager.PlayerController;
        _playerController.ChangeCharacter(true,0);
        

    }


    public override void OnUpdate()
    {
        base.OnUpdate();
        HandleAllMouvement();


        if (_playerController.Data.IsSpaceShip)
            _playerStateManager.ChangeState(PlayerState.SpaceshipCharacter);
        else if (_playerController.Data.IsDeath)
            _playerStateManager.ChangeState(PlayerState.Death);
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        _playerController.PlayerMouvement.HandleMouvementBaseCharacter();
    }

    public override void OnExit()
    {
        base.OnExit();
        _playerController.ChangeCharacter(false, 0);
        
    }



    private void HandleAllMouvement()
    {
        if (_playerController.isGrounded())
        {
            _playerController.PlayerMouvement.RotationWhenGroundedBaseCharacter(_playerController.Data.Ships[0]);
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (!_playerController.Data.IsGravityChange)
                    GameManager.Instance.ObserverPatternPlayer.TriggerEvent(GameEventEnum.PlayerGameEvent.DefaultJump, 1f);
                else
                    GameManager.Instance.ObserverPatternPlayer.TriggerEvent(GameEventEnum.PlayerGameEvent.DefaultJump, -1f);
            }
        }
        else _playerController.PlayerMouvement.RotationNotGroundedBaseCharacter(_playerController.Data.Ships[0], !_playerController.Data.IsGravityChange);
    }



    

    
}