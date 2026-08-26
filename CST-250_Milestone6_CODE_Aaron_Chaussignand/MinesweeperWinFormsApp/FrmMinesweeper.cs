using MinesweeperClassLibrary.BusinessLogicLayer;
using MinesweeperClassLibrary.DataAccessLayer;
using MinesweeperClassLibrary.Models;

namespace MinesweeperWinFormsApp;

public sealed class FrmMinesweeper : Form
{
    private int _size;
    private int _difficulty;
    private readonly IBoardLogic _logic = new BoardLogic();
    private readonly ThemeLogic _themeLogic = new();
    private readonly GameSaveLogic _saveLogic = new();
    private readonly SavedGameRepository _savedGameRepository = new();
    private BoardModel _board = null!;
    private CellButton[,] _buttons = null!;
    private GameTheme _theme = new ClassicGameTheme();
    private readonly TableLayoutPanel _boardPanel = new() { Dock = DockStyle.Fill };
    private readonly TableLayoutPanel _sidebar = new() { Dock = DockStyle.Fill, Padding = new Padding(22), ColumnCount = 1 };
    private readonly Label _timerLabel = new() { AutoSize = true, Text = "00:00" };
    private readonly Label _settingsLabel = new() { AutoSize = true };
    private readonly Label _statusLabel = new() { AutoSize = true, MaximumSize = new Size(230, 0), Text = "Left-click to reveal. Right-click to flag." };
    private readonly Label _progressLabel = new() { AutoSize = true };
    private readonly Label _scoreLabel = new() { AutoSize = true, Text = "Score: -" };
    private readonly ComboBox _themeComboBox = new() { Dock = DockStyle.Top, DropDownStyle = ComboBoxStyle.DropDownList };
    private readonly List<Button> _actionButtons = new();
    private readonly System.Windows.Forms.Timer _timer = new() { Interval = 1000 };
    private DateTime _startedAt;
    private bool _gameEnded;

    public FrmMinesweeper(int size, int difficulty, string themeName = "Classic")
    {
        _size = size;
        _difficulty = difficulty;
        _theme = _themeLogic.FindByName(themeName);
        ConfigureWindow();
        BuildInterface();
        ConfigureEvents();
        StartNewGame();
    }

    public FrmMinesweeper(SavedGameModel snapshot)
    {
        ArgumentNullException.ThrowIfNull(snapshot);
        _board = _saveLogic.RestoreBoard(snapshot);
        _size = _board.Size;
        _difficulty = _board.DifficultyPercent;
        _theme = _themeLogic.FindByName(snapshot.ThemeName);
        ConfigureWindow();
        BuildInterface();
        ConfigureEvents();
        LoadSnapshot(snapshot);
    }

    private void ConfigureWindow()
    {
        Text = $"Minesweeper - {_size} x {_size} - {_difficulty}%";
        StartPosition = FormStartPosition.CenterScreen;
        MinimumSize = new Size(860, 670);
        WindowState = _size > 13 ? FormWindowState.Maximized : FormWindowState.Normal;
        Font = new Font("Segoe UI", 10F);
    }

    private void ConfigureEvents()
    {
        _timer.Tick += (_, _) => _timerLabel.Text = (DateTime.Now - _startedAt).ToString(@"mm\:ss");
        FormClosed += (_, _) => StopAndDisposeTimer();
        _themeComboBox.SelectionChangeCommitted += (_, _) =>
        {
            _theme = _themeLogic.FindByName(_themeComboBox.SelectedItem?.ToString());
            ApplyTheme();
            UpdateWindowSettings();
            UpdateButtonFaces();
        };
    }

