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
        AllStates.Add(PlayerState.CubeCharacter, new CubeCharacterState(PlayerState.CubeCharacter,this));
        AllStates.Add(PlayerState.SpaceshipCharacter, new SpaceShipCharacterState(PlayerState.SpaceshipCharacter, this));
        AllStates.Add(PlayerState.GearModeCharacter, new GearModeCharacterState(PlayerState.GearModeCharacter, this));
        AllStates.Add(PlayerState.UfoCharacter, new UfoCharacterState(PlayerState.UfoCharacter, this));
        AllStates.Add(PlayerState.RobotCharacter, new RobotCharacterState(PlayerState.RobotCharacter, this));
        AllStates.Add(PlayerState.MeteoraMode, new MeteoraModeState(PlayerState.MeteoraMode, this));
        AllStates.Add(PlayerState.Win, new WinState(PlayerState.Win, this));
        AllStates.Add(PlayerState.Death, new DeathState(PlayerState.Death, this));


        CurrentState = AllStates[PlayerState.CubeCharacter];
        CurrentState.OnEnter();
    }


}



public enum PlayerState
{
    CubeCharacter,
    SpaceshipCharacter,
    GearModeCharacter,
    UfoCharacter,
    RobotCharacter,
    MeteoraMode,
    Win,
    Death,
}