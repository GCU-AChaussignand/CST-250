namespace MinesweeperClassLibrary.Models;

/// <summary>
/// Represents optional reward types that can be added to cells in later milestones.
/// </summary>
public enum RewardType
{
    None,
    Hint,
    TimeFreeze,
    BombDefuseKit,
    BombSquad,
    Undo
}
