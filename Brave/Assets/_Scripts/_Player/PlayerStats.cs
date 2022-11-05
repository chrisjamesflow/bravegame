using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour, IDamageable
{
    protected Core core;

    [SerializeField]
    private float maxHealth;

    private GameObject player;

    private float currentHealth;

    public GameManager GM;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GM = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    private void Start()
    {
        player = gameObject;
        currentHealth = maxHealth;
    }

    public void Damage(float amount)
    {
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
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02F;
        GM.Respawn();
        player.transform.gameObject.SetActive(false);
    }
}
