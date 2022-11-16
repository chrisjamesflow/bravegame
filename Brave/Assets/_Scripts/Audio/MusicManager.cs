using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public AudioSource source;
    public AudioSource sourceTwo;
    public Transform sourcePosition;
    public static MusicManager instance;

    private float maxVolume = 0.2f;
    private float minVolume = 0f;

    public float fadeInDuration = 1f;
    public float fadeOutDuration = 1f;

    private IEnumerator fadeIn;
    private IEnumerator fadeOut;

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
        sourcePosition = GameObject.FindWithTag("MusicSource").GetComponent<Transform>();
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if(source.isPlaying && sourceTwo.isPlaying)
        {
            source.timeSamples = sourceTwo.timeSamples;
        }
        if(sourcePosition != null)
        {
            transform.position = sourcePosition.position;
        }
    }

    public void StartSourceMusic(AudioClip audioClip)
    {
        if (sourceTwo.clip.name == audioClip.name && sourceTwo.volume > maxVolume - 0.05) return;

        sourceTwo.clip = audioClip;
        sourceTwo.timeSamples = 0;
        sourceTwo.volume = 0.2f;
        sourceTwo.Play();
    }

    public void StopSourceMusic(AudioClip audioClip)
    {
        sourceTwo.clip = audioClip;
        sourceTwo.volume = 0;
    }

    public void StartMusic(AudioClip audioClip)
    {
        if (source.clip.name == audioClip.name && source.volume > maxVolume - 0.05) return;

        if (fadeOut != null)
            StopCoroutine(fadeOut);
        source.clip = audioClip;
        source.timeSamples = 0;
        source.Play();
        fadeIn = FadeIn(source, fadeInDuration, maxVolume);
        StartCoroutine(fadeIn);
    }

    public void StopMusic()
    {
        fadeOut = FadeOut(source, fadeOutDuration, minVolume);
        if (source.isPlaying)
        {
            StopCoroutine(fadeIn);
            StartCoroutine(fadeOut);
        }
    }

    public void SetStopMusic(AudioClip audioClip)
    {
        source.clip = audioClip;
        StopMusic();
    }

    public void StopMusicInstantly()
    {
        source.volume = 0;
        sourceTwo.volume = 0;
        source.Stop();
        sourceTwo.Stop();
    }

    IEnumerator FadeIn(AudioSource audioSource, float duration, float targetVolume)
    {
        float timer = 0f;
        float currentVolume = audioSource.volume;
        float targetValue = Mathf.Clamp(targetVolume, minVolume, maxVolume);

        while (timer < duration)
        {
            timer += Time.deltaTime;
            var newVolume = Mathf.Lerp(currentVolume, targetValue, timer / duration);
            audioSource.volume = newVolume;
            yield return null;
        }
    }

    IEnumerator FadeOut(AudioSource audioSource, float duration, float targetVolume)
    {
        float timer = 0f;
        float currentVolume = audioSource.volume;
        float targetValue = Mathf.Clamp(targetVolume, minVolume, maxVolume);

        while (audioSource.volume > 0)
        {
            timer += Time.deltaTime;
            var newVolume = Mathf.Lerp(currentVolume, targetValue, timer / duration);
            audioSource.volume = newVolume;
            yield return null;
        }
    }
}
