using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]

public class GameData
{
    public Vector3 playerPosition;
    public int currentScene;
    public bool inAirClimb, wallSlideClimb, ledgeCrouch, groundCrouch, dash, glide, wallJump, ledgeWallJump, doubleJump;
    public string globalVariablesStateJson;

    public GameData()
    {
        playerPosition = new Vector2(-6, 1.25f);
        currentScene = 1;
        //
        inAirClimb = false;
        wallSlideClimb = false;
        ledgeCrouch = false;
        groundCrouch = false;
        dash = false;
        glide = false;
        wallJump = false;
        ledgeWallJump = false;
        doubleJump = false;
        //
    }
}
