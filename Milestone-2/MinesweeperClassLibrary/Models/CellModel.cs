namespace MinesweeperClassLibrary.Models;

/// <summary>
/// Stores the data for one Minesweeper cell.
/// Business logic is intentionally kept out of the model class.
/// </summary>
public class CellModel
{
    public CellModel() : this(-1, -1)
    {
    }

    public CellModel(int row, int column)
    {
        Row = row;
        Column = column;
        IsVisited = false;
        IsBomb = false;
        IsFlagged = false;
        NumberOfBombNeighbors = 0;
        HasSpecialReward = false;
    }

    public int Row { get; set; }

    public int Column { get; set; }

    public bool IsVisited { get; set; }

    public bool IsBomb { get; set; }

    public bool IsFlagged { get; set; }

    public int NumberOfBombNeighbors { get; set; }

    public bool HasSpecialReward { get; set; }
}
