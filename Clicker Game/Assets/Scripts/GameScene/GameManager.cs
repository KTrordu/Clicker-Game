// Author: Alptuğ Yılmaz
// Date: 08.06.2025
// Description: Manages the core game logic in the GameScene, including vote tracking, UI updates, and win/lose conditions.

using TMPro;
using UnityEngine;

/// <summary>
/// Represents the current state of the game.
/// </summary>
public enum GameState { CONTINUE, WON, LOST };

/// <summary>
/// Controls the voting logic, UI display, and win/loss conditions in the GameScene.
/// </summary>
public class GameManager : MonoBehaviour
{
    #region VARIABLES

    [Header("Vote Numbers")]
    [Tooltip("Represents the total number of voters in the country.")]
    public int totalVotes;

    /// <summary>
    /// Represents the number of votes acquired by the player and the opponent.
    /// </summary>
    private int playerVotes = 0;
    private int opponentVotes = 0;

    [Header("UI Elements")]
    public TMP_Text totalVotesText;
    public TMP_Text playerVotesText;
    public TMP_Text opponentVotesText;

    /// <summary>
    /// Current state of the game (ongoing, won, or lost).
    /// </summary>
    private GameState gameState = GameState.CONTINUE;

    #endregion

    /// <summary>
    /// Initializes the UI at game start.
    /// </summary>
    void Start()
    {
        UpdateUI();
    }

    /// <summary>
    /// Called once per frame to refresh UI and check for win/loss conditions.
    /// </summary>
    void Update()
    {
        UpdateUI();
        ControlVotes();
    }

    /// <summary>
    /// Increases the player's vote count by the given amount.
    /// </summary>
    /// <param name="amount">Number of votes to add.</param>
    public void IncreasePlayerVotes(int amount) => playerVotes += amount;

    /// <summary>
    /// Increases the opponent's vote count by the given amount.
    /// </summary>
    /// <param name="amount">Number of votes to add.</param>
    public void IncreaseOpponentVotes(int amount) => opponentVotes += amount;

    /// <summary>
    /// Updates the UI elements to reflect current vote counts.
    /// </summary>
    private void UpdateUI()
    {
        totalVotesText.text = $"Total Votes: {totalVotes}";
        playerVotesText.text = $"Your Votes: {playerVotes}";
        opponentVotesText.text = $"Their Votes: {opponentVotes}";
    }

    /// <summary>
    /// Checks if the player or opponent has reached the winning condition.
    /// </summary>
    private void ControlVotes()
    {
        if (playerVotes >= totalVotes / 2)
        {
            gameState = GameState.WON;
            EndGame();
        }
        else if (opponentVotes >= totalVotes / 2)
        {
            gameState = GameState.LOST;
            EndGame();
        }
    }

    /// <summary>
    /// Executes logic when the game ends (placeholder for win/lose actions).
    /// </summary>
    private void EndGame()
    {
        if (gameState == GameState.WON)
        {
            // TODO: Add win logic (e.g. show win screen, play animation)
            Debug.Log("You won the game!");
        }
        else if (gameState == GameState.LOST)
        {
            // TODO: Add lose logic (e.g. show lose screen, disable controls)
            Debug.Log("You lost the game!");
        }
    }
}
