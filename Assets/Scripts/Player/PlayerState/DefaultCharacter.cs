using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultCharacter : State<PlayerState> 
{
    PlayerController _playerController;
    PlayerStateManager _playerStateManager;
    PlayerInputManager _playerInputManager;

    public DefaultCharacter(PlayerState playerState, StatesMachine<PlayerState> stateManager = null) : base(playerState, stateManager)
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
        _playerController.ChangeCharacter(true,0);
        _playerController.PlayerRigidBody2D.gravityScale = _playerController.DefaultCharacterData.GravityScale;
        _playerController.PlayerData.IsGravityChange = false;
        _playerController.DefaultCharacterData.IsDefaultCharacter = true;

    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        ChangeState();

        if (!_playerController.PlayerData.IsGravityChange)
            _playerController.VFXManager.CubeCollision.transform.localPosition = new Vector3(-0.26f, -0.31f, 0f);
        else
            _playerController.VFXManager.CubeCollision.transform.localPosition = new Vector3(-0.26f, 0.29f, 0);
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
        if (!_playerController.PlayerData.isWin && !_playerController.MeteoraModeData.IsMeteora)
        {
            _playerController.ChangeCharacter(false, 0);
            _playerController.DefaultCharacterData.IsDefaultCharacter = false;
            
        }

    }



    private void HandleAllMouvement()
    {
        _playerController.PlayerMouvement.HandleMouvementBaseCharacter();


        if (_playerController.PlayerMouvement.isGrounded())
        {
            //maybe better place
            ActionManager.OnCubeCollision?.Invoke();
            if (PlayerInputManager.IsTouchEnded || PlayerInputManager.IsTouchStationary)
            {
                _playerController.PlayerMouvement.HandleJumpingBaseCharacter();
            }
            _playerController.PlayerMouvement.RotationWhenGroundedBaseCharacter(_playerController.PlayerData.Ships[0]);
        }
        else
        {
            //maybe better place
            ActionManager.OnNoCubeCollision?.Invoke();
            _playerController.PlayerMouvement.RotationNotGroundedBaseCharacter(_playerController.PlayerData.Ships[0], !_playerController.PlayerData.IsGravityChange);
        }
    }


   
    private void ChangeState()
    {
        if (_playerController.SpaceShipCharacterData.IsSpaceShip)
            _playerStateManager.ChangeState(PlayerState.SpaceshipCharacter);
        else if (_playerController.GearModeData.IsGearMode)
            _playerStateManager.ChangeState(PlayerState.GearModeCharacter);
        else if (_playerController.UfoCharacterData.IsUfo)
            _playerStateManager.ChangeState(PlayerState.UfoCharacter);
        else if (_playerController.RobotData.IsRobot)
            _playerStateManager.ChangeState(PlayerState.RobotCharacter);
        else if (_playerController.MeteoraModeData.IsMeteora)
            _playerStateManager.ChangeState(PlayerState.MeteoraMode);
    }

}