using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwoStateMachine
{
    public PlayerTwoState CurrentState { get; private set; }

    public void Initialize(PlayerTwoState startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    public void ChangeState(PlayerTwoState newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}
