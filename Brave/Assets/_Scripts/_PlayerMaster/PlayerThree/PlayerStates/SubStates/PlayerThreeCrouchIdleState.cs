using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThreeCrouchIdleState : PlayerThreeGroundedState
{
    public PlayerThreeCrouchIdleState(PlayerThree player, PlayerThreeStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Movement?.SetVelocityZero();
        player.SetColliderHeight(playerData.crouchColliderHeight);
    }

    public override void Exit()
    {
        base.Exit();

        player.SetColliderHeight(playerData.standColliderHeight);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isExitingState)
        {
            if(xInput != 0)
            {
                stateMachine.ChangeState(player.CrouchMoveState);
            }
            else if(/*yInput != -1*/!crouchInput && !isTouchingCeiling)
            {
                stateMachine.ChangeState(player.IdleState);
            }
        }
    }
}
