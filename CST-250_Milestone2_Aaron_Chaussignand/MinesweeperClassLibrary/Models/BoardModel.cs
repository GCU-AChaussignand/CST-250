namespace MinesweeperClassLibrary.Models;

/// <summary>
/// Stores the data for a Minesweeper board.
/// Board behavior is implemented in the BusinessLogicLayer.
/// </summary>
public class BoardModel
{
    public BoardModel(int size)
    {
        Size = Math.Max(size, 1);
        StartTime = DateTime.Now;
        EndTime = DateTime.MinValue;
        Difficulty = 1;
        RewardsRemaining = 0;
        GameState = GameState.StillPlaying;
        Cells = new CellModel[Size, Size];

        for (int row = 0; row < Size; row++)
        {
            for (int column = 0; column < Size; column++)
            {
                Cells[row, column] = new CellModel(row, column);
            }
        }
    }

    public int Size { get; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public CellModel[,] Cells { get; }

    /// <summary>
    /// Represents the total number of bombs requested for the board.
    /// </summary>
    public int Difficulty { get; set; }

    public int RewardsRemaining { get; set; }

    public GameState GameState { get; set; }
}
