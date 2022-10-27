using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeState : State
{
    private Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private Movement movement;

    private CollisionSenses CollisionSenses { get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses); }
    private CollisionSenses collisionSenses;

    protected D_ChargeState stateData;

    protected bool isPlayerInLowAgroRange;
    protected bool isPlayerInMidAgroRange;
    protected bool isPlayerInHighAgroRange;
    protected bool isPlayerBehindAgroRangeBelow;
    protected bool isPlayerBehindAgroRangeLow;
    protected bool isPlayerBehindAgroRangeMid;
    protected bool isPlayerBehindAgroRangeHigh;
    protected bool isDetectingLedge;
    protected bool isDetectingWall;
    protected bool isDetectingJumpWall;
    protected bool unableToJumpUp;
    protected bool ableToJumpDown;
    protected bool isChargeTimeOver;
    protected bool performCloseRangeActionLow;
    protected bool performCloseRangeActionMid;
    protected bool performCloseRangeActionHigh;
    protected bool performCloseRangeActionBehindLow;
    protected bool performCloseRangeActionBehindMid;

    public ChargeState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_ChargeState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInLowAgroRange = entity.CheckPlayerInLowAgroRange();
        isPlayerInMidAgroRange = entity.CheckPlayerInMidAgroRange();
        isPlayerInHighAgroRange = entity.CheckPlayerInHighAgroRange();
        isPlayerBehindAgroRangeBelow = entity.CheckPlayerBehindAgroRangeBelow();
        isPlayerBehindAgroRangeLow = entity.CheckPlayerBehindAgroRangeLow();
        isPlayerBehindAgroRangeMid = entity.CheckPlayerBehindAgroRangeMid();
        isPlayerBehindAgroRangeHigh = entity.CheckPlayerBehindAgroRangeHigh();
        isDetectingLedge = CollisionSenses.LedgeVertical;
        isDetectingWall = CollisionSenses.WallFront;
        isDetectingJumpWall = entity.CheckWallForJump();
        unableToJumpUp = entity.CheckJumpUp();
        ableToJumpDown = entity.CheckJumpDown();
        performCloseRangeActionLow = entity.CheckPlayerInCloseRangeActionLow();
        performCloseRangeActionMid = entity.CheckPlayerInCloseRangeActionMid();
        performCloseRangeActionHigh = entity.CheckPlayerInCloseRangeActionHigh();
        performCloseRangeActionBehindLow = entity.CheckPlayerBehindCloseRangeActionLow();
        performCloseRangeActionBehindMid = entity.CheckPlayerBehindCloseRangeActionMid();
    }

    public override void Enter()
    {
        base.Enter();

        isChargeTimeOver = false;
        Movement?.SetVelocityX(stateData.chargeSpeed * Movement.FacingDirection);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Movement?.SetVelocityX(stateData.chargeSpeed * Movement.FacingDirection);

        if (Time.time >= startTime + stateData.chargeTime)
        {
            isChargeTimeOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
