using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThreeWallGrabState : PlayerThreeTouchingWallState
{
    private Vector2 holdPosition;

    public PlayerThreeWallGrabState(PlayerThree player, PlayerThreeStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        player.Audio.Play("Ledge");

        holdPosition = player.transform.position;

        HoldPosition();
    }

    public override void Exit()
    {
        base.Exit();
        player.Audio.Stop("Ledge");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.RB.gravityScale = playerData.gravity;

        if (!isExitingState)
        {
            HoldPosition();

            if (yInput > 0)
            {
                stateMachine.ChangeState(player.WallClimbState);
            }
            else if (yInput < 0 || !grabInput)
            {
                stateMachine.ChangeState(player.WallSlideState);
            }
        }
    }

    private void HoldPosition()
    {
        player.transform.position = holdPosition;

        Movement?.SetVelocityX(0f);
        Movement?.SetVelocityY(0f);
        player.Audio.Stop("Slide");
        player.Audio.Stop("Climb");
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
