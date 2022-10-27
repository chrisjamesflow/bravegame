using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHitboxToWeaponOne : MonoBehaviour
{
    private AggressiveWeaponOne weapon;

    private void Awake()
    {
        weapon = GetComponentInParent<AggressiveWeaponOne>();
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
