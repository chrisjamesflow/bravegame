using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimationToWeaponOne : MonoBehaviour
{
    private WeaponOne weapon;

    private void Start()
    {
        weapon = GetComponentInParent<WeaponOne>();
    }

    private void AnimationFinishTrigger()
    {
        weapon.AnimationFinishTrigger();
    }

    private void AnimationStartMovementTrigger()
    {
        weapon.AnimationStartMovementTrigger();
    }

    private void AnimationStopMovementTrigger()
    {
        weapon.AnimationStopMovementTrigger();
    }

    private void AnimationActionTrigger()
    {
        weapon.AnimationActionTrigger();
    }
}
