using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwoLandState : PlayerTwoGroundedState
{
    public PlayerTwoLandState(PlayerTwo player, PlayerTwoStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.Audio.Play("Land");
    }

    public override void Exit()
    {
        base.Exit();
        player.Audio.Stop("Land");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isExitingState)
        {
            if (xInput != 0)
            {
                stateMachine.ChangeState(player.MoveState);
                player.Audio.Play("Land");
            }
            else if (isAnimationFinished)
            {
                stateMachine.ChangeState(player.IdleState);
            }
        }
    }
}
