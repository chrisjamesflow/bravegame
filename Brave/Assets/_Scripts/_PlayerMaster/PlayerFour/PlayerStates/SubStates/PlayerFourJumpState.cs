using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFourJumpState : PlayerFourAbilityState
{
    private int amountOfJumpsLeft;

    public PlayerFourJumpState(PlayerFour player, PlayerFourStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        amountOfJumpsLeft = 1;
    }
    public override void Enter()
    {
        base.Enter();

        player.InputHandler.UseJumpInput();
        Movement?.SetVelocityY(playerData.jumpVelocity);
        isAbilityDone = true;
        amountOfJumpsLeft--;
        player.InAirState.SetIsJumping();
    }

    public bool CanJump()
    {
        if(amountOfJumpsLeft > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ResetAmountOfJumpsLeft() => amountOfJumpsLeft = 1;

    public void DecreaseAmountOfJumpsLeft() => amountOfJumpsLeft--;
}
