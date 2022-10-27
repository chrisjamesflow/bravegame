using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneGlideState : PlayerOneInAirState
{
    private bool isGrounded;
    private bool glideInput;

    public PlayerOneGlideState(PlayerOne player, PlayerOneStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
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
        player.Audio.Play("GlideSoar");
        player.Audio.Play("GlideOpen");
    }

    public override void Exit()
    {
        base.Exit();
        player.Audio.Stop("GlideSoar");
        player.Audio.Stop("GlideOpen");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        glideInput = player.InputHandler.GlideInput;

        player.RB.gravityScale = playerData.glideGravity;

        if (!isExitingState)
        {
            Movement?.SetVelocityY(playerData.glideVelocity);
            Movement?.SetVelocityX(playerData.movementVelocity * xInput);
            Movement?.CheckIfShouldFlip(xInput);

            if (!isGrounded && !glideInput)
            {
                stateMachine.ChangeState(player.InAirState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
