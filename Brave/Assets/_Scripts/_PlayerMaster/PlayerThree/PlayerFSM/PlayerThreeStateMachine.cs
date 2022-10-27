using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThreeStateMachine
{
    public PlayerThreeState CurrentState { get; private set; }

    public void Initialize(PlayerThreeState startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    public void ChangeState(PlayerThreeState newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}
