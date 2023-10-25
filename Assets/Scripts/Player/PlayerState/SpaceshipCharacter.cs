using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipCharacter : State<PlayerState>
{
    PlayerController _playerController;
    PlayerStateManager _playerStateManager;

    public SpaceShipCharacter(PlayerState playerState, StatesMachine<PlayerState> stateManager = null) : base(playerState, stateManager)
    {
        _playerStateManager = (PlayerStateManager)m_stateMachine;
        
    }


    public override void OnEnter()
    {
        base.OnEnter();
        if (_playerController == null) _playerController = _playerStateManager.PlayerController;
        _playerController.ChangeCharacter(true, 1);
        _playerController.PlayerRigidBody2D.gravityScale = _playerController.SpaceShipCharacterData.GravityScale;

        //Debug.Log(" SECOND STATE ON ENTER _PlayerController : " + _playerController);
        //Debug.Log(_playerController);
    }


    public override void OnUpdate()
    {
        base.OnUpdate();

        if (_playerController.DefaultCharacterData.IsDefaultCharacter)
            _playerStateManager.ChangeState(PlayerState.DefaultCharacter);
        else if (_playerController.GearModeData.IsGearMode)
            _playerStateManager.ChangeState(PlayerState.GearModeCharacter);
    }


    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();

        HandleAllMouvement();
        _playerController.PlayerMouvement.HandleMouvementBaseCharacter();
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.TryGetComponent(out Death death)) _playerStateManager.ChangeState(PlayerState.Death);
    }


    public override void OnExit()
    {
        base.OnExit();
        _playerController.ChangeCharacter(false, 1);
        _playerController.SpaceShipCharacterData.IsSpaceShip = false;
    }


   
    private void HandleAllMouvement()
    {
        if (PlayerInputManager.IsTouchEnded)
        {
            _playerController.PlayerMouvement.HandleJumpingShipCharacter();
        }
        else if (PlayerInputManager.IsTouchStationary)
        {
            _playerController.PlayerMouvement.HandleJumpingForceShipCharacter();
        }
    }

    
  
}