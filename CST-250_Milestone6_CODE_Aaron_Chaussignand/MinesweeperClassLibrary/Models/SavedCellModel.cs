namespace MinesweeperClassLibrary.Models;

/// <summary>
/// Serializable state for one cell in a saved Minesweeper game.
/// </summary>
public sealed class SavedCellModel
{
    public int Row { get; set; }
    public int Column { get; set; }
    public bool IsBomb { get; set; }
    public bool IsVisited { get; set; }
    public bool IsFlagged { get; set; }
    public int NumberOfBombNeighbors { get; set; }
}
