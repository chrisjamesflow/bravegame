using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFourWallSlideState : PlayerFourTouchingWallState
{
    public PlayerFourWallSlideState(PlayerFour player, PlayerFourStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.Audio.Play("Slide");
    }

    public override void Exit()
    {
        base.Exit();
        player.Audio.Stop("Slide");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isExitingState)
        {
            Movement?.SetVelocityY(-playerData.wallSlideVelocity);

            if (grabInput && yInput == 0)
            {
                stateMachine.ChangeState(player.WallGrabState);
            }
        }
    }
}
