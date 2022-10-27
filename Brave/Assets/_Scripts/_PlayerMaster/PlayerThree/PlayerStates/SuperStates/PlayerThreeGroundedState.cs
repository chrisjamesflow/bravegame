using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThreeGroundedState : PlayerThreeState
{
    protected int xInput;
    protected int yInput;

    protected bool isTouchingCeiling;

    protected Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private Movement movement;

    private CollisionSenses CollisionSenses { get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses); }
    private CollisionSenses collisionSenses;

    private bool jumpInput;
    private bool grabInput;
    public bool crouchInput;
    public bool isGrounded;
    private bool isTouchingWall;
    private bool isTouchingLedge;

    public PlayerThreeGroundedState(PlayerThree player, PlayerThreeStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        if (CollisionSenses)
        {
            isGrounded = CollisionSenses.Ground;
            isTouchingWall = CollisionSenses.WallFront;
            isTouchingLedge = CollisionSenses.LedgeHorizontal;
            isTouchingCeiling = CollisionSenses.Ceiling;
        }
    }

    public override void Enter()
    {
        base.Enter();

        player.JumpState.ResetAmountOfJumpsLeft();
        player.InputHandler.UseDashInput();

        Movement?.SetVelocityX(0f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.RB.gravityScale = playerData.gravity;

        xInput = player.InputHandler.NormInputX;
        yInput = player.InputHandler.NormInputY;
        jumpInput = player.InputHandler.JumpInput;
        grabInput = player.InputHandler.GrabInput;
        crouchInput = player.InputHandler.CrouchInput;

        Movement?.SetVelocityX(0f);

        if (player.InputHandler.AttackInputs[(int)CombatInputs.primary] && !isTouchingCeiling && !crouchInput)
        {
            xInput = 0;
            stateMachine.ChangeState(player.PrimaryAttackState);
            player.Audio.Play("PanFlute");
        }
        else if(player.InputHandler.AttackInputs[(int)CombatInputs.secondary] && !isTouchingCeiling)
        {
            stateMachine.ChangeState(player.SecondaryAttackState);
        }
        else if (player.InputHandler.AttackInputs[(int)CombatInputs.tertiary] && !isTouchingCeiling)
        {
            stateMachine.ChangeState(player.TertiaryAttackState);
        }
        else if (jumpInput && player.JumpState.CanJump() && !isTouchingCeiling)
        {
            stateMachine.ChangeState(player.JumpState);
            player.Audio.Play("Jump");
        }
        else if (!isGrounded)
        {
            player.InAirState.StartCoyoteTime();
            stateMachine.ChangeState(player.InAirState);
        }
        /* GRAB WALL FROM GROUND
        else if (isTouchingWall && grabInput && isTouchingLedge)
        {
            stateMachine.ChangeState(player.WallGrabState);
            player.Audio.Stop("Move");
            player.Audio.Stop("Crawl");
        }*/
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
