using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource source;
    public static MusicManager instance;

    private float maxVolume = 0.2f;
    private float minVolume = 0f;

    public float fadeInDuration = 1f;
    public float fadeOutDuration = 1f;

    private IEnumerator fadeIn;
    private IEnumerator fadeOut;

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

    public void StartMusic(AudioClip audioClip)
    {
        if (source.clip.name == audioClip.name && source.volume > maxVolume - 0.05) return;

        if (fadeOut != null)
            StopCoroutine(fadeOut);
        source.clip = audioClip;
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
