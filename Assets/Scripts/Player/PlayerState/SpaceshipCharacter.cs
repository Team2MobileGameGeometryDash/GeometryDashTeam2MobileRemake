using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipCharacter : State<PlayerState>
{
    PlayerController _playerController;
    PlayerStateManager _playerStateManager;

    public SpaceshipCharacter(PlayerState playerState, StatesMachine<PlayerState> stateManager = null) : base(playerState, stateManager)
    {
        _playerStateManager = (PlayerStateManager)m_stateMachine;
        
    }


    public override void OnEnter()
    {
        base.OnEnter();
        if (_playerController == null) _playerController = _playerStateManager.PlayerController;
        _playerController.ChangeCharacter(true, 1);
        
        //Debug.Log(" SECOND STATE ON ENTER _PlayerController : " + _playerController);
        //Debug.Log(_playerController);

    }


    public override void OnUpdate()
    {
        base.OnUpdate();

        HandleAllMouvement();

        if (!_playerController.Data.IsSpaceShip)
            _playerStateManager.ChangeState(PlayerState.DefaultCharacter);

       


    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        _playerController.PlayerMouvement.HandleMouvementBaseCharacter();
    }


    public override void OnExit()
    {
        base.OnExit();
        _playerController.ChangeCharacter(false, 1);
        

    }



    private void HandleAllMouvement()
    {

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {

            HandleJumpingBaseCharacter();
            Debug.Log("non ");


        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Debug.Log("sto premendo");
            HandleJumpingShip();


        }

    }

    public void HandleJumpingBaseCharacter()
    {
        float jumpTime = Mathf.Sqrt(_playerController.Data.JumpHeight * 2f * -Physics2D.gravity.y);
        float jump = jumpTime - (9.81f * Time());
        _playerController.PlayerRigidBody2D.velocity = new Vector2(_playerController.PlayerRigidBody2D.velocity.x, jump );

    }
    float Time() => _playerController.Data.Time = UnityEngine.Time.deltaTime;

    private void HandleJumpingShip()
    {
        //_playerController.PlayerRigidBody2D.velocity = new Vector2(_playerController.PlayerRigidBody2D.velocity.x, _playerController.PlayerRigidBody2D.velocity.y * UnityEngine.Time.deltaTime * _playerController.Data.JumpImpulse);
        _playerController.PlayerRigidBody2D.AddRelativeForce(Vector2.up * _playerController.Data.JumpImpulse ,ForceMode2D.Force);
    }
}