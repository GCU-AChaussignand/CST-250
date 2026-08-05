namespace MinesweeperClassLibrary.Models;

/// <summary>Represents the complete square Minesweeper board.</summary>
public class BoardModel
{
    public BoardModel(int size, int difficultyPercent)
    {
        if (size is < 5 or > 20) throw new ArgumentOutOfRangeException(nameof(size));
        if (difficultyPercent is < 5 or > 35) throw new ArgumentOutOfRangeException(nameof(difficultyPercent));
        Size = size;
        DifficultyPercent = difficultyPercent;
        Cells = new CellModel[size, size];
        for (int row = 0; row < size; row++)
            for (int column = 0; column < size; column++)
                Cells[row, column] = new CellModel(row, column);
    }

    public int Size { get; }
    public int DifficultyPercent { get; }
    public CellModel[,] Cells { get; }
    public int BombCount => Math.Max(1, (int)Math.Round(Size * Size * (DifficultyPercent / 100.0)));
    public int SafeCellCount => Size * Size - BombCount;
}
