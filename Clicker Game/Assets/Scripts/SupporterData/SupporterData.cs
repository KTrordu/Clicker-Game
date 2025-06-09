// Author: Alptuğ Yılmaz
// Date: 09.06.2025
// Description: Defines the Supporters' scriptable object, their basic information, cost, income.

using UnityEngine;

/// <summary>
/// Defines the Supporters' scriptable object.
/// </summary>
[CreateAssetMenu(fileName = "NewSupporterData", menuName = "Game/Supporter Data")]
public class SupporterData : ScriptableObject
{
    #region VARIABLES

    /// <summary>
    /// Represents the supporters' basic information.
    /// </summary>
    [Header("Supporter Information")]
    public string supporterName;
    public string supporterDescription;
    public Sprite supporterSprite;

    /// <summary>
    /// Represents the supporters' acquired votes/second for each supporter and their costs.
    /// </summary>
    [Header("Supporter Properties")]
    public int supporterCost;
    public int supporterVotePerSecond;
    public int maxSupporterCount;

    #endregion
}
