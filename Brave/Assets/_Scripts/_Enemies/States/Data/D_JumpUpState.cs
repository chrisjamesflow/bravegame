using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newJumpUpStateData", menuName = "Data/State Data/Jump Up State")]

public class D_JumpUpState : ScriptableObject
{
    public float jumpUpHeight = 4.0f;
    public float jumpUpSpeed = 8.0f;
}
