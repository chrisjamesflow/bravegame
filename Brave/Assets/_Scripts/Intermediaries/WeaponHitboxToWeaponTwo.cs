using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHitboxToWeaponTwo : MonoBehaviour
{
    private AggressiveWeaponTwo weapon;

    private void Awake()
    {
        weapon = GetComponentInParent<AggressiveWeaponTwo>();
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
