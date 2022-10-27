using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]

public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    [Range(0.1f, 3)]
    public float pitch;
    [SerializeField]
    public bool loop;
    [Range(0f, 1f)]
    public float spatialBlend;
    [SerializeField]
    public float minDistance;
    [SerializeField]
    public float maxDistance;
    [SerializeField]
    public AudioRolloffMode rolloffMode;

    [HideInInspector]
    public AudioSource source;
}
