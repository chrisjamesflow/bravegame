using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf_PlayerDetectedState : PlayerDetectedState
{
    private Wolf enemy;

    public Wolf_PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetected stateData, Wolf enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (performCloseRangeActionLow || performCloseRangeActionMid)
        {
            stateMachine.ChangeState(enemy.meleeAttackState);
        }
        else if (performLongRangeAction && (!isDetectingWall || !isDetectingLedge))
        {
            stateMachine.ChangeState(enemy.chargeState);
        }
        else if (!isPlayerInLowMaxAgroRange && !isPlayerInMidMaxAgroRange && !isPlayerInHighMaxAgroRange && !isPlayerBehindAgroRange)
        {
            stateMachine.ChangeState(enemy.lookForPlayerState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
