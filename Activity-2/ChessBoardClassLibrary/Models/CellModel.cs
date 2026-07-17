namespace ChessBoardClassLibrary.Models;
public class CellModel
{
    public int Row { get; set; } = -1;
    public int Column { get; set; } = -1;
    public bool IsSelected { get; set; }
    public bool IsLegalMove { get; set; }
    public string Piece { get; set; } = string.Empty;
    public CellModel() { }
    public CellModel(int row, int column) { Row = row; Column = column; }
}
