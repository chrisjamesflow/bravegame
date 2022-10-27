using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneMoveState : PlayerOneGroundedState
{
    public PlayerOneMoveState(PlayerOne player, PlayerOneStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        player.Audio.Play("Move");
    }

    public override void Exit()
    {
        base.Exit();
        player.Audio.Stop("Move");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Movement?.CheckIfShouldFlip(xInput);

        Movement?.SetVelocityX(playerData.movementVelocity * xInput);

        if (!isExitingState)
        {
            if (xInput == 0)
            {
                stateMachine.ChangeState(player.IdleState);
            }
            else if (/*yInput == -1*/crouchInput)
            {
                stateMachine.ChangeState(player.CrouchMoveState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
