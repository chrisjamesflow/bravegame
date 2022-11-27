using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour, IDamageable
{
    public static PlayerManager instance;
    public GameManager GM;
    private GameObject player;
    private float currentHealth;
    [SerializeField] private float maxHealth;
    public int currentScene;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        DontDestroyOnLoad(gameObject);
    }

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
        currentHealth = maxHealth;
    }

    public void Damage(float amount)
    {
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

    private void OnTriggerEnter2D(Collider2D upgrade)
    {
        if(upgrade.tag == "CourageOrb1")
        {
            GM.OrbEffects();
            transform.gameObject.SetActive(false);
        }
    }

    public void Die()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02F;
        GM.Respawn();
        PauseMenu.CanPause = false;
        player.transform.gameObject.SetActive(false);
    }
}
