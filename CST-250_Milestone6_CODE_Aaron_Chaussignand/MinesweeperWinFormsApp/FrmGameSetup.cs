using MinesweeperClassLibrary.BusinessLogicLayer;
using MinesweeperClassLibrary.DataAccessLayer;
using MinesweeperClassLibrary.Models;

namespace MinesweeperWinFormsApp;

public sealed class FrmGameSetup : Form
{
    private readonly TrackBar _sizeTrackBar = new() { Minimum = 5, Maximum = 20, Value = 10, TickFrequency = 1, Dock = DockStyle.Fill };
    private readonly TrackBar _difficultyTrackBar = new() { Minimum = 5, Maximum = 35, Value = 15, TickFrequency = 5, Dock = DockStyle.Fill };
    private readonly Label _sizeValueLabel = new() { AutoSize = true, Text = "10 x 10" };
    private readonly Label _difficultyValueLabel = new() { AutoSize = true, Text = "15% bombs" };
    private readonly ComboBox _themeComboBox = new() { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList };
    private readonly ThemeLogic _themeLogic = new();
    private readonly SavedGameRepository _savedGameRepository = new();
    private readonly GameSaveLogic _saveLogic = new();

    public FrmGameSetup()
    {
        Text = "Minesweeper - New Game";
        StartPosition = FormStartPosition.CenterScreen;
        MinimumSize = new Size(540, 610);
        BackColor = Color.FromArgb(243, 246, 250);
        Font = new Font("Segoe UI", 10F);
        _themeComboBox.DataSource = _themeLogic.GetThemes().Select(theme => theme.Name).ToList();
        BuildInterface();
        _sizeTrackBar.ValueChanged += (_, _) => _sizeValueLabel.Text = $"{_sizeTrackBar.Value} x {_sizeTrackBar.Value}";
        _difficultyTrackBar.ValueChanged += (_, _) => _difficultyValueLabel.Text = $"{_difficultyTrackBar.Value}% bombs";
    }

    private void BuildInterface()
    {
        var title = new Label { Text = "MINESWEEPER", Font = new Font("Segoe UI Semibold", 25F), AutoSize = true, ForeColor = Color.FromArgb(25, 47, 70), Anchor = AnchorStyles.None };
        var subtitle = new Label { Text = "Choose a board size, difficulty, and theme to begin.", AutoSize = true, ForeColor = Color.DimGray, Anchor = AnchorStyles.None };
        var playButton = CreateButton("Play New Game", Color.FromArgb(40, 111, 168));
        playButton.Click += PlayButton_Click;
        var resumeButton = CreateButton("Resume Saved Game", Color.FromArgb(43, 156, 166));
        resumeButton.Click += ResumeButton_Click;
        var scoresButton = CreateButton("View High Scores", Color.FromArgb(78, 89, 102));
        scoresButton.Click += (_, _) =>
        {
            using var highScores = new Form4();
            highScores.ShowDialog(this);
        };

        var layout = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(42), ColumnCount = 1, RowCount = 15 };
        int[] heights = { 60, 42, 30, 55, 35, 30, 55, 35, 30, 42, 20, 52, 52, 50 };
        foreach (int height in heights)
        {
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, height));
        }
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

        layout.Controls.Add(title, 0, 0);
        layout.Controls.Add(subtitle, 0, 1);
        layout.Controls.Add(new Label { Text = "Board size", AutoSize = true, Font = new Font(Font, FontStyle.Bold) }, 0, 2);
        layout.Controls.Add(_sizeTrackBar, 0, 3);
        layout.Controls.Add(_sizeValueLabel, 0, 4);
        layout.Controls.Add(new Label { Text = "Difficulty", AutoSize = true, Font = new Font(Font, FontStyle.Bold) }, 0, 5);
        layout.Controls.Add(_difficultyTrackBar, 0, 6);
        layout.Controls.Add(_difficultyValueLabel, 0, 7);
        layout.Controls.Add(new Label { Text = "Theme / skin", AutoSize = true, Font = new Font(Font, FontStyle.Bold) }, 0, 8);
        layout.Controls.Add(_themeComboBox, 0, 9);
        layout.Controls.Add(playButton, 0, 11);
        layout.Controls.Add(resumeButton, 0, 12);
        layout.Controls.Add(scoresButton, 0, 13);
        Controls.Add(layout);
    }

    private static Button CreateButton(string text, Color color)
    {
        var button = new Button { Text = text, Height = 44, Dock = DockStyle.Fill, BackColor = color, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
        button.FlatAppearance.BorderSize = 0;
        return button;
    }

    private void PlayButton_Click(object? sender, EventArgs e)
    {
        string themeName = _themeComboBox.SelectedItem?.ToString() ?? "Classic";
        Hide();
        using var gameForm = new FrmMinesweeper(_sizeTrackBar.Value, _difficultyTrackBar.Value, themeName);
        gameForm.ShowDialog(this);
        Show();
    }

    private void ResumeButton_Click(object? sender, EventArgs e)
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
            _saveLogic.ValidateSnapshot(snapshot);
            Hide();
            using var gameForm = new FrmMinesweeper(snapshot);
            gameForm.ShowDialog(this);
            Show();
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or System.Text.Json.JsonException or InvalidDataException)
        {
            MessageBox.Show($"The saved game could not be opened.\n\n{ex.Message}", "Resume Game", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
