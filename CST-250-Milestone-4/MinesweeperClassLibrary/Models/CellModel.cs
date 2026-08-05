namespace MinesweeperClassLibrary.Models;

/// <summary>Stores the state of one square. Behavior remains in the BLL.</summary>
public class CellModel
{
    public CellModel(int row, int column)
    {
        Row = row;
        Column = column;
    }

    public int Row { get; }
    public int Column { get; }
    public bool IsBomb { get; set; }
    public bool IsVisited { get; set; }
    public bool IsFlagged { get; set; }
    public int NumberOfBombNeighbors { get; set; }
}
