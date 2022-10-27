using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf_JumpUpState : JumpUpState
{
    private Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private Movement movement;

    private Wolf enemy;

    public Wolf_JumpUpState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_JumpUpState stateData, Wolf enemy) : base(entity, stateMachine, animBoolName, stateData)
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
    }

    public override void Exit()
    {
        base.Exit();
        enemy.Audio.Play("Land");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (performCloseRangeActionLow || performCloseRangeActionMid)
        {
            stateMachine.ChangeState(enemy.meleeAttackState);
        }
        else if (isDetectingLedge && !isDetectingWall)
        {
            if(isPlayerInLowAgroRange || isPlayerInMidAgroRange || isPlayerInHighAgroRange || isPlayerBehindAgroRangeBelow || isPlayerBehindAgroRangeLow || isPlayerBehindAgroRangeMid || isPlayerBehindAgroRangeHigh)
            {
                stateMachine.ChangeState(enemy.chargeState);

                if (isPlayerBehindAgroRangeBelow || isPlayerBehindAgroRangeLow || isPlayerBehindAgroRangeMid || isPlayerBehindAgroRangeHigh)
                {
                    Movement?.Flip();
                }
            }
            else
            {
                stateMachine.ChangeState(enemy.moveState);
            }
        }
        else if (isDetectingWall/* && unableToJumpUp*/)
        {
            stateMachine.ChangeState(enemy.lookForPlayerState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
