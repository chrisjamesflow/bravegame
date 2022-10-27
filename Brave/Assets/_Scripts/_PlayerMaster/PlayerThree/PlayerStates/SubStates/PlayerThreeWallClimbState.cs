using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThreeWallClimbState : PlayerThreeTouchingWallState
{
    public PlayerThreeWallClimbState(PlayerThree player, PlayerThreeStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.Audio.Play("Climb");
    }

    public override void Exit()
    {
        base.Exit();
        player.Audio.Stop("Climb");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isExitingState)
        {
            Movement?.SetVelocityY(playerData.wallClimbVelocity);

            if (yInput != 1)
            {
                stateMachine.ChangeState(player.WallGrabState);
            }
        }
    }
}
