namespace MinesweeperClassLibrary.Models;

/// <summary>
/// Extends CellModel with data describing a special reward.
/// </summary>
public class RewardCellModel : CellModel
{
    public RewardCellModel(int row, int column, RewardType rewardType)
        : base(row, column)
    {
        RewardType = rewardType;
        HasSpecialReward = rewardType != RewardType.None;
    }

    public RewardType RewardType { get; set; }
}
