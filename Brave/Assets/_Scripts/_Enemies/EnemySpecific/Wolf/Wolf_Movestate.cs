using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf_MoveState : MoveState
{
    private Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private Movement movement;

    private Wolf enemy;

    public Wolf_MoveState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_MoveState stateData, Wolf enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.Audio.Play("Move");
    }

    public override void Exit()
    {
        base.Exit();
        enemy.Audio.Stop("Move");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isPlayerInLowAgroRange || isPlayerInMidAgroRange || isPlayerInHighAgroRange)
        {
            stateMachine.ChangeState(enemy.playerDetectedState);
        }
        else if (isPlayerBehindAgroRange)
        {
            Movement.Flip();
            stateMachine.ChangeState(enemy.playerDetectedState);
        }
        else if (isDetectingJumpWall && !unableToJumpUp)
        {
            stateMachine.ChangeState(enemy.jumpUpState);
        }
        else if (!isDetectingLedge && ableToJumpDown)
        {
            stateMachine.ChangeState(enemy.jumpDownState);
        }
        else if (isDetectingWall || !isDetectingLedge)
        {
            enemy.idleState.SetFlipAfterIdle(true);
            stateMachine.ChangeState(enemy.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
