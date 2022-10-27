using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFourAttackState : PlayerFourAbilityState
{
    private WeaponFour weapon;
    private float velocityToSet;
    private bool setVelocity;

    public PlayerFourAttackState(PlayerFour player, PlayerFourStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        setVelocity = false;
        weapon.EnterWeapon();
    }

    public override void Exit()
    {
        base.Exit();
        weapon.ExitWeapon();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (setVelocity)
        {

            Movement?.SetVelocityX(velocityToSet * Movement.FacingDirection);
        }
    }

    public void SetWeapon(WeaponFour weapon)
    {
        this.weapon = weapon;
        weapon.InitializeWeapon(this, core);
    }

    public void SetPlayerVelocity(float velocity)
    {
        Movement?.SetVelocityX(velocity * Movement.FacingDirection);

        velocityToSet = velocity;
        setVelocity = true;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        isAbilityDone = true;
    }
}
