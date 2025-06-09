// Author: Alptuð Yýlmaz
// Date: 09.06.2025
// Description: Manages the clicking of the ballot box, increasing player votes.

using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Controls the vote-clicking logic for the ballot box in the game scene.
/// </summary>
public class BallotBox : MonoBehaviour, IPointerClickHandler
{
    #region VARIABLES

    /// <summary>
    /// Represents the number of votes acquired by the player when clicking the ballot box.
    /// </summary>
    private int voteToAdd = 1;

    /// <summary>
    /// Represents the GameManager GameObject in the scene.
    /// </summary>
    private GameObject gameManager;

    #endregion

    /// <summary>
    /// Initialize the GameManager GameObject
    /// </summary>
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
    }

    /// <summary>
    /// Increases the player's vote count by the specified amount when the ballot box is left-clicked.
    /// </summary>
    /// <param name="eventData">Event data containing information about the pointer click.</param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log("Ballot box left-clicked! Adding votes.");
            gameManager.GetComponent<GameManager>().IncreasePlayerVotes(voteToAdd);
        }
    }
}
