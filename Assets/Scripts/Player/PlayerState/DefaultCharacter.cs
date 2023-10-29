using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultCharacter : State<PlayerState> 
{
    PlayerController _playerController;
    PlayerStateManager _playerStateManager;
    PlayerInputManager _playerInputManager;

    public DefaultCharacter(PlayerState playerState, StatesMachine<PlayerState> stateManager = null) : base(playerState, stateManager)
    {
        _playerStateManager = (PlayerStateManager)m_stateMachine;
        
    }


    public override void OnEnter()
    {
        base.OnEnter();
        
        if(_playerController==null) _playerController = _playerStateManager.PlayerController;
        _playerInputManager = _playerController.PlayerInputManager;
        _playerController.ChangeCharacter(true,0);
        _playerController.PlayerRigidBody2D.gravityScale = _playerController.DefaultCharacterData.GravityScale;
        _playerController.DefaultCharacterData.IsGravityChange = false;

    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (_playerController.SpaceShipCharacterData.IsSpaceShip)
            _playerStateManager.ChangeState(PlayerState.SpaceshipCharacter);
        else if (_playerController.GearModeData.IsGearMode)
            _playerStateManager.ChangeState(PlayerState.GearModeCharacter);
    }




    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        _playerController.PlayerMouvement.HandleMouvementBaseCharacter();

        HandleAllMouvement();
        
    }


    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.TryGetComponent(out Death death))
        {
            //Debug.Log(collision);
            _playerStateManager.ChangeState(PlayerState.Death);
        }
        
    }

    public override void OnExit()
    {
        base.OnExit();
        _playerController.ChangeCharacter(false, 0);
        _playerController.DefaultCharacterData.IsDefaultCharacter = false;
        

    }



    private void HandleAllMouvement()
    {
        if (_playerController.PlayerMouvement.isGrounded())
        {
            if (PlayerInputManager.IsTouchEnded || PlayerInputManager.IsTouchStationary)
            {
                _playerController.PlayerMouvement.HandleJumpingBaseCharacter();
            }
            _playerController.PlayerMouvement.RotationWhenGroundedBaseCharacter(_playerController.PlayerData.Ships[0]);
        }
        else _playerController.PlayerMouvement.RotationNotGroundedBaseCharacter(_playerController.PlayerData.Ships[0], !_playerController.DefaultCharacterData.IsGravityChange);
    }


   


}