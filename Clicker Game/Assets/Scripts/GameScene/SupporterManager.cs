// Author: Alptuğ Yılmaz
// Date: 09.06.2025
// Description: Manages the supporters' logic, including acquiring votes.

using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the supporters' logic.
/// </summary>
public class SupporterManager : MonoBehaviour
{
    #region VARIABLES

    /// <summary>
    /// Enables or disables the Debug mode.
    /// </summary>
    [Header("Debugging"), Tooltip("Enables or disables the Debug mode for logging.")]
    public bool debugMode = false;

    /// <summary>
    /// Represents the Supporters.
    /// </summary>
    [Header("Supporter References")]
    public Supporter[] allSupporters;

    /// <summary>
    /// Tracks Supporter counts by their data.
    /// </summary>
    private Dictionary<SupporterData, int> supporterCounts = new Dictionary<SupporterData, int>();

    /// <summary>
    /// Represents the GameManager GameObject in the scene.
    /// </summary>
    private GameObject gameManagerObject;
    private GameManager gameManager;

    #endregion

    #region UNITY METHODS

    /// <summary>
    /// Initialize the GameManager and supporters GameObjects.
    /// </summary>
    void Start()
    {
        gameManagerObject = GameObject.Find("GameManager");
        gameManager = gameManagerObject.GetComponent<GameManager>();

        InitializeSupporters();

        InvokeRepeating("AddSupporterVotes", 1f, 1f);
    }

    #endregion

    #region INITIALIZATION METHODS

    /// <summary>
    /// Sets up all supporters and connects their buttons to this manager.
    /// </summary>
    private void InitializeSupporters()
    {
        foreach (var supporter in allSupporters)
        {
            if (supporter != null && supporter.supporterData != null)
            {
                if (!supporterCounts.ContainsKey(supporter.supporterData))
                {
                    supporterCounts[supporter.supporterData] = 0;
                }

                supporter.SetManager(this);

                if (debugMode) Debug.Log($"Initialized supporter: {supporter.supporterData.supporterName}");
            }
        }
    }

    #endregion

    #region PUBLIC METHODS

    /// <summary>
    /// Attempts to buy a supporter - called by individual Supporter scripts
    /// </summary>
    /// <param name="supporterData">The supporter data to buy</param>
    /// <returns>True if purchase was successful</returns>
    public bool TryBuySupporter(SupporterData supporterData)
    {
        if (supporterData == null) return false;

        if (supporterCounts[supporterData] >= supporterData.maxSupporterCount)
        {
            if (debugMode) Debug.Log($"Cannot buy anymore {supporterData.supporterName}!");
            return false;
        }

        int cost = GetSupporterCost(supporterData);

        if (gameManager.GetPlayerVotes() >= cost)
        {
            gameManager.DecreasePlayerVotes(cost);

            if (!supporterCounts.ContainsKey(supporterData)) supporterCounts[supporterData] = 0;

            supporterCounts[supporterData]++;

            if (debugMode) Debug.Log($"Bought {supporterData.supporterName}! Count: {supporterCounts[supporterData]}, Votes: {gameManager.GetPlayerVotes()}");

            return true;
        }
        else
        {
            if (debugMode) Debug.Log($"Cannot afford {supporterData.supporterName}. Need: {cost}, Have: {gameManager.GetPlayerVotes()}");
            return false;
        }
    }

    /// <summary>
    /// Gets the current cost for a supporter
    /// </summary>
    public int GetSupporterCost(SupporterData supporterData)
    {
        if (supporterData == null) return 0;

        return supporterData.supporterCost;
    }

    /// <summary>
    /// Gets the current count for a supporter
    /// </summary>
    public int GetSupporterCount(SupporterData supporterData)
    {
        return supporterCounts.ContainsKey(supporterData) ? supporterCounts[supporterData] : 0;
    }

    /// <summary>
    /// Checks if the player can afford this supporter
    /// </summary>
    public bool CanAffordSupporter(SupporterData supporterData) => gameManager.GetPlayerVotes() >= supporterData.supporterCost;

    #endregion

    #region UTILITY METHODS

    /// <summary>
    /// Calculate total votes per second from all supporters
    /// </summary>
    private int GetTotalVotesPerSecond()
    {
        int totalVotes = 0;

        foreach (var pair in supporterCounts)
        {
            SupporterData supporterData = pair.Key;
            int count = pair.Value;

            totalVotes += supporterData.supporterVotePerSecond * count;
        }

        return totalVotes;
    }

    /// <summary>
    /// Increases the votes coming from the supporters
    /// </summary>
    private void AddSupporterVotes() => gameManager.IncreasePlayerVotes(GetTotalVotesPerSecond());

    #endregion
}
