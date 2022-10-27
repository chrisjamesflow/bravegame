using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFour : MonoBehaviour
{
    #region State Variables
    public PlayerFourStateMachine StateMachine { get; private set; }

    public PlayerFourIdleState IdleState { get; private set; }
    public PlayerFourMoveState MoveState { get; private set; }
    public PlayerFourJumpState JumpState { get; private set; }
    public PlayerFourInAirState InAirState { get; private set; }
    public PlayerFourLandState LandState { get; private set; }
    public PlayerFourWallSlideState WallSlideState { get; private set; }
    public PlayerFourWallGrabState WallGrabState { get; private set; }
    public PlayerFourWallClimbState WallClimbState { get; private set; }
    public PlayerFourWallJumpState WallJumpState { get; private set; }
    public PlayerFourLedgeClimbState LedgeClimbState { get; private set; }
    public PlayerFourAttackState PrimaryAttackState { get; private set; }
    public PlayerFourAttackState SecondaryAttackState { get; private set; }
    public PlayerFourAttackState TertiaryAttackState { get; private set; }


    [SerializeField]
    private PlayerData playerData;
    #endregion

    #region Components
    public Core Core { get; private set; }
    public Animator Anim { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public Transform DashDirectionIndicator { get; private set; }
    public BoxCollider2D MovementCollider { get; private set; }
    public AudioManager Audio { get; private set; }
    public PlayerFourInventory Inventory { get; private set; }
    #endregion

    #region Other Variables
    private Vector2 workspace;
    #endregion

    #region Unity Callback Functions
    private void Awake()
    {
        Core = GetComponentInChildren<Core>();

        StateMachine = new PlayerFourStateMachine();

        IdleState = new PlayerFourIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerFourMoveState(this, StateMachine, playerData, "move");
        JumpState = new PlayerFourJumpState(this, StateMachine, playerData, "inAir");
        InAirState = new PlayerFourInAirState(this, StateMachine, playerData, "inAir");
        LandState = new PlayerFourLandState(this, StateMachine, playerData, "land");
        WallSlideState = new PlayerFourWallSlideState(this, StateMachine, playerData, "wallSlide");
        WallGrabState = new PlayerFourWallGrabState(this, StateMachine, playerData, "wallGrab");
        WallClimbState = new PlayerFourWallClimbState(this, StateMachine, playerData, "wallClimb");
        WallJumpState = new PlayerFourWallJumpState(this, StateMachine, playerData, "inAir");
        LedgeClimbState = new PlayerFourLedgeClimbState(this, StateMachine, playerData, "ledgeClimbState");
        PrimaryAttackState = new PlayerFourAttackState(this, StateMachine, playerData, "attack");
        SecondaryAttackState = new PlayerFourAttackState(this, StateMachine, playerData, "attack");
        TertiaryAttackState = new PlayerFourAttackState(this, StateMachine, playerData, "attack");
    }

    private void Start()
    {
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        RB = GetComponent<Rigidbody2D>();
        MovementCollider = GetComponent<BoxCollider2D>();
        Audio = GetComponent<AudioManager>();
        Inventory = GetComponent<PlayerFourInventory>();

        PrimaryAttackState.SetWeapon(Inventory.weapons[(int)CombatInputs.primary]);
        //SecondaryAttackState.SetWeapon(Inventory.weapons[(int)CombatInputs.secondary]);

        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        Core.LogicUpdate();
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion

    #region Other Functions

    public void SetColliderHeight(float height)
    {
        Vector2 center = MovementCollider.offset;
        workspace.Set(MovementCollider.size.x, height);

        center.y += (height - MovementCollider.size.y) / 2;

        MovementCollider.size = workspace;
        MovementCollider.offset = center;
    }

    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

    private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
    #endregion
}
