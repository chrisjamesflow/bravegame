using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatTestDummy : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject hitParticles;

    private Animator anim;

    public void Damage(float amount)
    {
        Debug.Log(amount + " Damage Taken");

        Instantiate(hitParticles, transform.position, hitParticles.transform.rotation);
        anim.SetTrigger("damage");
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
}
