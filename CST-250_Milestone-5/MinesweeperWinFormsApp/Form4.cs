using MinesweeperClassLibrary.BusinessLogicLayer;
using MinesweeperClassLibrary.DataAccessLayer;
using MinesweeperClassLibrary.Models;

namespace MinesweeperWinFormsApp;

/// <summary>
/// Displays, saves, loads, and sorts Minesweeper high scores.
/// </summary>
public sealed class Form4 : Form
{
    private readonly GameStatLogic _logic = new();
    private readonly GameStatRepository _repository = new();
    private readonly DataGridView _scoreGrid = new();
    private readonly Label _statusLabel = new() { AutoSize = true, ForeColor = Color.DimGray };
    private readonly string _defaultFilePath = Path.Combine(AppContext.BaseDirectory, "Data", "GameStats.txt");
    private List<GameStat> _scores = new();
    private string _currentFilePath;

    public Form4(GameStat? newScore = null)
    {
        _currentFilePath = _defaultFilePath;
        Text = "Minesweeper - High Scores";
        StartPosition = FormStartPosition.CenterParent;
        MinimumSize = new Size(720, 500);
        ClientSize = new Size(820, 560);
        Font = new Font("Segoe UI", 10F);
        BackColor = Color.FromArgb(243, 246, 250);

        BuildInterface();
        LoadDefaultScores();

        if (newScore is not null)
        {
            newScore.Id = _logic.GetNextId(_scores);
            _scores.Add(newScore);
            _scores = _logic.SortByScore(_scores);
            SaveCurrentFile(silent: false, showSuccessMessage: false);
        }

        RefreshGrid();
    }

