using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeMusicTrigger : MonoBehaviour
{
    public AudioClip newMusic;
    public AudioClip newMusicSource;
    public AudioSource music;

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
        music = GameObject.FindWithTag("Music").GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        MusicManager.instance.StartMusic(newMusic);
        MusicManager.instance.StartSourceMusic(newMusicSource);
    }
}
