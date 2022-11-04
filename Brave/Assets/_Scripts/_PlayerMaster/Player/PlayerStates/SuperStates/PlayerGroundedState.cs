using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int xInput;
    protected int yInput;

    protected bool isTouchingCeiling;

    protected Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private Movement movement;

    private CollisionSenses CollisionSenses { get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses); }
    private CollisionSenses collisionSenses;

    private bool jumpInput;
    public bool crouchInput;
    public bool isGrounded;

    public static bool crouchAbility;

    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        if (CollisionSenses)
        {
            isGrounded = CollisionSenses.Ground;
            isTouchingCeiling = CollisionSenses.Ceiling;
        }
    }

    public void Start()
    {
        crouchAbility = false;
    }

    public override void Enter()
    {
        base.Enter();

        player.JumpState.ResetAmountOfJumpsLeft();
        player.DashState.ResetCanDash();
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
        }
        /* DASH FROM GROUND
        else if (dashInput && player.DashState.CheckIfCanDash())
        {
            stateMachine.ChangeState(player.DashState);
        }
        */
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