    private void BuildInterface()
    {
        var menu = new MenuStrip();
        var fileMenu = new ToolStripMenuItem("File");
        var saveMenuItem = new ToolStripMenuItem("Save");
        var loadMenuItem = new ToolStripMenuItem("Load");
        var exitMenuItem = new ToolStripMenuItem("Exit");
        saveMenuItem.Click += SaveMenuItem_Click;
        loadMenuItem.Click += LoadMenuItem_Click;
        exitMenuItem.Click += (_, _) => Close();
        fileMenu.DropDownItems.Add(saveMenuItem);
        fileMenu.DropDownItems.Add(loadMenuItem);
        fileMenu.DropDownItems.Add(new ToolStripSeparator());
        fileMenu.DropDownItems.Add(exitMenuItem);

        var sortMenu = new ToolStripMenuItem("Sort");
        var byNameMenuItem = new ToolStripMenuItem("By Name");
        var byScoreMenuItem = new ToolStripMenuItem("By Score");
        var byDateMenuItem = new ToolStripMenuItem("By Date");
        byNameMenuItem.Click += (_, _) => ApplySort(_logic.SortByName(_scores), "Sorted by name.");
        byScoreMenuItem.Click += (_, _) => ApplySort(_logic.SortByScore(_scores), "Sorted by score.");
        byDateMenuItem.Click += (_, _) => ApplySort(_logic.SortByDate(_scores), "Sorted by date.");
        sortMenu.DropDownItems.Add(byNameMenuItem);
        sortMenu.DropDownItems.Add(byScoreMenuItem);
        sortMenu.DropDownItems.Add(byDateMenuItem);

        menu.Items.Add(fileMenu);
        menu.Items.Add(sortMenu);
        MainMenuStrip = menu;

        var title = new Label
        {
            Text = "HIGH SCORES",
            AutoSize = true,
            Font = new Font("Segoe UI Semibold", 21F),
            ForeColor = Color.FromArgb(25, 47, 70)
        };

        var subtitle = new Label
        {
            Text = "Winning games are stored in a text file and can be sorted by name, score, or date.",
            AutoSize = true,
            ForeColor = Color.DimGray
        };

        ConfigureGrid();

        var closeButton = new Button
        {
            Text = "OK",
            Width = 110,
            Height = 40,
            Anchor = AnchorStyles.Right,
            BackColor = Color.FromArgb(40, 111, 168),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat
        };
        closeButton.FlatAppearance.BorderSize = 0;
        closeButton.Click += (_, _) => Close();

        var footer = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2 };
        footer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        footer.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120));
        footer.Controls.Add(_statusLabel, 0, 0);
        footer.Controls.Add(closeButton, 1, 0);

        var content = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(24),
            ColumnCount = 1,
            RowCount = 4
        };
        content.RowStyles.Add(new RowStyle(SizeType.Absolute, 46));
        content.RowStyles.Add(new RowStyle(SizeType.Absolute, 36));
        content.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        content.RowStyles.Add(new RowStyle(SizeType.Absolute, 52));
        content.Controls.Add(title, 0, 0);
        content.Controls.Add(subtitle, 0, 1);
        content.Controls.Add(_scoreGrid, 0, 2);
        content.Controls.Add(footer, 0, 3);

        Controls.Add(content);
        Controls.Add(menu);
    }

    private void ConfigureGrid()
    {
        _scoreGrid.Dock = DockStyle.Fill;
        _scoreGrid.AutoGenerateColumns = false;
        _scoreGrid.ReadOnly = true;
        _scoreGrid.AllowUserToAddRows = false;
        _scoreGrid.AllowUserToDeleteRows = false;
        _scoreGrid.AllowUserToResizeRows = false;
        _scoreGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _scoreGrid.MultiSelect = false;
        _scoreGrid.RowHeadersVisible = false;
        _scoreGrid.BackgroundColor = Color.White;
        _scoreGrid.BorderStyle = BorderStyle.FixedSingle;
        _scoreGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        _scoreGrid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Id", DataPropertyName = nameof(GameStat.Id), FillWeight = 20 });
        _scoreGrid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Name", DataPropertyName = nameof(GameStat.Name), FillWeight = 45 });
        _scoreGrid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Score", DataPropertyName = nameof(GameStat.Score), FillWeight = 30, DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" } });
        _scoreGrid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Date", DataPropertyName = nameof(GameStat.GameTime), FillWeight = 45, DefaultCellStyle = new DataGridViewCellStyle { Format = "g" } });
    }

    private void LoadDefaultScores()
    {
        try
        {
            if (File.Exists(_defaultFilePath))
            {
                _scores = _logic.SortByScore(_repository.Load(_defaultFilePath));
                _statusLabel.Text = $"Loaded {_scores.Count} score(s) from GameStats.txt.";
            }
            else
            {
                _scores = new List<GameStat>();
                _statusLabel.Text = "No saved scores yet. Win a game to create GameStats.txt.";
            }
        }
        catch (Exception ex)
        {
            _scores = new List<GameStat>();
            _statusLabel.Text = "High scores could not be loaded.";
            MessageBox.Show($"The high-score file could not be loaded.\n\n{ex.Message}", "High Scores", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private void SaveMenuItem_Click(object? sender, EventArgs e)
    {
        using var dialog = new SaveFileDialog
        {
            Title = "Save High Scores",
            Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*",
            FileName = Path.GetFileName(_currentFilePath),
            InitialDirectory = Directory.Exists(Path.GetDirectoryName(_currentFilePath)) ? Path.GetDirectoryName(_currentFilePath) : null
        };

        if (dialog.ShowDialog(this) != DialogResult.OK)
            return;

        _currentFilePath = dialog.FileName;
        SaveCurrentFile(silent: false, showSuccessMessage: true);
    }

    private void LoadMenuItem_Click(object? sender, EventArgs e)
    {
        using var dialog = new OpenFileDialog
        {
            Title = "Load High Scores",
            Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"
        };

        if (dialog.ShowDialog(this) != DialogResult.OK)
            return;

        try
        {
            _scores = _repository.Load(dialog.FileName);
            _currentFilePath = dialog.FileName;
            _scores = _logic.SortByScore(_scores);
            RefreshGrid();
            _statusLabel.Text = $"Loaded {_scores.Count} score(s) from {Path.GetFileName(_currentFilePath)}.";
        }
        catch (Exception ex)
        {
            MessageBox.Show($"The selected file could not be loaded.\n\n{ex.Message}", "Load High Scores", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void SaveCurrentFile(bool silent, bool showSuccessMessage = true)
    {
        try
        {
            _repository.Save(_currentFilePath, _scores);
            _statusLabel.Text = $"Saved {_scores.Count} score(s) to {Path.GetFileName(_currentFilePath)}.";
            if (!silent && showSuccessMessage)
                MessageBox.Show("High scores saved successfully.", "Save High Scores", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            if (!silent)
                MessageBox.Show($"The high-score file could not be saved.\n\n{ex.Message}", "Save High Scores", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void ApplySort(List<GameStat> sortedScores, string status)
    {
        _scores = sortedScores;
        RefreshGrid();
        _statusLabel.Text = status;
    }

    private void RefreshGrid()
    {
        _scoreGrid.DataSource = null;
        _scoreGrid.DataSource = _scores;
    }
}
