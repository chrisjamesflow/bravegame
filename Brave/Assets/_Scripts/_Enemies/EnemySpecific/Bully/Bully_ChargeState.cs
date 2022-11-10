using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bully_ChargeState : ChargeState
{
    private Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private Movement movement;

    private Bully enemy;

    public Bully_ChargeState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_ChargeState stateData, Bully enemy) : base(entity, stateMachine, animBoolName, stateData)
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
        enemy.Audio.Play("Charge");
    }

    public override void Exit()
    {
        base.Exit();
        enemy.Audio.Stop("Charge");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (performCloseRangeActionLow || performCloseRangeActionMid || performCloseRangeActionBehindLow || performCloseRangeActionBehindMid)
        {
            stateMachine.ChangeState(enemy.meleeAttackState);
        }
        else if (isPlayerBehindAgroRangeBelow || isPlayerBehindAgroRangeLow || isPlayerBehindAgroRangeMid || isPlayerBehindAgroRangeHigh)
        {
            Movement?.Flip();
        }
        else if (isDetectingJumpWall && !unableToJumpUp && !isPlayerInLowAgroRange)
        {
            stateMachine.ChangeState(enemy.jumpUpState);
        }
        else if (!isDetectingLedge)
        {
            if (ableToJumpDown)
            {
                stateMachine.ChangeState(enemy.jumpDownState);
            }
            else
            {
                stateMachine.ChangeState(enemy.lookForPlayerState);
            }
        }
        else if (isDetectingWall)
        {
            stateMachine.ChangeState(enemy.lookForPlayerState);
        }
        else if (isChargeTimeOver)
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
}
