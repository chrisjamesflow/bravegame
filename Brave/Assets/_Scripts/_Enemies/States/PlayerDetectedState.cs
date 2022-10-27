using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectedState : State
{
    protected Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private Movement movement;

    private CollisionSenses CollisionSenses { get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses); }
    private CollisionSenses collisionSenses;

    protected D_PlayerDetected stateData;

    protected bool isPlayerInLowAgroRange;
    protected bool isPlayerInMidAgroRange;
    protected bool isPlayerInHighAgroRange;
    protected bool isPlayerBehindAgroRange;
    protected bool isPlayerInLowMaxAgroRange;
    protected bool isPlayerInMidMaxAgroRange;
    protected bool isPlayerInHighMaxAgroRange;
    protected bool isPlayerBehindMaxAgroRange;
    protected bool performLongRangeAction;
    protected bool performCloseRangeActionLow;
    protected bool performCloseRangeActionMid;
    protected bool performCloseRangeActionHigh;
    protected bool isDetectingWall;
    protected bool isDetectingLedge;

    public PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetected stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInLowAgroRange = entity.CheckPlayerInLowAgroRange();
        isPlayerInMidAgroRange = entity.CheckPlayerInMidAgroRange();
        isPlayerInHighAgroRange = entity.CheckPlayerInHighAgroRange();
        isPlayerBehindAgroRange = entity.CheckPlayerBehindAgroRangeLow();
        isPlayerInLowMaxAgroRange = entity.CheckPlayerInLowMaxAgroRange();
        isPlayerInMidMaxAgroRange = entity.CheckPlayerInMidMaxAgroRange();
        isPlayerInHighMaxAgroRange = entity.CheckPlayerInHighMaxAgroRange();
        isPlayerBehindMaxAgroRange = entity.CheckPlayerBehindMaxAgroRange();
        isDetectingWall = CollisionSenses.WallFront;
        isDetectingLedge = CollisionSenses.LedgeVertical;
        performCloseRangeActionLow = entity.CheckPlayerInCloseRangeActionLow();
        performCloseRangeActionMid = entity.CheckPlayerInCloseRangeActionMid();
        performCloseRangeActionHigh = entity.CheckPlayerInCloseRangeActionHigh();
    }

    public override void Enter()
    {
        base.Enter();

        performLongRangeAction = false;
        Movement?.SetVelocityX(0f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Movement?.SetVelocityX(0f);

        if (Time.time >= startTime + stateData.longRangeActionTime)
        {
            performLongRangeAction = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
