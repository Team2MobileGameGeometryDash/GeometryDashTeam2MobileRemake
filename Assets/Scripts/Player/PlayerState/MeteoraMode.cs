using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoraMode : State<PlayerState>
{
    PlayerController _playerController;
    PlayerStateManager _playerStateManager;
    PlayerInputManager _playerInputManager;

    public MeteoraMode(PlayerState playerState, StatesMachine<PlayerState> stateManager = null) : base(playerState, stateManager)
    {
        _playerStateManager = (PlayerStateManager)m_stateMachine;

    }

    public override void OnEnter()
    {
        base.OnEnter();
        if (_playerController == null) _playerController = _playerStateManager.PlayerController;
        _playerInputManager = _playerController.PlayerInputManager;
        MeteoraVelocity();
        ActionManager.OnMeteoraActiveVFX?.Invoke();
        _playerController.PlayerRigidBody2D.constraints = RigidbodyConstraints2D.FreezePositionY;
             

    }

    public override void OnUpdate()
    {
        Debug.Log("meteora");
        base.OnUpdate();

        if (!PlayerInputManager.IsTouchStationary)
        {
            if (_playerController.SpaceShipCharacterData.IsSpaceShip)
                _playerStateManager.ChangeState(PlayerState.SpaceshipCharacter);
            else if (_playerController.GearModeData.IsGearMode)
                _playerStateManager.ChangeState(PlayerState.GearModeCharacter);
            else if (_playerController.UfoCharacterData.IsUfo)
                _playerStateManager.ChangeState(PlayerState.UfoCharacter);
            else if (_playerController.DefaultCharacterData.IsDefaultCharacter)
                _playerStateManager.ChangeState(PlayerState.DefaultCharacter);
        }
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();

        _playerController.PlayerMouvement.HandleMouvementBaseCharacter();
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
        ActionManager.OnMeteoraDisactiveVFX?.Invoke();
        _playerController.PlayerData.WalkingSpeed = _playerController.PlayerData.DefaultWalkingSpeed;
        _playerController.MeteoraModeData.IsMeteora = false;
        _playerController.PlayerRigidBody2D.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
        _playerController.PlayerRigidBody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        DisableMeteora();

    }
    
    private void MeteoraVelocity()
    {
        _playerController.PlayerData.WalkingSpeed = _playerController.MeteoraModeData.MeteoraVelocity;
    }


    private void DisableMeteora()
    {
        if (_playerController.DefaultCharacterData.IsDefaultCharacter)
        {
            _playerController.DefaultCharacterData.IsDefaultCharacter = false;
            _playerController.ChangeCharacter(false, 0);
        }
        else if (_playerController.SpaceShipCharacterData.IsSpaceShip)
        {
            _playerController.SpaceShipCharacterData.IsSpaceShip = false;
            _playerController.ChangeCharacter(false, 1);
        }
        else if (_playerController.GearModeData.IsGearMode)
        {
            _playerController.GearModeData.IsGearMode = false;
            _playerController.ChangeCharacter(false, 2);
        }
        else if (_playerController.UfoCharacterData.IsUfo)
        {
            _playerController.UfoCharacterData.IsUfo = false;
            _playerController.ChangeCharacter(false, 3);
        }
        
    }
}
