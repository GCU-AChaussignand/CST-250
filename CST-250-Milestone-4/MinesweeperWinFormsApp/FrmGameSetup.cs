namespace MinesweeperWinFormsApp;

public sealed class FrmGameSetup : Form
{
    private readonly TrackBar _sizeTrackBar = new() { Minimum = 5, Maximum = 20, Value = 10, TickFrequency = 1, Dock = DockStyle.Fill };
    private readonly TrackBar _difficultyTrackBar = new() { Minimum = 5, Maximum = 35, Value = 15, TickFrequency = 5, Dock = DockStyle.Fill };
    private readonly Label _sizeValueLabel = new() { AutoSize = true, Text = "10 x 10" };
    private readonly Label _difficultyValueLabel = new() { AutoSize = true, Text = "15% bombs" };

    public FrmGameSetup()
    {
        Text = "Minesweeper - New Game";
        StartPosition = FormStartPosition.CenterScreen;
        MinimumSize = new Size(500, 430);
        BackColor = Color.FromArgb(243, 246, 250);
        Font = new Font("Segoe UI", 10F);
        BuildInterface();
        _sizeTrackBar.ValueChanged += (_, _) => _sizeValueLabel.Text = $"{_sizeTrackBar.Value} x {_sizeTrackBar.Value}";
        _difficultyTrackBar.ValueChanged += (_, _) => _difficultyValueLabel.Text = $"{_difficultyTrackBar.Value}% bombs";
    }

    private void BuildInterface()
    {
        var title = new Label { Text = "MINESWEEPER", Font = new Font("Segoe UI Semibold", 25F), AutoSize = true, ForeColor = Color.FromArgb(25, 47, 70), Anchor = AnchorStyles.None };
        var subtitle = new Label { Text = "Choose a board size and difficulty to begin.", AutoSize = true, ForeColor = Color.DimGray, Anchor = AnchorStyles.None };
        var playButton = new Button { Text = "Play Game", Height = 46, Dock = DockStyle.Fill, BackColor = Color.FromArgb(40, 111, 168), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
        playButton.FlatAppearance.BorderSize = 0;
        playButton.Click += PlayButton_Click;

        var layout = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(42), ColumnCount = 1, RowCount = 10 };
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60)); layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30)); layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 55));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 35)); layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 55)); layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 35));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100)); layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 52));
        layout.Controls.Add(title, 0, 0); layout.Controls.Add(subtitle, 0, 1);
        layout.Controls.Add(new Label { Text = "Board size", AutoSize = true, Font = new Font(Font, FontStyle.Bold) }, 0, 2);
        layout.Controls.Add(_sizeTrackBar, 0, 3); layout.Controls.Add(_sizeValueLabel, 0, 4);
        layout.Controls.Add(new Label { Text = "Difficulty", AutoSize = true, Font = new Font(Font, FontStyle.Bold) }, 0, 5);
        layout.Controls.Add(_difficultyTrackBar, 0, 6); layout.Controls.Add(_difficultyValueLabel, 0, 7); layout.Controls.Add(playButton, 0, 9);
        Controls.Add(layout);
    }

    private void PlayButton_Click(object? sender, EventArgs e)
    {
        Hide();
        using var gameForm = new FrmMinesweeper(_sizeTrackBar.Value, _difficultyTrackBar.Value);
        gameForm.ShowDialog(this);
        Show();
    }
}
