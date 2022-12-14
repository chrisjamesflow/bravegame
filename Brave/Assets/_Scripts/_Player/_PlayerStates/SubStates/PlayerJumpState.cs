using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    private int amountOfJumpsLeft;
    private int jumpAmount;
    public static bool doubleJumpAbility;

    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        amountOfJumpsLeft = jumpAmount;
        jumpAmount = 1;
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

    public void ResetAmountOfJumpsLeft() => amountOfJumpsLeft = jumpAmount;

    public void DecreaseAmountOfJumpsLeft() => amountOfJumpsLeft--;

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (doubleJumpAbility)
        {
            jumpAmount = 2;
        }
    }
}
