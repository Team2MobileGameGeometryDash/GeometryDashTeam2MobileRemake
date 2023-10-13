using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : StatesMachine<PlayerState>
{
    public PlayerController PlayerController;

    public PlayerStateManager(PlayerController playerController, Dictionary<PlayerState, State<PlayerState>> listOfSTtes = null, State<PlayerState> currentState = null, State<PlayerState> nextState = null)
    {
        PlayerController = playerController;
        InitStatesManager();
    }

    

    protected override void InitStates()
    {
        AllStates.Add(PlayerState.DefaultCharacter, new DefaultCharacter(PlayerState.DefaultCharacter,this));
        AllStates.Add(PlayerState.SpaceshipCharacter, new SpaceshipCharacter(PlayerState.SpaceshipCharacter, this));


        CurrentState = AllStates[PlayerState.DefaultCharacter];
        CurrentState.OnEnter();
    }


}



public enum PlayerState
{
    DefaultCharacter,
    SpaceshipCharacter,

}