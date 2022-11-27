using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("Debugging")]
    [SerializeField] private bool initializeDataIfNull = false;

    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    [SerializeField] private bool useEncryption;

    private GameData gameData;

    private FileDataHandler dataHandler;

    public static DataPersistenceManager instance { get; private set; }

    public GameObject player;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        DontDestroyOnLoad(gameObject);

        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);

        if (gameData == null && initializeDataIfNull)
        {
            NewGame();
        }
    }

    public void NewGame()
    {
        gameData = new GameData();
        Debug.Log("New Game Data");
    }

    public void LoadGame()
    {
        if(gameData == null && initializeDataIfNull)
        {
            NewGame();
        }

        if(gameData == null)
        {
            return;
        }

        gameData = dataHandler.Load();
        player.transform.position = gameData.playerPosition;
        SceneManager.LoadScene(gameData.currentScene);
        //
        PlayerInAirState.climbAbility = gameData.inAirClimb;
        PlayerWallSlideState.climbAbility = gameData.wallSlideClimb;
        PlayerLedgeClimbState.crouchAbility = gameData.ledgeCrouch;
        PlayerGroundedState.crouchAbility = gameData.groundCrouch;
        PlayerInAirState.dashAbility = gameData.dash;
        PlayerJumpState.doubleJumpAbility = gameData.doubleJump;
        PlayerInAirState.glideAbility = gameData.glide;
        PlayerTouchingWallState.wallJumpAbility = gameData.wallJump;
        PlayerLedgeClimbState.wallJumpAbility = gameData.ledgeWallJump;
        //
        Debug.Log("Loaded Game Data");
    }

    public void SaveGame()
    {
        if(gameData == null)
        {
            return;
        }

        gameData.playerPosition = player.transform.position;
        gameData.currentScene = SceneManager.GetActiveScene().buildIndex;
        //
        gameData.inAirClimb = PlayerInAirState.climbAbility;
        gameData.wallSlideClimb = PlayerWallSlideState.climbAbility;
        gameData.ledgeCrouch = PlayerLedgeClimbState.crouchAbility;
        gameData.groundCrouch = PlayerGroundedState.crouchAbility;
        gameData.dash = PlayerInAirState.dashAbility;
        gameData.doubleJump = PlayerJumpState.doubleJumpAbility;
        gameData.glide = PlayerInAirState.glideAbility;
        gameData.wallJump = PlayerTouchingWallState.wallJumpAbility;
        gameData.ledgeWallJump = PlayerLedgeClimbState.wallJumpAbility;
        //
        dataHandler.Save(gameData);
        Debug.Log("Saved Game Data");
    }
}
