using UnityEngine;
using UnityEngine.SceneManagement;

public class SourceMusicControl : MonoBehaviour
{
    public float sourceVolume;

    private void Start()
    {
        MusicManager.instance.sourceTwo.volume = sourceVolume;
    }
}
