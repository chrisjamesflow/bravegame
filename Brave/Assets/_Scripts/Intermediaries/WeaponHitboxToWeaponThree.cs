using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHitboxToWeaponThree : MonoBehaviour
{
    private AggressiveWeaponThree weapon;

    private void Awake()
    {
        weapon = GetComponentInParent<AggressiveWeaponThree>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        weapon.AddToDetected(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        weapon.RemoveFromDetected(collision);
    }
}
