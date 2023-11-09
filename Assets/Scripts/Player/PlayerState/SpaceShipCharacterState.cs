using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipCharacterState : State<PlayerState>
{
    PlayerController _playerController;
    PlayerStateManager _playerStateManager;

    public SpaceShipCharacterState(PlayerState playerState, StatesMachine<PlayerState> stateManager = null) : base(playerState, stateManager)
    {
        _playerStateManager = (PlayerStateManager)m_stateMachine;
        
    }


    public override void OnEnter()
    {
        base.OnEnter();
        if (_playerController == null) _playerController = _playerStateManager.PlayerController;
        PlayerInputManager.IsTouchEnded = false;
        PlayerInputManager.IsTouchStationary = false;
        ActionManager.OnChangeShip?.Invoke(new SpaceShipInput());
        _playerController.PlayerSOShipCharacter.ApplyDefaultParameters(_playerController);
        //Debug.Log(" SECOND STATE ON ENTER _PlayerController : " + _playerController);
        //Debug.Log(_playerController);
    }


    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();

        HandleAllMouvement();

    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.TryGetComponent(out Death death)) _playerStateManager.ChangeState(PlayerState.Death);
    }



    private void HandleAllMouvement()
    {
        _playerController.PlayerMouvement.HandleMouvementBaseCharacter(_playerController.PlayerSOShipCharacter);

        if (PlayerInputManager.IsTouchEnded)
        {
            _playerController.PlayerMouvement.HandleJumpingShipCharacter(_playerController.PlayerSOShipCharacter);
        }
        else if (PlayerInputManager.IsTouchStationary)
        {
            _playerController.PlayerMouvement.HandleJumpingForceShipCharacter(_playerController.PlayerSOShipCharacter);
        }
    }


}