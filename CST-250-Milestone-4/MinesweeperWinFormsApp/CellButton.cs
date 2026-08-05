namespace MinesweeperWinFormsApp;
public sealed class CellButton : Button
{
    public CellButton(int row, int column) { Row = row; Column = column; Margin = new Padding(1); FlatStyle = FlatStyle.Flat; FlatAppearance.BorderSize = 0; Font = new Font("Segoe UI Semibold", 11F); }
    public int Row { get; }
    public int Column { get; }
}
