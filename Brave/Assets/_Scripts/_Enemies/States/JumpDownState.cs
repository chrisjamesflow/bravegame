using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpDownState : State
{
    private Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private Movement movement;

    private CollisionSenses CollisionSenses { get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses); }
    private CollisionSenses collisionSenses;

    protected D_JumpDownState stateData;

    protected bool isDetectingLedge;
    protected bool isDetectingWall;
    protected bool performCloseRangeActionLow;
    protected bool performCloseRangeActionMid;
    protected bool performCloseRangeActionHigh;
    protected bool isPlayerInLowAgroRange;
    protected bool isPlayerInMidAgroRange;
    protected bool isPlayerInHighAgroRange;
    protected bool isPlayerBehindAgroRangeBelow;
    protected bool isPlayerBehindAgroRangeLow;
    protected bool isPlayerBehindAgroRangeMid;
    protected bool isPlayerBehindAgroRangeHigh;

    public JumpDownState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_JumpDownState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isDetectingLedge = CollisionSenses.LedgeVertical;
        isDetectingWall = CollisionSenses.WallFront;
        performCloseRangeActionLow = entity.CheckPlayerInCloseRangeActionLow();
        performCloseRangeActionMid = entity.CheckPlayerInCloseRangeActionMid();
        performCloseRangeActionHigh = entity.CheckPlayerInCloseRangeActionHigh();
        isPlayerInLowAgroRange = entity.CheckPlayerInLowAgroRange();
        isPlayerInMidAgroRange = entity.CheckPlayerInMidAgroRange();
        isPlayerInHighAgroRange = entity.CheckPlayerInHighAgroRange();
        isPlayerBehindAgroRangeBelow = entity.CheckPlayerBehindAgroRangeBelow();
        isPlayerBehindAgroRangeLow = entity.CheckPlayerBehindAgroRangeLow();
        isPlayerBehindAgroRangeMid = entity.CheckPlayerBehindAgroRangeMid();
        isPlayerBehindAgroRangeHigh = entity.CheckPlayerBehindAgroRangeHigh();
    }

    public override void Enter()
    {
        base.Enter();

        Movement?.SetVelocityX(stateData.jumpDownSpeed * Movement.FacingDirection);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
