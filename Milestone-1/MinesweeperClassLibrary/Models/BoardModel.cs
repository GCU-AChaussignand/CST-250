namespace MinesweeperClassLibrary.Models;

/// <summary>
/// Stores data for the Minesweeper board. Logic belongs in the BusinessLogicLayer.
/// </summary>
public class BoardModel
{
    public BoardModel(int size)
    {
        if (size < 1)
        {
            size = 1;
        }

        Size = size;
        StartTime = DateTime.Now;
        EndTime = DateTime.MinValue;
        Difficulty = 1;
        RewardsRemaining = 0;
        GameState = GameState.StillPlaying;
        Cells = new CellModel[size, size];

        for (int row = 0; row < size; row++)
        {
            for (int column = 0; column < size; column++)
            {
                Cells[row, column] = new CellModel(row, column);
            }
        }
    }

    public int Size { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public CellModel[,] Cells { get; set; }

    /// <summary>
    /// In this milestone, Difficulty represents the number of bombs placed on the board.
    /// </summary>
    public int Difficulty { get; set; }

    public int RewardsRemaining { get; set; }

    public GameState GameState { get; set; }
}
