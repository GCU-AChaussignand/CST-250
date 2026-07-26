/*
 * Aaron Chaussignand
 * CST-250: Programming in C# II
 * 07/20/2026
 * Chess Board Project - Activity 2
 * Windows Forms presentation layer for the shared chessboard class library.
 */

using ChessBoardClassLibrary.Models;
using ChessBoardClassLibrary.Services.BusinessLogicLayer;

namespace ChessBoardGUIApp
{
    /// <summary>
    /// Presents the chessboard in a Windows Forms graphical user interface.
    /// All visible controls, including the 64 board buttons, are created in
    /// FrmChessBoard.Designer.cs so they remain editable in the Visual Studio Designer.
    /// </summary>
    public partial class FrmChessBoard : Form
    {
        private readonly BoardModel _board;
        private readonly BoardLogic _boardLogic;
        private readonly Button[,] _buttons;

        private readonly Dictionary<string, Color[]> _themeColors = new()
        {
            { "Classic", new[] { Color.White, Color.Gainsboro, Color.LightGreen, Color.Gold, Color.Black } },
            { "Cool", new[] { Color.AliceBlue, Color.LightSteelBlue, Color.Aquamarine, Color.DeepSkyBlue, Color.Black } },
            { "Warm", new[] { Color.LemonChiffon, Color.Moccasin, Color.LightSalmon, Color.OrangeRed, Color.Black } },
            { "Neon", new[] { Color.Black, Color.FromArgb(45, 45, 45), Color.Lime, Color.Magenta, Color.White } },
            { "Pastel", new[] { Color.LavenderBlush, Color.Lavender, Color.PaleGreen, Color.Plum, Color.Black } },
            { "Nature", new[] { Color.Honeydew, Color.Beige, Color.YellowGreen, Color.SaddleBrown, Color.Black } }
        };

        /// <summary>
        /// Initializes the form, shared business logic, and designer-created button grid.
        /// </summary>
        public FrmChessBoard()
        {
            InitializeComponent();

            _boardLogic = new BoardLogic();
            _board = new BoardModel(8);
            _buttons = new Button[,]
            {
                { btnSquare00, btnSquare01, btnSquare02, btnSquare03, btnSquare04, btnSquare05, btnSquare06, btnSquare07 },
                { btnSquare10, btnSquare11, btnSquare12, btnSquare13, btnSquare14, btnSquare15, btnSquare16, btnSquare17 },
                { btnSquare20, btnSquare21, btnSquare22, btnSquare23, btnSquare24, btnSquare25, btnSquare26, btnSquare27 },
                { btnSquare30, btnSquare31, btnSquare32, btnSquare33, btnSquare34, btnSquare35, btnSquare36, btnSquare37 },
                { btnSquare40, btnSquare41, btnSquare42, btnSquare43, btnSquare44, btnSquare45, btnSquare46, btnSquare47 },
                { btnSquare50, btnSquare51, btnSquare52, btnSquare53, btnSquare54, btnSquare55, btnSquare56, btnSquare57 },
                { btnSquare60, btnSquare61, btnSquare62, btnSquare63, btnSquare64, btnSquare65, btnSquare66, btnSquare67 },
                { btnSquare70, btnSquare71, btnSquare72, btnSquare73, btnSquare74, btnSquare75, btnSquare76, btnSquare77 }
            };

            SetUpButtons();
            cmbChessPieces.SelectedIndex = 0;
            cmbTheme.SelectedIndex = 0;
            UpdateButtons();
        }

        /// <summary>
        /// Connects the 64 buttons created by the Windows Forms Designer to the 2D array.
        /// The method adds coordinates to each Tag and attaches one shared click handler.
        /// It does not create controls, so the entire board remains visible in Design view.
        /// </summary>
        private void SetUpButtons()
        {
            for (int row = 0; row < _board.Size; row++)
            {
                for (int column = 0; column < _board.Size; column++)
                {
                    Button button = _buttons[row, column];
                    button.Tag = new Point(row, column);
                    button.Click -= BtnSquareClickEH;
                    button.Click += BtnSquareClickEH;
                    button.AccessibleName = $"Row {row}, Column {column}";
                }
            }
        }

