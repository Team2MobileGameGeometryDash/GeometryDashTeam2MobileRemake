using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotCharacterState : State<PlayerState>
{
    PlayerController _playerController;
    PlayerStateManager _playerStateManager;
    PlayerInputManager _playerInputManager;

    public RobotCharacterState(PlayerState playerState, StatesMachine<PlayerState> stateManager = null) : base(playerState, stateManager)
    {
        _playerStateManager = (PlayerStateManager)m_stateMachine;

    }

    public override void OnEnter()
    {
        base.OnEnter();
        if (_playerController == null) _playerController = _playerStateManager.PlayerController;
        ActionManager.OnChangeShip?.Invoke(new RobotInput());
        _playerController.PlayerSORobotCharacter.ApplyDefaultParameters(_playerController);
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


    public override void OnExit()
    {
        base.OnExit();

    }



    private void HandleMouvement()
    {
        _playerController.PlayerMouvement.HandleMouvementBaseCharacter(_playerController.PlayerSOBaseData);
        _playerController.PlayerMouvement.HandleJumpRobotCharacter(_playerController.PlayerSORobotCharacter);
    }



}
