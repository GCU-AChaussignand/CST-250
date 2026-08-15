using MinesweeperClassLibrary.BusinessLogicLayer;
using MinesweeperClassLibrary.Models;

namespace MinesweeperWinFormsApp;

public sealed class FrmMinesweeper : Form
{
    private readonly int _size;
    private readonly int _difficulty;
    private readonly IBoardLogic _logic = new BoardLogic();
    private BoardModel _board = null!;
    private CellButton[,] _buttons = null!;
    private readonly TableLayoutPanel _boardPanel = new() { Dock = DockStyle.Fill, BackColor = Color.FromArgb(190, 199, 207) };
    private readonly Label _timerLabel = new() { AutoSize = true, Text = "00:00" };
    private readonly Label _statusLabel = new() { AutoSize = true, MaximumSize = new Size(230, 0), Text = "Left-click to reveal. Right-click to flag." };
    private readonly Label _progressLabel = new() { AutoSize = true };
    private readonly Label _scoreLabel = new() { AutoSize = true, Text = "Score: -" };
    private readonly System.Windows.Forms.Timer _timer = new() { Interval = 1000 };
    private DateTime _startedAt;
    private bool _gameEnded;

    public FrmMinesweeper(int size, int difficulty)
    {
        _size = size;
        _difficulty = difficulty;
        Text = $"Minesweeper - {size} x {size} - {difficulty}%";
        StartPosition = FormStartPosition.CenterScreen;
        MinimumSize = new Size(780, 650);
        WindowState = size > 13 ? FormWindowState.Maximized : FormWindowState.Normal;
        Font = new Font("Segoe UI", 10F);
        BackColor = Color.FromArgb(243, 246, 250);
        BuildInterface();
        _timer.Tick += (_, _) => _timerLabel.Text = (DateTime.Now - _startedAt).ToString(@"mm\:ss");
        FormClosed += (_, _) => StopAndDisposeTimer();
        StartNewGame();
    }

    private void BuildInterface()
    {
        var sidebar = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(22), RowCount = 13, ColumnCount = 1, BackColor = Color.White };
        sidebar.Controls.Add(new Label { Text = "GAME STATUS", AutoSize = true, Font = new Font("Segoe UI Semibold", 16F), ForeColor = Color.FromArgb(25, 47, 70) });
        sidebar.Controls.Add(new Label { Text = "Elapsed time", AutoSize = true, ForeColor = Color.DimGray });
        _timerLabel.Font = new Font("Consolas", 22F, FontStyle.Bold);
        sidebar.Controls.Add(_timerLabel);
        sidebar.Controls.Add(new Label { Text = $"Board: {_size} x {_size}\nDifficulty: {_difficulty}%", AutoSize = true });
        sidebar.Controls.Add(_progressLabel);
        sidebar.Controls.Add(_scoreLabel);
        sidebar.Controls.Add(_statusLabel);

        var restartButton = CreateActionButton("Restart Game", Color.FromArgb(40, 111, 168));
        restartButton.Click += (_, _) => StartNewGame();
        var highScoresButton = CreateActionButton("High Scores", Color.FromArgb(43, 156, 166));
        highScoresButton.Click += (_, _) =>
        {
            using var highScores = new Form4();
            highScores.ShowDialog(this);
        };
        var newSettingsButton = CreateActionButton("Choose New Settings", Color.FromArgb(78, 89, 102));
        newSettingsButton.Click += (_, _) => Close();

        sidebar.Controls.Add(restartButton);
        sidebar.Controls.Add(highScoresButton);
        sidebar.Controls.Add(newSettingsButton);
        sidebar.Controls.Add(new Label { Text = "Controls\n- Left-click: reveal\n- Right-click: flag/unflag", AutoSize = true, ForeColor = Color.DimGray });

