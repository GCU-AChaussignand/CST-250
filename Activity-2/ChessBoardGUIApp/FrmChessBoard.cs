using ChessBoardClassLibrary.Models;
using ChessBoardClassLibrary.Services.BusinessLogicLayer;
namespace ChessBoardGUIApp;
public class FrmChessBoard : Form
{
    private readonly BoardLogic logic = new();
    private BoardModel board = new(8);
    private Button[,] buttons = new Button[0,0];
    private readonly ComboBox cboPiece = new();
    private readonly NumericUpDown nudSize = new();
    private readonly Panel pnlBoard = new();
    private readonly Label lblStatus = new();
    public FrmChessBoard() { InitializeComponent(); CreateNewBoard(); }
    private void InitializeComponent()
    {
        Text = "Activity 2 - ChessBoard"; StartPosition = FormStartPosition.CenterScreen; Width = 780; Height = 680;
        Controls.Add(new Label { Text = "Piece:", Location = new Point(20,20), AutoSize = true });
        cboPiece.Location = new Point(75,16); cboPiece.Width = 130; cboPiece.DropDownStyle = ComboBoxStyle.DropDownList; cboPiece.DataSource = Enum.GetValues(typeof(ChessPieceType)); Controls.Add(cboPiece);
        Controls.Add(new Label { Text = "Board Size:", Location = new Point(225,20), AutoSize = true });
        nudSize.Minimum = 4; nudSize.Maximum = 16; nudSize.Value = 8; nudSize.Location = new Point(305,16); Controls.Add(nudSize);
        Button btnNew = new() { Text = "New Board", Location = new Point(390,14), Width = 100 }; btnNew.Click += (s,e)=>CreateNewBoard(); Controls.Add(btnNew);
        Button btnClear = new() { Text = "Clear Moves", Location = new Point(505,14), Width = 100 }; btnClear.Click += (s,e)=>{ logic.ResetBoard(board); UpdateButtons(); lblStatus.Text="Moves cleared."; }; Controls.Add(btnClear);
        lblStatus.Text = "Select a piece and click a square."; lblStatus.Location = new Point(20,55); lblStatus.Width = 720; lblStatus.Height = 30; Controls.Add(lblStatus);
        pnlBoard.Location = new Point(20,95); pnlBoard.Width = 620; pnlBoard.Height = 520; pnlBoard.BorderStyle = BorderStyle.FixedSingle; pnlBoard.AutoScroll = true; Controls.Add(pnlBoard);
    }
    private void CreateNewBoard()
    {
        int size = (int)nudSize.Value; board = new BoardModel(size); buttons = new Button[size,size]; pnlBoard.Controls.Clear(); int cellSize = Math.Max(32, Math.Min(54, 500/size));
        for(int r=0;r<size;r++) for(int c=0;c<size;c++)
        { Button b = new() { Width=cellSize, Height=cellSize, Location=new Point(c*cellSize, r*cellSize), Text=".", Tag=Tuple.Create(r,c), BackColor=Color.White }; b.Click += BoardButton_Click; buttons[r,c]=b; pnlBoard.Controls.Add(b); }
        lblStatus.Text = $"New {size}x{size} board created.";
    }
    private void BoardButton_Click(object? sender, EventArgs e)
    {
        if (sender is not Button b || b.Tag is not Tuple<int,int> pos) return;
        ChessPieceType piece = (ChessPieceType)cboPiece.SelectedItem!;
        int count = logic.MarkLegalMoves(board, piece, pos.Item1, pos.Item2);
        UpdateButtons(); lblStatus.Text = $"{piece} selected at row {pos.Item1}, column {pos.Item2}. {count} legal moves marked.";
    }
    private void UpdateButtons()
    {
        for(int r=0;r<board.Size;r++) for(int c=0;c<board.Size;c++)
        { CellModel cell = board.Cells[r,c]; Button b = buttons[r,c]; b.Text = cell.IsSelected ? cell.Piece[..1] : cell.IsLegalMove ? "*" : "."; b.BackColor = cell.IsSelected ? Color.LightSkyBlue : cell.IsLegalMove ? Color.LightGreen : Color.White; }
    }
}
