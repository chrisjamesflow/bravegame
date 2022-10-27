using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf_LookForPlayerState : LookForPlayerState
{
    private Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private Movement movement;

    private Wolf enemy;

    public Wolf_LookForPlayerState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_LookForPlayer stateData, Wolf enemy) : base(entity, stateMachine, animBoolName, stateData)
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
        enemy.Audio.Play("Sniff");
    }

    public override void Exit()
    {
        base.Exit();
        enemy.Audio.Stop("Sniff");
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
        else if (isAllTurnsTimeDone)
        {
            stateMachine.ChangeState(enemy.moveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
