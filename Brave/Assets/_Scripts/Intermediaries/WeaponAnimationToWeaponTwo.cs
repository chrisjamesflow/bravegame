using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimationToWeaponTwo : MonoBehaviour
{
    private WeaponTwo weapon;

    private void Start()
    {
        weapon = GetComponentInParent<WeaponTwo>();
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
