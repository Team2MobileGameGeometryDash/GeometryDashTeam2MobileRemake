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
        AllStates.Add(PlayerState.SpaceshipCharacter, new SpaceShipCharacter(PlayerState.SpaceshipCharacter, this));
        AllStates.Add(PlayerState.GearModeCharacter, new GearModeCharacter(PlayerState.GearModeCharacter, this));
        AllStates.Add(PlayerState.UfoCharacter, new UfoCharacter(PlayerState.UfoCharacter, this));
        AllStates.Add(PlayerState.MeteoraMode, new MeteoraMode(PlayerState.MeteoraMode, this));
        AllStates.Add(PlayerState.Win, new WinState(PlayerState.Win, this));
        AllStates.Add(PlayerState.Death, new DeathState(PlayerState.Death, this));


        CurrentState = AllStates[PlayerState.DefaultCharacter];
        CurrentState.OnEnter();
    }


}



public enum PlayerState
{
    DefaultCharacter,
    SpaceshipCharacter,
    GearModeCharacter,
    UfoCharacter,
    MeteoraMode,
    Win,
    Death,
}