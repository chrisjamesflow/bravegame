using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    private CinemachineVirtualCamera CVC;

    [SerializeField]
    private Transform respawnPoint;

    [SerializeField]
    private float respawnTime;

    public static GameObject player;

    [SerializeField]
    public ParticleSystem upgradeEffect;

    [SerializeField]
    public Transform orbPosition;

    private AudioManager Audio;

    private float respawnTimeStart;

    private bool respawn;

    [SerializeField]
    private float sleepTime = 2f;

    private float sleepStartTime;

    private bool asleep;
    
    [SerializeField]
    private GameObject[] enemyAwake;

    [SerializeField]
    private GameObject[] enemyAsleep;

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
        player = GameObject.FindWithTag("Player");
    }

    private void Start()
    {
        Audio = GetComponent<AudioManager>();
        CVC = GameObject.Find("Player Camera").GetComponent<CinemachineVirtualCamera>();
        CVC.m_Follow = player.transform;
    }

    private void Update()
    {
        CheckRespawn();
        CheckSleepTime();
    }

    public void OrbEffects()
    {
        Instantiate(upgradeEffect, orbPosition);
        Audio.Play("OrbGet");
    }

    public void Respawn()
    {
        respawnTimeStart = Time.time;
        respawn = true;
    }

    public void WakeUp()
    {
        sleepStartTime = Time.time;
        asleep = true;
    }

    private void CheckRespawn()
    {
        if(Time.time >= respawnTimeStart + respawnTime && respawn)
        {
            player.transform.position = respawnPoint.transform.position;
            player.transform.gameObject.SetActive(true);
            respawn = false;
        }
    }

    private void CheckSleepTime()
    {
        if(Time.time >= sleepStartTime + sleepTime && asleep)
        {
            enemyAwake[0].transform.gameObject.SetActive(true);
            enemyAsleep[0].transform.gameObject.SetActive(false);
            asleep = false;
        }
    }
}




