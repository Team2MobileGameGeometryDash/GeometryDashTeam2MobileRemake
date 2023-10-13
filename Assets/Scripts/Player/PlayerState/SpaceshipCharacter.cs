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

            GameManager.Instance.ObserverPattern.TriggerEvent(GameEventEnum.GameEvent.DefaultJump, 1f);



        }
        if (Input.GetKey(KeyCode.Mouse0))
        {

            GameManager.Instance.ObserverPattern.TriggerEvent(GameEventEnum.GameEvent.ShipJump);


        }

    }

    
  
}