using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFiveJumpState : PlayerFiveAbilityState
{
    private int amountOfJumpsLeft;

    public PlayerFiveJumpState(PlayerFive player, PlayerFiveStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
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
