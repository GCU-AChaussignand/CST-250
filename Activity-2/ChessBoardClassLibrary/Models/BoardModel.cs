namespace ChessBoardClassLibrary.Models;
public class BoardModel
{
    public int Size { get; set; }
    public CellModel[,] Cells { get; set; }
    public BoardModel(int size)
    {
        if (size < 4 || size > 16) throw new ArgumentOutOfRangeException(nameof(size), "Board size must be between 4 and 16.");
        Size = size;
        Cells = new CellModel[size, size];
        for (int row = 0; row < size; row++)
            for (int column = 0; column < size; column++)
                Cells[row, column] = new CellModel(row, column);
    }
}
