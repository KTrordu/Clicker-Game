// Author: Alptuğ Yılmaz
// Date: 09.06.2025
// Description: Manages the core opponent logic in the GameScene, including vote tracking and UI updates.

using TMPro;
using UnityEngine;

/// <summary>
/// Controls the opponent logic.
/// </summary>
public class OpponentManager : MonoBehaviour
{
    #region VARIABLES

    /// <summary>
    /// Enables or disables the Debug mode.
    /// </summary>
    [Header("Debugging"), Tooltip("Enables or disables the Debug mode for logging.")]
    public bool debugMode = false;

    /// <summary>
    /// Represents the GameManager GameObject in the scene.
    /// </summary>
    private GameObject gameManager;

    /// <summary>
    /// Represents the TimeManager GameObject in the scene.
    /// </summary>
    private GameObject timeManager;

    #endregion

    /// <summary>
    /// Initialize the GameManager GameObject, TimeManager GameObject, and start updating opponent votes every second.
    /// </summary>
    void Start()
    {
        if (debugMode) Debug.Log("OpponentManager started. Initializing GameManager and TimeManager.");

        gameManager = GameObject.Find("GameManager");
        timeManager = GameObject.Find("TimeManager");

        InvokeRepeating("IncreaseOppenentVote", 1f, 1f);
    }

    /// <summary>
    /// Increases the opponent's vote count by a random amount based on the time passed in the game.
    /// </summary>
    private void IncreaseOppenentVote()
    {
        int timePassed = timeManager.GetComponent<TimeManager>().GetTimePassed();
        int voteToAdd = Random.Range(7, 15) * Mathf.FloorToInt(timePassed);
        if (debugMode) Debug.Log($"Increasing opponent votes by {voteToAdd} based on time passed: {timePassed} seconds.");
        gameManager.GetComponent<GameManager>().IncreaseOpponentVotes(voteToAdd);
    }
}
