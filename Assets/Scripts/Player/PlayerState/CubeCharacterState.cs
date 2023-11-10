using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCharacterState : State<PlayerState> 
{
    PlayerController _playerController;
    PlayerStateManager _playerStateManager;
    PlayerInputManager _playerInputManager;

    public CubeCharacterState(PlayerState playerState, StatesMachine<PlayerState> stateManager = null) : base(playerState, stateManager)
    {
        _playerStateManager = (PlayerStateManager)m_stateMachine;
        
    }


    public override void OnEnter()
    {
        base.OnEnter();
        
        if(_playerController == null) _playerController = _playerStateManager.PlayerController;
        _playerInputManager = _playerController.PlayerInputManager;  
        PlayerInputManager.IsTouchEnded = false;
        ActionManager.OnChangeShip?.Invoke(new BaseCharacterInput());
        _playerController.PlayerSOCubeCharacter.ApplyDefaultParameters(_playerController);
        
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (_playerController.PlayerSOCubeCharacter.IsGravityChange)
            _playerController.VFXManager.CubeCollision.transform.localPosition = new Vector3(-0.26f,0.31f, 0);
        else
            _playerController.VFXManager.CubeCollision.transform.localPosition = new Vector3(-0.26f,-0.31f,0);
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


        if (_playerController.PlayerMouvement.isGrounded(_playerController.PlayerSOBaseData))
        {
            //maybe better place
            ActionManager.OnCubeCollision?.Invoke();
            if (PlayerInputManager.IsTouchEnded || PlayerInputManager.IsTouchStationary)
            {
                _playerController.PlayerMouvement.HandleJumpingBaseCharacter(_playerController.PlayerSOCubeCharacter);
            }
            _playerController.PlayerMouvement.RotationWhenGroundedBaseCharacter(_playerController.PlayerSOCubeCharacter.SpriteRotation);
        }
        else
        {
            //maybe better place
            ActionManager.OnNoCubeCollision?.Invoke();
            _playerController.PlayerMouvement.RotationNotGroundedBaseCharacter(_playerController.PlayerSOCubeCharacter);
        }
    }



}