using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfoCharacter : State<PlayerState>
{
    PlayerController _playerController;
    PlayerStateManager _playerStateManager;
    PlayerInputManager _playerInputManager;

    public UfoCharacter(PlayerState playerState, StatesMachine<PlayerState> stateManager = null) : base(playerState, stateManager)
    {
        _playerStateManager = (PlayerStateManager)m_stateMachine;

    }

    public override void OnEnter()
    {
        base.OnEnter();

        if (_playerController == null) _playerController = _playerStateManager.PlayerController;
        _playerInputManager = _playerController.PlayerInputManager;
        ActionManager.OnChangeShip?.Invoke(new UfoInput());
        _playerController.ChangeCharacter(true, 3);
        _playerController.PlayerRigidBody2D.gravityScale = _playerController.UfoCharacterData.GravityScale;

    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (_playerController.SpaceShipCharacterData.IsSpaceShip)
            _playerStateManager.ChangeState(PlayerState.SpaceshipCharacter);
        else if (_playerController.GearModeData.IsGearMode)
            _playerStateManager.ChangeState(PlayerState.GearModeCharacter);
        else if (_playerController.DefaultCharacterData.IsDefaultCharacter)
            _playerStateManager.ChangeState(PlayerState.DefaultCharacter);
    }




    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
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

        if (!_playerController.PlayerData.isWin)
        {
            _playerController.ChangeCharacter(false, 3);
            _playerController.UfoCharacterData.IsUfo = false;
        }
    }



    private void HandleAllMouvement()
    {
        _playerController.PlayerMouvement.HandleMouvementBaseCharacter();

        if (PlayerInputManager.IsTouchEnded)
        {
            _playerController.PlayerMouvement.HandleJumpingUfoCharacter();
            
        }
        
    }





}
