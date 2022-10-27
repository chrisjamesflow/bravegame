using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwoCrouchMoveState : PlayerTwoGroundedState
{
    public PlayerTwoCrouchMoveState(PlayerTwo player, PlayerTwoStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.Audio.Play("Crawl");

        player.SetColliderHeight(playerData.crouchColliderHeight);
    }

    public override void Exit()
    {
        base.Exit();
        player.Audio.Stop("Crawl");

        player.SetColliderHeight(playerData.standColliderHeight);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isExitingState)
        {
            Movement?.SetVelocityX(playerData.crouchMovementVelocity * Movement.FacingDirection);
            Movement?.CheckIfShouldFlip(xInput);

            if (xInput == 0)
            {
                stateMachine.ChangeState(player.CrouchIdleState);
            }
            else if (/*yInput != -1*/!crouchInput && !isTouchingCeiling)
            {
                stateMachine.ChangeState(player.MoveState);
            }
        }
    }
}
