using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bully_LookForPlayerState : LookForPlayerState
{
    private Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private Movement movement;

    private Bully enemy;

    public Bully_LookForPlayerState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_LookForPlayer stateData, Bully enemy) : base(entity, stateMachine, animBoolName, stateData)
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
        enemy.Audio.Play("Call");
    }

    public override void Exit()
    {
        base.Exit();
        enemy.Audio.Stop("Call");
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
