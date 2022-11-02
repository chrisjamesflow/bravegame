using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public int sceneToLoad;
    public Vector2 playerPosition;
    public SO_PlayerVectorValue playerPositionMemory;
    public float transitionTime = 0.4f;
    public Animator transition;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            playerPositionMemory.initialPlayerPosition = playerPosition;
            StartCoroutine(LoadLevel());
        }
    }

    IEnumerator LoadLevel()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneToLoad);
    }
}
