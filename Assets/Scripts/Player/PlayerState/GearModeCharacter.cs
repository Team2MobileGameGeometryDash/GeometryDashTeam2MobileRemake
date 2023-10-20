using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearModeCharacter : State<PlayerState>
{
    PlayerController _playerController;
    PlayerStateManager _playerStateManager;

    public GearModeCharacter(PlayerState playerState, StatesMachine<PlayerState> stateManager = null) : base(playerState, stateManager)
    {
        _playerStateManager = (PlayerStateManager)m_stateMachine;

    }


    public override void OnEnter()
    {
        base.OnEnter();
        if (_playerController == null) _playerController = _playerStateManager.PlayerController;
        _playerController.ChangeCharacter(true, 2);
        _playerController.PlayerRigidBody2D.gravityScale = _playerController.GearModeData.GravityScale * _playerController.GearModeData.GravityVelocity;

    }


    public override void OnUpdate()
    {
        base.OnUpdate();
        HandleMouvement();
        if (_playerController.DefaultCharacterData.IsDefaultCharacter)
            _playerStateManager.ChangeState(PlayerState.DefaultCharacter);
        else if (_playerController.SpaceShipCharacterData.IsSpaceShip)
            _playerStateManager.ChangeState(PlayerState.SpaceshipCharacter);

    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
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
        _playerController.ChangeCharacter(false, 2);
        _playerController.GearModeData.IsGearMode = false;
    }


    //to fix
    private void HandleMouvement()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (_playerController.PlayerRigidBody2D.gravityScale < 0)
                _playerController.PlayerRigidBody2D.gravityScale = _playerController.GearModeData.GravityScale * _playerController.GearModeData.GravityVelocity;
            else _playerController.PlayerRigidBody2D.gravityScale = _playerController.GearModeData.GravityScale * -1 * _playerController.GearModeData.GravityVelocity;
        }

    }
}
