using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoraModeState : State<PlayerState>
{
    PlayerController _playerController;
    PlayerStateManager _playerStateManager;
    PlayerInputManager _playerInputManager;

    public MeteoraModeState(PlayerState playerState, StatesMachine<PlayerState> stateManager = null) : base(playerState, stateManager)
    {
        _playerStateManager = (PlayerStateManager)m_stateMachine;

    }

    public override void OnEnter()
    {
        base.OnEnter();
        if (_playerController == null) _playerController = _playerStateManager.PlayerController;
        _playerInputManager = _playerController.PlayerInputManager;
        ActionManager.OnMeteoraActiveVFX?.Invoke();
        _playerController.PlayerRigidBody2D.constraints = RigidbodyConstraints2D.FreezePositionY;
        Debug.Log("meteora");

    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        ChangeState();
            

    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        _playerController.PlayerMouvement.HandleMouvementBaseCharacter(_playerController.PlayerSOBaseData);
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
        _playerController.PlayerSOBaseData.WalkingSpeed = _playerController.PlayerSOBaseData.DefaultWalkingSpeed;
        _playerController.PlayerRigidBody2D.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
        _playerController.PlayerRigidBody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        

    }


    private void ChangeState()
    {
        if (!PlayerInputManager.IsTouchStationary)
        {
            if (_playerController.PlayerSOCubeCharacter.isCubeMeteora)
            {
                _playerStateManager.ChangeState(PlayerState.CubeCharacter);
                _playerController.PlayerSOCubeCharacter.isCubeMeteora = false;
            }
            else if (_playerController.PlayerSOShipCharacter.isShipMeteora)
            {
                _playerStateManager.ChangeState(PlayerState.SpaceshipCharacter);
                _playerController.PlayerSOShipCharacter.isShipMeteora = false;
            }
            else if (_playerController.PlayerSOGearModeCharacter.isGearModeMeteora)
            {
                _playerStateManager.ChangeState(PlayerState.GearModeCharacter);
                _playerController.PlayerSOGearModeCharacter.isGearModeMeteora = false;
            }
            else if (_playerController.PlayerSOUfoCharacter.isUfoMeteora)
            {
                _playerStateManager.ChangeState(PlayerState.UfoCharacter);
                _playerController.PlayerSOUfoCharacter.isUfoMeteora = false;
            }
            else if (_playerController.PlayerSORobotCharacter.isRobotMeteora)
            {
                _playerStateManager.ChangeState(PlayerState.RobotCharacter);
                _playerController.PlayerSORobotCharacter.isRobotMeteora = false;
            }
        }
    }




}
