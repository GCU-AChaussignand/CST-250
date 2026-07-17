namespace MinesweeperClassLibrary.Models;

/// <summary>
/// Demonstrates inheritance by extending CellModel with reward data.
/// </summary>
public class RewardCellModel : CellModel
{
    public RewardCellModel(int row, int column, RewardType rewardType) : base(row, column)
    {
        RewardType = rewardType;
        HasSpecialReward = rewardType != RewardType.None;
    }

    public RewardType RewardType { get; set; }
}
