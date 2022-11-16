using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    private CinemachineVirtualCamera CVC;
    private AudioManager Audio;

    public static GameObject player;
    public PauseMenu pauseMenu;

    [SerializeField] public ParticleSystem upgradeEffect;
    [SerializeField] public Transform orbPosition;

    [SerializeField] private float respawnTime;
    private float respawnTimeStart;
    private bool respawn;

    [SerializeField] private float sleepTime = 2f;
    private float sleepTimeStart1;
    private float sleepTimeStart2;
    private bool asleep1;
    private bool asleep2;

    [SerializeField] private GameObject[]
        enemyAwake,
        enemyAsleep;

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
        CheckSleepTime1();
        CheckSleepTime2();
    }

    public void OrbEffects()
    {
        StartCoroutine(LowerMusicVolume());
        Instantiate(upgradeEffect, orbPosition);
        Audio.Play("OrbGet");
    }

    public void Respawn()
    {
        respawnTimeStart = Time.time;
        respawn = true;
    }

    private void CheckRespawn()
    {
        if(Time.time >= respawnTimeStart + respawnTime && respawn)
        {
            player.transform.gameObject.SetActive(true);
            PauseMenu.CanPause = true;
            pauseMenu.LoadPlayer();
            respawn = false;
        }
    }

    IEnumerator LowerMusicVolume()
    {
        yield return new WaitForSeconds(0.5f);
        MusicManager.instance.source.volume = 0.05f;
        MusicManager.instance.sourceTwo.volume = 0.05f;
        yield return new WaitForSeconds(3.5f);
        MusicManager.instance.source.volume = 0.2f;
        MusicManager.instance.sourceTwo.volume = 0.2f;
    }

    // Enemy 1
    public void Sleep1()
    {
        sleepTimeStart1 = Time.time;
        asleep1 = true;
    }

    private void CheckSleepTime1()
    {
        if(Time.time >= sleepTimeStart1 + sleepTime && asleep1)
        {
            enemyAsleep[0].transform.gameObject.SetActive(false);
            enemyAwake[0].transform.gameObject.SetActive(true);
            asleep1 = false;
        }
    }

    // Enemy 2
    public void Sleep2()
    {
        sleepTimeStart2 = Time.time;
        asleep2 = true;
    }

    private void CheckSleepTime2()
    {
        if (Time.time >= sleepTimeStart2 + sleepTime && asleep2)
        {
            enemyAsleep[1].transform.gameObject.SetActive(false);
            enemyAwake[1].transform.gameObject.SetActive(true);
            asleep1 = false;
        }
    }
}




