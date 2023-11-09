using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfoCharacterState : State<PlayerState>
{
    PlayerController _playerController;
    PlayerStateManager _playerStateManager;
    PlayerInputManager _playerInputManager;

    public UfoCharacterState(PlayerState playerState, StatesMachine<PlayerState> stateManager = null) : base(playerState, stateManager)
    {
        _playerStateManager = (PlayerStateManager)m_stateMachine;

    }

    public override void OnEnter()
    {
        base.OnEnter();

        if (_playerController == null) _playerController = _playerStateManager.PlayerController;
        _playerInputManager = _playerController.PlayerInputManager;
        ActionManager.OnChangeShip?.Invoke(new UfoInput());
        _playerController.PlayerSOUfoCharacter.ApplyDefaultParameters(_playerController);
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





    private void HandleAllMouvement()
    {
        _playerController.PlayerMouvement.HandleMouvementBaseCharacter(_playerController.PlayerSOBaseData);

        if (PlayerInputManager.IsTouchEnded)
        {
            _playerController.PlayerMouvement.HandleJumpingUfoCharacter(_playerController.PlayerSOUfoCharacter);

        }

    }





}
