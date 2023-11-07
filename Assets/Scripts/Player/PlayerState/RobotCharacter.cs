using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotCharacter : State<PlayerState>
{
    PlayerController _playerController;
    PlayerStateManager _playerStateManager;
    PlayerInputManager _playerInputManager;

    public RobotCharacter(PlayerState playerState, StatesMachine<PlayerState> stateManager = null) : base(playerState, stateManager)
    {
        _playerStateManager = (PlayerStateManager)m_stateMachine;

    }

    public override void OnEnter()
    {
        base.OnEnter();
        if (_playerController == null) _playerController = _playerStateManager.PlayerController;
        ActionManager.OnChangeShip?.Invoke(new RobotInput());
        _playerController.ChangeCharacter(true, 4);
        _playerController.PlayerRigidBody2D.gravityScale = _playerController.RobotData.GravityScale;
        _playerController.RobotData.IsRobot = true;
    }


    public override void OnUpdate()
    {
        base.OnUpdate();
        HandleMouvement();
        ChangeState();
    }



    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.TryGetComponent(out Death death)) _playerStateManager.ChangeState(PlayerState.Death);

    }


    public override void OnExit()
    {
        base.OnExit();

        if (!_playerController.PlayerData.isWin && !_playerController.MeteoraModeData.IsMeteora)
        {
            _playerController.ChangeCharacter(false, 4);
            _playerController.RobotData.IsRobot = false;
        }
    }



    private void HandleMouvement()
    {
        _playerController.PlayerMouvement.HandleMouvementBaseCharacter();
        _playerController.PlayerMouvement.HandleJumpRobotCharacter();
    }


    private void ChangeState()
    {
        if (_playerController.DefaultCharacterData.IsDefaultCharacter)
            _playerStateManager.ChangeState(PlayerState.DefaultCharacter);
        else if (_playerController.SpaceShipCharacterData.IsSpaceShip)
            _playerStateManager.ChangeState(PlayerState.SpaceshipCharacter);
        else if (_playerController.UfoCharacterData.IsUfo)
            _playerStateManager.ChangeState(PlayerState.UfoCharacter);
        else if (_playerController.GearModeData.IsGearMode)
            _playerStateManager.ChangeState(PlayerState.GearModeCharacter);
        else if (_playerController.MeteoraModeData.IsMeteora)
            _playerStateManager.ChangeState(PlayerState.MeteoraMode);
    }
}
