using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerSaveData
{
    public float[] position;
    public int currentScene;

    public PlayerSaveData (GameObject player)
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }
}
