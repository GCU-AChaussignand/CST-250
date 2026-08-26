namespace MinesweeperClassLibrary.Models;

/// <summary>
/// Serializable snapshot of a Minesweeper game used by the save/resume feature.
/// </summary>
public sealed class SavedGameModel
{
    public int Size { get; set; }
    public int DifficultyPercent { get; set; }
    public double ElapsedSeconds { get; set; }
    public string ThemeName { get; set; } = "Classic";
    public DateTime SavedAtUtc { get; set; } = DateTime.UtcNow;
    public List<SavedCellModel> Cells { get; set; } = new();
}