        var root = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, Padding = new Padding(12) };
        root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        root.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 270));
        root.Controls.Add(_boardPanel, 0, 0);
        root.Controls.Add(sidebar, 1, 0);
        Controls.Add(root);
    }

    private static Button CreateActionButton(string text, Color backColor)
    {
        var button = new Button { Text = text, Height = 44, Dock = DockStyle.Top, BackColor = backColor, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Margin = new Padding(0, 8, 0, 0) };
        button.FlatAppearance.BorderSize = 0;
        return button;
    }

    private void StartNewGame()
    {
        _timer.Stop();
        _gameEnded = false;
        _board = new BoardModel(_size, _difficulty);
        _logic.InitializeBoard(_board);
        BuildBoardButtons();
        _startedAt = DateTime.Now;
        _timerLabel.Text = "00:00";
        _scoreLabel.Text = "Score: -";
        _statusLabel.Text = "Left-click to reveal. Right-click to flag.";
        UpdateProgress();
        _timer.Start();
    }

    private void BuildBoardButtons()
    {
        _boardPanel.SuspendLayout();
        _boardPanel.Controls.Clear();
        _boardPanel.RowStyles.Clear();
        _boardPanel.ColumnStyles.Clear();
        _boardPanel.RowCount = _size;
        _boardPanel.ColumnCount = _size;
        _buttons = new CellButton[_size, _size];
        float percent = 100F / _size;

        for (int i = 0; i < _size; i++)
        {
            _boardPanel.RowStyles.Add(new RowStyle(SizeType.Percent, percent));
            _boardPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, percent));
        }

        for (int row = 0; row < _size; row++)
        {
            for (int column = 0; column < _size; column++)
            {
                var button = new CellButton(row, column)
                {
                    Dock = DockStyle.Fill,
                    BackColor = Color.FromArgb(101, 119, 134),
                    ForeColor = Color.White,
                    Text = string.Empty
                };
                button.MouseUp += CellButton_MouseUp;
                _buttons[row, column] = button;
                _boardPanel.Controls.Add(button, column, row);
            }
        }

        _boardPanel.ResumeLayout();
    }

    private void CellButton_MouseUp(object? sender, MouseEventArgs e)
    {
        if (_gameEnded || sender is not CellButton button)
            return;

        if (e.Button == MouseButtons.Right)
        {
            _logic.ToggleFlag(_board, button.Row, button.Column);
            _statusLabel.Text = "Flag status updated.";
        }
        else if (e.Button == MouseButtons.Left)
        {
            MoveResult result = _logic.RevealCell(_board, button.Row, button.Column);
            _statusLabel.Text = result.Message;
            if (result.State != GameState.StillPlaying)
                EndGame(result.State);
        }

        UpdateButtonFaces();
        UpdateProgress();
    }

    private void UpdateButtonFaces()
    {
        for (int row = 0; row < _size; row++)
        {
            for (int column = 0; column < _size; column++)
            {
                CellModel cell = _board.Cells[row, column];
                CellButton button = _buttons[row, column];

                if (cell.IsFlagged && !cell.IsVisited)
                {
                    button.Text = "F";
                    button.BackColor = Color.FromArgb(43, 156, 166);
                    button.ForeColor = Color.White;
                    continue;
                }

                if (!cell.IsVisited)
                {
                    button.Text = string.Empty;
                    button.BackColor = Color.FromArgb(101, 119, 134);
                    continue;
                }

                button.Enabled = false;
                if (cell.IsBomb)
                {
                    button.Text = "B";
                    button.BackColor = Color.FromArgb(204, 76, 76);
                    button.ForeColor = Color.White;
                }
                else
                {
                    button.Text = cell.NumberOfBombNeighbors == 0 ? string.Empty : cell.NumberOfBombNeighbors.ToString();
                    button.BackColor = Color.FromArgb(230, 235, 239);
                    button.ForeColor = NumberColor(cell.NumberOfBombNeighbors);
                }
            }
        }
    }

    private void StopAndDisposeTimer()
    {
        _timer.Stop();
        _timer.Dispose();
    }

    private static Color NumberColor(int number) => number switch
    {
        1 => Color.RoyalBlue,
        2 => Color.SeaGreen,
        3 => Color.Firebrick,
        4 => Color.DarkViolet,
        _ => Color.DarkSlateGray
    };

    private void UpdateProgress()
    {
        int revealed = _logic.CountRevealedSafeCells(_board);
        int flags = _board.Cells.Cast<CellModel>().Count(cell => cell.IsFlagged);
        _progressLabel.Text = $"Safe cells: {revealed}/{_board.SafeCellCount}\nFlags: {flags}\nBombs: {_board.BombCount}";
    }

    private void EndGame(GameState state)
    {
        _gameEnded = true;
        _timer.Stop();
        TimeSpan elapsed = DateTime.Now - _startedAt;

        if (state == GameState.Won)
        {
            int score = _logic.CalculateScore(_board, elapsed);
            _scoreLabel.Text = $"Score: {score:N0}";
            _statusLabel.Text = "Game won. Enter your name for the high-score list.";

            using var winnerForm = new Form3(score, elapsed);
            if (winnerForm.ShowDialog(this) == DialogResult.OK)
            {
                var gameStat = new GameStat
                {
                    Name = winnerForm.WinnerName,
                    Score = score,
                    GameTime = DateTime.Now
                };

                using var highScores = new Form4(gameStat);
                highScores.ShowDialog(this);
            }
        }
        else
        {
            MessageBox.Show("You hit a bomb. Try again!", "Minesweeper", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
