using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFourStateMachine
{
    public PlayerFourState CurrentState { get; private set; }

    public void Initialize(PlayerFourState startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    public void ChangeState(PlayerFourState newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}
