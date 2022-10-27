using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/Entity Data/Base Data")]

public class D_Entity : ScriptableObject
{
    public float maxHealth = 30f;

    public float DamageHopSpeed = 3f;

    public float wallCheckDistance = 0.1f;
    public float ledgeCheckDistance = 0.5f;
    public float groundCheckRadius = 0.3f;
    public float ledgeForAttackCheckDistance = 0.5f;

    public float jumpWallCheckDistance = 4.0f;
    public float jumpUpCheckDistance = 4.0f;
    public float jumpDownCheckDistance = 8.0f;

    public float lowAgroDistance = 9f;
    public float lowMaxAgroDistance = 13f;
    public float midAgroDistance = 9f;
    public float midMaxAgroDistance = 13f;
    public float highAgroDistance = 9f;
    public float highMaxAgroDistance = 13f;
    public float behindAgroDistance = 3f;
    public float behindMaxAgroDistance = 5f;

    public float stunResistance = 3f;
    public float stunRecoveryTime = 2f;

    public float closeRangeActionDistance = 1f;

    public GameObject hitParticle;

    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;
}
