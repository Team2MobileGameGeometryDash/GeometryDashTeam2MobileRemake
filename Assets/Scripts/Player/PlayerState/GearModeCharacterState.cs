using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearModeCharacterState : State<PlayerState>
{
    PlayerController _playerController;
    PlayerStateManager _playerStateManager;

    public GearModeCharacterState(PlayerState playerState, StatesMachine<PlayerState> stateManager = null) : base(playerState, stateManager)
    {
        _playerStateManager = (PlayerStateManager)m_stateMachine;

    }


    public override void OnEnter()
    {
        base.OnEnter();
        if (_playerController == null) _playerController = _playerStateManager.PlayerController;
        ActionManager.OnChangeShip?.Invoke(new GearModeInput());
        _playerController.PlayerSOGearModeCharacter.ApplyDefaultParameters(_playerController);
    }


    public override void OnUpdate()
    {
        base.OnUpdate();
        HandleMouvement();

    }



    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.TryGetComponent(out Death death)) _playerStateManager.ChangeState(PlayerState.Death);

    }


   


    private void HandleMouvement()
    {

        _playerController.PlayerMouvement.HandleMouvementBaseCharacter(_playerController.PlayerSOBaseData);

        if (PlayerInputManager.IsTouchEnded)
        {
            if (_playerController.PlayerRigidBody2D.gravityScale < 0)
            {
                UpdateGravityScale(1);
                PlayerInputManager.IsTouchEnded = false;
            }
            else
            {
                UpdateGravityScale(-1);
                PlayerInputManager.IsTouchEnded = false;
            }
        }
    }

    private void UpdateGravityScale(float direction)
    {
        _playerController.PlayerRigidBody2D.gravityScale = _playerController.PlayerSOGearModeCharacter.GravityScale * _playerController.PlayerSOGearModeCharacter.GravityVelocity * direction;
    }

  
}