        /// <summary>
        /// Handles a click on any chessboard square.
        /// </summary>
        private void BtnSquareClickEH(object? sender, EventArgs e)
        {
            if (sender is not Button button || button.Tag is not Point point)
            {
                MessageBox.Show(
                    this,
                    "The selected board square could not be identified.",
                    "Board Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            string piece = cmbChessPieces.Text.Trim();
            if (string.IsNullOrWhiteSpace(piece))
            {
                MessageBox.Show(
                    this,
                    "Select a chess piece before selecting a square.",
                    "Piece Required",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                cmbChessPieces.Focus();
                return;
            }

            int row = point.X;
            int column = point.Y;

            if (!_boardLogic.IsValidCoordinate(_board, row, column))
            {
                MessageBox.Show(
                    this,
                    "The selected square is outside the board.",
                    "Invalid Square",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            try
            {
                _boardLogic.MarkLegalMoves(_board, _board.Grid[row, column], piece);
                UpdateButtons();
                lblStatus.Text = $"{piece} selected at row {row}, column {column}.";
            }
            catch (ArgumentException exception)
            {
                MessageBox.Show(
                    this,
                    exception.Message,
                    "Unable to Mark Moves",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Updates button text and color from the current BoardModel grid.
        /// </summary>
        private void UpdateButtons()
        {
            Dictionary<string, string> pieceMap = new()
            {
                { "N", "Knight" },
                { "R", "Rook" },
                { "B", "Bishop" },
                { "Q", "Queen" },
                { "K", "King" }
            };

            Color[] colors = GetSelectedTheme();

            for (int row = 0; row < _board.Size; row++)
            {
                for (int column = 0; column < _board.Size; column++)
                {
                    CellModel cell = _board.Grid[row, column];
                    Button button = _buttons[row, column];
                    bool alternateSquare = (row + column) % 2 == 1;

                    button.ForeColor = colors[4];
                    button.BackColor = alternateSquare ? colors[1] : colors[0];
                    button.Text = string.Empty;

                    if (!string.IsNullOrWhiteSpace(cell.PieceOccupyingCell))
                    {
                        button.Text = pieceMap[cell.PieceOccupyingCell];
                        button.BackColor = colors[3];
                    }
                    else if (cell.IsLegalNextMove)
                    {
                        button.Text = "Legal Move";
                        button.BackColor = colors[2];
                    }
                }
            }
        }

        /// <summary>
        /// Clears the selected piece and all legal move indicators.
        /// </summary>
        private void BtnClearMoves_Click(object? sender, EventArgs e)
        {
            _boardLogic.ClearBoard(_board);
            UpdateButtons();
            lblStatus.Text = "The piece and legal move indicators were cleared.";
        }

        /// <summary>
        /// Resets the board, piece selection, and theme to their default values.
        /// </summary>
        private void BtnReset_Click(object? sender, EventArgs e)
        {
            _boardLogic.ClearBoard(_board);
            cmbChessPieces.SelectedIndex = 0;
            cmbTheme.SelectedIndex = 0;
            UpdateButtons();
            lblStatus.Text = "The application was reset to its default settings.";
        }

        /// <summary>
        /// Reapplies colors when the user chooses a different theme.
        /// </summary>
        private void CmbTheme_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (_buttons is not null)
            {
                UpdateButtons();
            }
        }

        /// <summary>
        /// Gets the colors for the currently selected theme.
        /// </summary>
        private Color[] GetSelectedTheme()
        {
            if (_themeColors.TryGetValue(cmbTheme.Text, out Color[]? colors)
                && colors is not null)
            {
                return colors;
            }

            return _themeColors["Classic"];
        }
    }
}
