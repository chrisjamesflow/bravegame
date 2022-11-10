using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newMeleeAttackStateData", menuName = "Data/State Data/Melee Attack State")]

public class D_MeleeAttackState : ScriptableObject
{
    public float attackRadius = 5f;
    public float attackDamage = 1f;
    public float attackSpeed = 8f;
    public float attackTime = 0.3f;

    public Vector2 knockbackAngle = Vector2.one;
    public float knockbackStrength = 0f;

    public LayerMask whatIsPlayer;
}
