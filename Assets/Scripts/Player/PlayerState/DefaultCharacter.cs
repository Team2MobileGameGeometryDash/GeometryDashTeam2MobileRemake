using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultCharacter : State<PlayerState>
{
    PlayerController _playerController;
    PlayerStateManager _playerStateManager;
    public Vector2 UpAxis = Vector2.up;
    public DefaultCharacter(PlayerState playerState, StatesMachine<PlayerState> stateManager = null) : base(playerState, stateManager)
    {
        _playerStateManager = (PlayerStateManager)m_stateMachine;
        GameManager.Instance.observerPattern.Register(GameEventEnum.GameEvent.GravitySwitch, HandleJumping);
    }


    public override void OnEnter()
    {
        base.OnEnter();
        
        if(_playerController==null) _playerController = _playerStateManager._playerController;
        _playerController.ChangeCharacter(true,0);

    }


    public override void OnUpdate()
    {
        base.OnUpdate();

        if (_playerController.isGrounded())
        {
            _playerController.RotationWhenGrounded(_playerController.Ships[0]); //Da migliorare la parentesi
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (!_playerController.data.isGravityChange)
                    GameManager.Instance.observerPattern.TriggerEvent(GameEventEnum.GameEvent.GravitySwitch, 1f);
                else
                    GameManager.Instance.observerPattern.TriggerEvent(GameEventEnum.GameEvent.GravitySwitch, -1f);
            }
        }
        else _playerController.RotationNotGrounded(_playerController.Ships[0], !_playerController.data.isGravityChange);
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        HandleMovement();
    }

    public override void OnExit()
    {
        base.OnExit();
        _playerController.ChangeCharacter(false, 0);
    }


    private void HandleMovement()
    {
        float speed = _playerController.data.WalkingSpeed;
        _playerController.PlayerRigidBody2D.velocity = new Vector2(speed * UnityEngine.Time.fixedDeltaTime, _playerController.PlayerRigidBody2D.velocity.y);
        
    }


    float Time() => _playerController.data.Time = UnityEngine.Time.deltaTime;
    public void HandleJumping(object[] jumpDirection)
    {
        float jumpForce = Mathf.Sqrt(_playerController.data.JumpHeight * 2f * -Physics2D.gravity.y );
        float jump = jumpForce - (9.81f * Time());
        _playerController.PlayerRigidBody2D.velocity = new Vector2(_playerController.PlayerRigidBody2D.velocity.x, jump * ((float)jumpDirection[0]));
            
    }





    

    
}