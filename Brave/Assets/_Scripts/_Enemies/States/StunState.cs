using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunState : State
{
    private Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private Movement movement;

    private CollisionSenses CollisionSenses { get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses); }
    private CollisionSenses collisionSenses;

    protected D_StunState stateData;

    protected bool isStunTimeOver;
    protected bool isGrounded;
    protected bool isMovementStopped;
    protected bool performCloseRangeActionLow;
    protected bool performCloseRangeActionMid;
    protected bool performCloseRangeActionHigh;
    protected bool isPlayerInLowAgroRange;
    protected bool isPlayerInMidAgroRange;
    protected bool isPlayerInHighAgroRange;

    public StunState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_StunState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = CollisionSenses.Ground;
        performCloseRangeActionLow = entity.CheckPlayerInCloseRangeActionLow();
        performCloseRangeActionMid = entity.CheckPlayerInCloseRangeActionMid();
        performCloseRangeActionHigh = entity.CheckPlayerInCloseRangeActionHigh();
        isPlayerInLowAgroRange = entity.CheckPlayerInLowAgroRange();
        isPlayerInMidAgroRange = entity.CheckPlayerInMidAgroRange();
        isPlayerInHighAgroRange = entity.CheckPlayerInHighAgroRange();

    }

    public override void Enter()
    {
        base.Enter();

        isStunTimeOver = false;
        isMovementStopped = false;
        Movement?.SetVelocity(stateData.stunKnockbackSpeed, stateData.stunKnockbackAngle, entity.lastDamageDirection);
    }

    public override void Exit()
    {
        base.Exit();

        //entity.ResetStunResistance();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(Time.time >= startTime + stateData.stunTime)
        {
            isStunTimeOver = true;
        }

        if (isGrounded && Time.time >= startTime + stateData.stunKnockbackTime && !isMovementStopped)
        {
            isMovementStopped = true;
            Movement?.SetVelocityX(0f);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
