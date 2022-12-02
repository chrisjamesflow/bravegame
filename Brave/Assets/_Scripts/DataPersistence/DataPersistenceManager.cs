using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("Debugging")]
    [SerializeField] private bool disableDataPersistence = false;
    [SerializeField] private bool initializeDataIfNull = false;

    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    [SerializeField] private bool useEncryption;

    private GameData gameData;

    private FileDataHandler dataHandler;

    private string selectedProfileId = "";

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

        if (disableDataPersistence)
        {
            Debug.LogWarning("Data Persistence is currently disabled!");
        }

        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
    }

    public void ChangeSelectedProfileId(string newProfileId)
    {
        selectedProfileId = newProfileId;
    }

    public void DeleteProfileData(string profileId)
    {
        dataHandler.Delete(profileId);
    }

    public void NewGame()
    {
        gameData = new GameData();
        dataHandler.Save(gameData, selectedProfileId);
    }

    public void LoadGame()
    {
        if (disableDataPersistence)
        {
            return;
        }

        gameData = dataHandler.Load(selectedProfileId);

        if (gameData == null && initializeDataIfNull)
        {
            NewGame();
        }

        if(gameData == null)
        {
            return;
        }

        player.transform.position = gameData.playerPosition;
        SceneManager.LoadScene(gameData.currentScene);

        PlayerInAirState.climbAbility = gameData.inAirClimb;
        PlayerWallSlideState.climbAbility = gameData.wallSlideClimb;
        PlayerLedgeClimbState.crouchAbility = gameData.ledgeCrouch;
        PlayerGroundedState.crouchAbility = gameData.groundCrouch;
        PlayerInAirState.dashAbility = gameData.dash;
        PlayerJumpState.doubleJumpAbility = gameData.doubleJump;
        PlayerInAirState.glideAbility = gameData.glide;
        PlayerTouchingWallState.wallJumpAbility = gameData.wallJump;
        PlayerLedgeClimbState.wallJumpAbility = gameData.ledgeWallJump;
    }

    public void SaveGame()
    {
        if (disableDataPersistence)
        {
            return;
        }

        if (gameData == null)
        {
            return;
        }

        gameData.playerPosition = player.transform.position;
        gameData.currentScene = SceneManager.GetActiveScene().buildIndex;

        gameData.inAirClimb = PlayerInAirState.climbAbility;
        gameData.wallSlideClimb = PlayerWallSlideState.climbAbility;
        gameData.ledgeCrouch = PlayerLedgeClimbState.crouchAbility;
        gameData.groundCrouch = PlayerGroundedState.crouchAbility;
        gameData.dash = PlayerInAirState.dashAbility;
        gameData.doubleJump = PlayerJumpState.doubleJumpAbility;
        gameData.glide = PlayerInAirState.glideAbility;
        gameData.wallJump = PlayerTouchingWallState.wallJumpAbility;
        gameData.ledgeWallJump = PlayerLedgeClimbState.wallJumpAbility;

        dataHandler.Save(gameData, selectedProfileId);
    }

    public bool HasGameData()
    {
        return gameData != null;
    }

    public Dictionary<string, GameData> GetAllProfilesGameData()
    {
        return dataHandler.LoadAllProfiles();
    }
}
