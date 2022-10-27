using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IDamageable
{
    protected Core core;

    [SerializeField]
    private float maxHealth;

    private GameObject courageOrb;

    /*[SerializeField]
    private GameObject
        deathChunkParticle,
        deathBloodParticle;*/

    private float currentHealth;

    private GameManager GM;

    private void Start()
    {
        currentHealth = maxHealth;
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        courageOrb = GameObject.FindGameObjectWithTag("CourageOrb1");
    }

    public void Damage(float amount)
    {
        //Debug.Log(core.transform.parent.name + " Damaged!");
        DecreaseHealth(amount);
    }

    public void DecreaseHealth(float amount)
    {
        currentHealth -= amount;

        if(currentHealth <= 0.0f)
        {
            Die();
        }
    }

    public void Die()
    {
        //Instantiate(deathChunkParticle, transform.position, deathChunkParticle.transform.rotation);
        //Instantiate(deathBloodParticle, transform.position, deathBloodParticle.transform.rotation);
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02F;
        GM.Respawn();
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="CourageOrb1")
        {
            GM.OrbSpawn();
            Destroy(courageOrb);
            Destroy(gameObject);
        }
    }
}