    private void BuildInterface()
    {
        _sidebar.RowCount = 17;
        _sidebar.Controls.Add(new Label { Text = "GAME STATUS", AutoSize = true, Font = new Font("Segoe UI Semibold", 16F), Tag = "heading" });
        _sidebar.Controls.Add(new Label { Text = "Elapsed time", AutoSize = true, Tag = "muted" });
        _timerLabel.Font = new Font("Consolas", 22F, FontStyle.Bold);
        _sidebar.Controls.Add(_timerLabel);
        _sidebar.Controls.Add(_settingsLabel);
        _sidebar.Controls.Add(_progressLabel);
        _sidebar.Controls.Add(_scoreLabel);
        _sidebar.Controls.Add(_statusLabel);
        _sidebar.Controls.Add(new Label { Text = "Theme / skin", AutoSize = true, Font = new Font(Font, FontStyle.Bold) });
        _themeComboBox.DataSource = _themeLogic.GetThemes().Select(theme => theme.Name).ToList();
        _themeComboBox.SelectedItem = _theme.Name;
        _sidebar.Controls.Add(_themeComboBox);

        var restartButton = CreateActionButton("Restart Game");
        restartButton.Click += (_, _) => StartNewGame();
        var saveButton = CreateActionButton("Save Game (JSON)");
        saveButton.Click += SaveGame_Click;
        var resumeButton = CreateActionButton("Resume Game (JSON)");
        resumeButton.Click += ResumeGame_Click;
        var highScoresButton = CreateActionButton("High Scores");
        highScoresButton.Click += (_, _) =>
        {
            using var highScores = new Form4();
            highScores.ShowDialog(this);
        };
        var newSettingsButton = CreateActionButton("Choose New Settings");
        newSettingsButton.Click += (_, _) => Close();

        _sidebar.Controls.Add(restartButton);
        _sidebar.Controls.Add(saveButton);
        _sidebar.Controls.Add(resumeButton);
        _sidebar.Controls.Add(highScoresButton);
        _sidebar.Controls.Add(newSettingsButton);
        _sidebar.Controls.Add(new Label { Text = "Controls\n- Left-click: reveal\n- Right-click: flag/unflag", AutoSize = true, Tag = "muted" });

        var root = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, Padding = new Padding(12) };
        root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        root.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 290));
        root.Controls.Add(_boardPanel, 0, 0);
        root.Controls.Add(_sidebar, 1, 0);
        Controls.Add(root);
        ApplyTheme();
    }

    private Button CreateActionButton(string text)
    {
        var button = new Button { Text = text, Height = 40, Dock = DockStyle.Top, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Margin = new Padding(0, 6, 0, 0) };
        button.FlatAppearance.BorderSize = 0;
        _actionButtons.Add(button);
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
        UpdateWindowSettings();
        UpdateProgress();
        ApplyTheme();
        _timer.Start();
    }

    private void LoadSnapshot(SavedGameModel snapshot)
    {
        _timer.Stop();
        _gameEnded = _logic.DetermineGameState(_board) != GameState.StillPlaying;
        BuildBoardButtons();
        _startedAt = DateTime.Now - TimeSpan.FromSeconds(Math.Max(0, snapshot.ElapsedSeconds));
        _timerLabel.Text = (DateTime.Now - _startedAt).ToString(@"mm\:ss");
        _scoreLabel.Text = "Score: -";
        _statusLabel.Text = _gameEnded ? "Loaded game is already complete." : "Saved game resumed successfully.";
        _themeComboBox.SelectedItem = _theme.Name;
        UpdateWindowSettings();
        UpdateButtonFaces();
        UpdateProgress();
        ApplyTheme();
        if (!_gameEnded)
        {
            _timer.Start();
        }
    }

    private void BuildBoardButtons()
    {
        _boardPanel.SuspendLayout();
        _boardPanel.Controls.Clear();
        _boardPanel.RowStyles.Clear();
        _boardPanel.ColumnStyles.Clear();
        _boardPanel.RowCount = _board.Size;
        _boardPanel.ColumnCount = _board.Size;
        _buttons = new CellButton[_board.Size, _board.Size];
        float percent = 100F / _board.Size;

        for (int i = 0; i < _board.Size; i++)
        {
            _boardPanel.RowStyles.Add(new RowStyle(SizeType.Percent, percent));
            _boardPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, percent));
        }

        for (int row = 0; row < _board.Size; row++)
        {
            for (int column = 0; column < _board.Size; column++)
            {
                var button = new CellButton(row, column)
                {
                    Dock = DockStyle.Fill,
                    ForeColor = Color.White,
                    Text = string.Empty
                };
                button.MouseUp += CellButton_MouseUp;
                _buttons[row, column] = button;
                _boardPanel.Controls.Add(button, column, row);
            }
        }

        _boardPanel.ResumeLayout();
        UpdateButtonFaces();
    }

    private void CellButton_MouseUp(object? sender, MouseEventArgs e)
    {
        if (_gameEnded || sender is not CellButton button)
        {
            return;
        }

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
            {
                EndGame(result.State);
            }
        }

        UpdateButtonFaces();
        UpdateProgress();
    }

    private void UpdateButtonFaces()
    {
        if (_buttons is null || _board is null)
        {
            return;
        }

        for (int row = 0; row < _board.Size; row++)
        {
            for (int column = 0; column < _board.Size; column++)
            {
                CellModel cell = _board.Cells[row, column];
                CellButton button = _buttons[row, column];
                button.Enabled = !_gameEnded && !cell.IsVisited;

                if (cell.IsFlagged && !cell.IsVisited)
                {
                    button.Text = "F";
                    button.BackColor = ThemeColor(_theme.FlagHex);
                    button.ForeColor = Color.White;
                    continue;
                }

                if (!cell.IsVisited)
                {
                    button.Text = string.Empty;
                    button.BackColor = ThemeColor(_theme.CoveredCellHex);
                    button.ForeColor = Color.White;
                    continue;
                }

                button.Enabled = false;
                if (cell.IsBomb)
                {
                    button.Text = "B";
                    button.BackColor = ThemeColor(_theme.BombHex);
                    button.ForeColor = Color.White;
                }
                else
                {
                    button.Text = cell.NumberOfBombNeighbors == 0 ? string.Empty : cell.NumberOfBombNeighbors.ToString();
                    button.BackColor = ThemeColor(_theme.RevealedCellHex);
                    button.ForeColor = NumberColor(cell.NumberOfBombNeighbors);
                }
            }
        }
    }

    private void ApplyTheme()
    {
        BackColor = ThemeColor(_theme.WindowBackgroundHex);
        _boardPanel.BackColor = ThemeColor(_theme.WindowBackgroundHex);
        _sidebar.BackColor = ThemeColor(_theme.PanelBackgroundHex);
        _settingsLabel.ForeColor = ThemeColor(_theme.TextHex);
        _timerLabel.ForeColor = ThemeColor(_theme.TextHex);
        _progressLabel.ForeColor = ThemeColor(_theme.TextHex);
        _scoreLabel.ForeColor = ThemeColor(_theme.TextHex);
        _statusLabel.ForeColor = ThemeColor(_theme.TextHex);

        foreach (Control control in _sidebar.Controls)
        {
            if (control is Label label)
            {
                label.ForeColor = label.Tag?.ToString() == "muted"
                    ? BlendWithBackground(ThemeColor(_theme.TextHex), ThemeColor(_theme.PanelBackgroundHex))
                    : ThemeColor(_theme.TextHex);
            }
        }

        foreach (Button button in _actionButtons)
        {
            button.BackColor = ThemeColor(_theme.AccentHex);
            button.ForeColor = Color.White;
        }
    }

    private static Color ThemeColor(string hex) => ColorTranslator.FromHtml(hex);

    private static Color BlendWithBackground(Color foreground, Color background)
    {
        const double ratio = 0.65;
        int r = (int)(foreground.R * ratio + background.R * (1 - ratio));
        int g = (int)(foreground.G * ratio + background.G * (1 - ratio));
        int b = (int)(foreground.B * ratio + background.B * (1 - ratio));
        return Color.FromArgb(r, g, b);
    }

    private void UpdateWindowSettings()
    {
        _size = _board.Size;
        _difficulty = _board.DifficultyPercent;
        Text = $"Minesweeper - {_size} x {_size} - {_difficulty}% - {_theme.Name}";
        _settingsLabel.Text = $"Board: {_size} x {_size}\nDifficulty: {_difficulty}%\nTheme: {_theme.Name}";
    }

    private void SaveGame_Click(object? sender, EventArgs e)
    {
        if (_gameEnded)
        {
            MessageBox.Show("Start or resume an active game before saving.", "Save Game", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        using var dialog = new SaveFileDialog
        {
            Title = "Save Minesweeper Game",
            Filter = "Minesweeper save (*.json)|*.json|JSON files (*.json)|*.json",
            DefaultExt = "json",
            AddExtension = true,
            FileName = $"Minesweeper-{DateTime.Now:yyyyMMdd-HHmm}.json"
        };

        if (dialog.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        try
        {
            SavedGameModel snapshot = _saveLogic.CreateSnapshot(_board, DateTime.Now - _startedAt, _theme.Name);
            _savedGameRepository.Save(dialog.FileName, snapshot);
            _statusLabel.Text = $"Game saved at {DateTime.Now:t}.";
            MessageBox.Show("Game progress saved successfully as JSON.", "Save Game", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            MessageBox.Show($"The game could not be saved.\n\n{ex.Message}", "Save Game", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void ResumeGame_Click(object? sender, EventArgs e)
    {
        using var dialog = new OpenFileDialog
        {
            Title = "Resume Minesweeper Game",
            Filter = "Minesweeper save (*.json)|*.json|JSON files (*.json)|*.json|All files (*.*)|*.*"
        };

        if (dialog.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        try
        {
            SavedGameModel snapshot = _savedGameRepository.Load(dialog.FileName);
            _board = _saveLogic.RestoreBoard(snapshot);
            _size = _board.Size;
            _difficulty = _board.DifficultyPercent;
            _theme = _themeLogic.FindByName(snapshot.ThemeName);
            LoadSnapshot(snapshot);
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or System.Text.Json.JsonException or InvalidDataException)
        {
            MessageBox.Show($"The saved game could not be resumed.\n\n{ex.Message}", "Resume Game", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
