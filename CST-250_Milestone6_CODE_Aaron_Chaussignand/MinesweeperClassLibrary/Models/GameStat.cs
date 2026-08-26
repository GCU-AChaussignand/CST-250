namespace MinesweeperClassLibrary.Models;

/// <summary>
/// Represents one winning Minesweeper game for high-score tracking.
/// </summary>
public sealed class GameStat
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Score { get; set; }
    public DateTime GameTime { get; set; }
}
