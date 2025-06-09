// Author: Alptuğ Yılmaz
// Date: 09.06.2025
// Description: Individual Supporter that cnnects to SupporterManager.

using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Individual supporter that handles its own UI and connects to SupporterManager.
/// </summary>
public class Supporter : MonoBehaviour
{
    #region VARIABLES

    /// <summary>
    /// Enables or disables the Debug mode.
    /// </summary>
    [Header("Debugging"), Tooltip("Enables or disables the Debug mode for logging.")]
    public bool debugMode = false;

    /// <summary>
    /// Represents the SupporterData scriptable object for the supporter. 
    /// </summary>
    [Header("Supporter Settings")]
    public SupporterData supporterData;

    /// <summary>
    /// Reference to the SupporterManager.
    /// </summary>
    private SupporterManager _supporterManager;

    /// <summary>
    /// Button for buying this supporter.
    /// </summary>
    private Button buyButton;

    #endregion

    #region UNITY METHODS

    /// <summary>
    /// Initial setup.
    /// </summary>
    void Start()
    {
        SetSupporterSprite();
        SetupBuyButton();
    }

    #endregion

    #region INITIALIZATION METHODS

    /// <summary>
    /// Called by SupporterManager to set the reference.
    /// </summary>
    public void SetManager(SupporterManager supporterManager)
    {
        _supporterManager = supporterManager;

        if (debugMode) Debug.Log($"Manager set for {supporterData?.supporterName}");
    }

    /// <summary>
    /// Sets up the BUY button.
    /// </summary>
    private void SetupBuyButton()
    {
        buyButton = GetComponentInChildren<Button>();

        if (buyButton != null)
        {
            buyButton.onClick.RemoveAllListeners();

            buyButton.onClick.AddListener(OnBuyButtonClicked);

            if (debugMode) Debug.Log($"Button setup complete for {supporterData?.supporterName}");
        }
        else
        {
            if (debugMode) Debug.LogWarning($"No Button component found for {supporterData?.supporterName}");
        }
    }

    #endregion

    #region VISUAL SETUP

    private void SetSupporterSprite()
    {
        if (supporterData?.supporterSprite == null) return;

        Image uiImage = GetComponent<Image>();
        if (uiImage != null)
        {
            uiImage.sprite = supporterData.supporterSprite;

            if (debugMode) Debug.Log("Set sprite for UI image.");
            return;
        }

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = supporterData.supporterSprite;

            if (debugMode) Debug.Log("Set sprite for SpriteRenderer.");
            return;
        }

        if (debugMode) Debug.LogWarning("No Image or SpriteRenderer component found!");
    }

    #endregion

    #region EVENT HANDLERS

    /// <summary>
    /// Called when buy button is clicked
    /// </summary>
    private void OnBuyButtonClicked()
    {
        if (_supporterManager == null)
        {
            if (debugMode) Debug.LogError("SupporterManager is null! Make sure SupporterManager initializes this supporter.");
            return;
        }

        if (supporterData == null)
        {
            if (debugMode) Debug.LogError("SupporterData is null!");
            return;
        }

        bool success = _supporterManager.TryBuySupporter(supporterData);

        if (debugMode)
        {
            if (success) Debug.Log($"Successfully bought {supporterData.supporterName}!");
            else Debug.Log($"Failed to buy {supporterData.supporterName} - not enough money");
        }
    }

    #endregion

    #region PUBLIC GETTERS

    /// <summary>
    /// Gets SupporterData for external access.
    /// </summary>
    public SupporterData GetSupporterData() => supporterData;

    #endregion
}
