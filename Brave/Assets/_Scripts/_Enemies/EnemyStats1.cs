using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats1 : MonoBehaviour, IDamageable
{
    [SerializeField]
    private float maxHealth;

    [SerializeField]
    private GameObject
        musicNotesParticle,
        sleepZsParticle;

    [SerializeField]
    public GameObject
        enemy,
        enemyAwake,
        enemyAsleep;

    private float currentHealth;

    private GameManager GM;

    private void Start()
    {
        enemyAwake.transform.position = enemy.transform.position;
        enemyAsleep.transform.position = enemy.transform.position;
        enemyAsleep.transform.gameObject.SetActive(false);
        currentHealth = maxHealth;
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void Damage(float amount)
    {
        //Debug.Log(transform.name + " Damaged!");
        DecreaseHealth(amount);
    }

    public void DecreaseHealth(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0.0f)
        {
            Die();
        }
    }

    public void Die()
    {
        Instantiate(musicNotesParticle, enemyAwake.transform.position, musicNotesParticle.transform.rotation);
        Instantiate(sleepZsParticle, enemyAwake.transform.position, sleepZsParticle.transform.rotation);
        enemy.transform.position = enemyAwake.transform.position;
        enemyAwake.transform.position = enemyAsleep.transform.position;
        GM.Sleep1();
        enemyAwake.transform.gameObject.SetActive(false);
        enemyAsleep.transform.gameObject.SetActive(true);
    }
}
