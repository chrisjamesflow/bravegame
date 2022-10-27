using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFiveStateMachine
{
    public PlayerFiveState CurrentState { get; private set; }

    public void Initialize(PlayerFiveState startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    public void ChangeState(PlayerFiveState newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}
