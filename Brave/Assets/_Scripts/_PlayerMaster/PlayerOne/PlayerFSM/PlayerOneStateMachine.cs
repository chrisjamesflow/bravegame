using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneStateMachine
{
    public PlayerOneState CurrentState { get; private set; }

    public void Initialize(PlayerOneState startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    public void ChangeState(PlayerOneState newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}
