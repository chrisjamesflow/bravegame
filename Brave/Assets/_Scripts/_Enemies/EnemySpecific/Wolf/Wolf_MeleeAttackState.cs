using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf_MeleeAttackState : MeleeAttackState
{
    private Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private Movement movement;

    private Wolf enemy;

    public Wolf_MeleeAttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_MeleeAttackState stateData, Wolf enemy) : base(entity, stateMachine, animBoolName, attackPosition, stateData)
    {
        this.enemy = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        enemy.Audio.Play("Attack");

        startTime = Time.unscaledTime;
    }

    public override void Exit()
    {
        base.Exit();
        enemy.Audio.Stop("Attack");
    }

    public override void FinishAttack()
    {
        base.FinishAttack();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Movement.SetVelocityX(stateData.attackSpeed * Movement.FacingDirection);

        if (Time.unscaledTime >= startTime + stateData.attackTime && isDetectingLedge)
        {
            Movement.SetVelocityX(0);
        }
        else if (!ableToJumpDown)
        {
            Movement.SetVelocityX(0);
        }

        if (isAnimationFinished)
        {
            if (isPlayerInLowAgroRange || isPlayerInMidAgroRange || isPlayerInHighAgroRange)
            {
                stateMachine.ChangeState(enemy.playerDetectedState);
            }
            else
            {
                stateMachine.ChangeState(enemy.lookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();
    }
}
