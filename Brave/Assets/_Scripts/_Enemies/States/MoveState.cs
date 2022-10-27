using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    private Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private Movement movement;

    private CollisionSenses CollisionSenses { get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses); }
    private CollisionSenses collisionSenses;

    protected D_MoveState stateData;

    protected bool isDetectingWall;
    protected bool isDetectingLedge;
    protected bool isDetectingJumpWall;
    protected bool unableToJumpUp;
    protected bool ableToJumpDown;
    protected bool isPlayerInLowAgroRange;
    protected bool isPlayerInMidAgroRange;
    protected bool isPlayerInHighAgroRange;
    protected bool isPlayerBehindAgroRange;

    public MoveState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_MoveState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isDetectingLedge = CollisionSenses.LedgeVertical;
        isDetectingWall = CollisionSenses.WallFront;
        isDetectingJumpWall = entity.CheckWallForJump();
        unableToJumpUp = entity.CheckJumpUp();
        ableToJumpDown = entity.CheckJumpDown();
        isPlayerInLowAgroRange = entity.CheckPlayerInLowAgroRange();
        isPlayerInMidAgroRange = entity.CheckPlayerInMidAgroRange();
        isPlayerInHighAgroRange = entity.CheckPlayerInHighAgroRange();
        isPlayerBehindAgroRange = entity.CheckPlayerBehindAgroRangeLow();
    }

    public override void Enter()
    {
        base.Enter();

        Movement?.SetVelocityX(0f);
        Movement?.SetVelocityX(stateData.movementSpeed * Movement.FacingDirection);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        Movement?.SetVelocityX(stateData.movementSpeed * Movement.FacingDirection);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
