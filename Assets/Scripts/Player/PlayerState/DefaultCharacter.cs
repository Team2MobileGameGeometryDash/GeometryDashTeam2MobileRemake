using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultCharacter : State<PlayerState>
{
    PlayerController _playerController;
    PlayerStateManager _playerStateManager;


    public DefaultCharacter(PlayerState playerState, StatesMachine<PlayerState> stateManager = null) : base(playerState, stateManager)
    {
        _playerStateManager = (PlayerStateManager)m_stateMachine;
        
    }


    public override void OnEnter()
    {
        base.OnEnter();
        
        if(_playerController==null) _playerController = _playerStateManager.PlayerController;
        _playerController.ChangeCharacter(true,0);
        _playerController.PlayerRigidBody2D.gravityScale = _playerController.DefaultCharacterData.GravityScale;

    }


    public override void OnUpdate()
    {
        base.OnUpdate();
        HandleRotation();
        if (_playerController.SpaceShipCharacterData.IsSpaceShip)
            _playerStateManager.ChangeState(PlayerState.SpaceshipCharacter);
        else if (_playerController.GearModeData.IsGearMode)
            _playerStateManager.ChangeState(PlayerState.GearModeCharacter);

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


    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        HandleMouvement();
        _playerController.PlayerMouvement.HandleMouvementBaseCharacter();
    }

    public override void OnExit()
    {
        base.OnExit();
        _playerController.ChangeCharacter(false, 0);
        _playerController.DefaultCharacterData.IsDefaultCharacter = false;
        
    }



    private void HandleMouvement()
    {
        if (_playerController.isGrounded())
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                if (!_playerController.DefaultCharacterData.IsGravityChange)
                    GameManager.Instance.ObserverPatternPlayer.TriggerEvent(GameEventEnum.PlayerGameEvent.DefaultJump, 1f);
                else
                    GameManager.Instance.ObserverPatternPlayer.TriggerEvent(GameEventEnum.PlayerGameEvent.DefaultJump, -1f);
            }
        }
    }

    private void HandleRotation()
    {
        if (_playerController.isGrounded())
            _playerController.PlayerMouvement.RotationWhenGroundedBaseCharacter(_playerController.Data.Ships[0]);
        else
            _playerController.PlayerMouvement.RotationNotGroundedBaseCharacter(_playerController.Data.Ships[0], !_playerController.DefaultCharacterData.IsGravityChange);
    }       

    

    
}