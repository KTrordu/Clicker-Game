// Author: Alptuð Yýlmaz
// Date: 08.06.2025
// Description: Manages the core time logic in the GameScene, including time tracking and game state updates.

using TMPro;
using UnityEngine;

/// <summary>
/// Controls the time logic.
/// </summary>
public class TimeManager : MonoBehaviour
{
    #region VARIABLES

    /// <summary>
    /// Enables or disables the Debug mode.
    /// </summary>
    [Header("Debugging"), Tooltip("Enables or disables the Debug mode for logging.")]
    public bool debugMode = false;

    /// <summary>
    /// Represents the total times passed text in the GameScene.
    /// </summary>
    [Header("UI Elements"), Tooltip("The text component that displays the total time passed in the GameScene.")]
    public TMP_Text timeText;

    /// <summary>
    /// Represents the total time passed in seconds.
    /// </summary>
    private float totalTimePassed = 0f;

    #endregion

    /// <summary>
    /// Initializes the time text at game start.
    /// </summary>
    void Start()
    {
        if (debugMode) Debug.Log("TimeManager started. Initializing time text.");

        timeText.text = $"Time Passed: {totalTimePassed}";
    }

    /// <summary>
    /// Called once per frame to update the time passed and display it.
    /// </summary>
    void Update()
    {
        if (debugMode) Debug.Log("1 frame passed.");

        totalTimePassed += Time.deltaTime;
        timeText.text = $"Time Passed: {Mathf.FloorToInt(totalTimePassed)}";
    }
}
