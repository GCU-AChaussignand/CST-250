namespace MinesweeperWinFormsApp;

/// <summary>
/// Captures the winner's name after a completed game.
/// </summary>
public sealed class Form3 : Form
{
    private readonly TextBox _nameTextBox = new()
    {
        Dock = DockStyle.Fill,
        Font = new Font("Segoe UI", 12F),
        MaxLength = 40
    };

    public Form3(int score, TimeSpan elapsed)
    {
        Text = "Minesweeper - Winner";
        StartPosition = FormStartPosition.CenterParent;
        ClientSize = new Size(470, 285);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Font = new Font("Segoe UI", 10F);
        BackColor = Color.FromArgb(243, 246, 250);

        BuildInterface(score, elapsed);
        AcceptButton = Controls.Find("btnOk", true).FirstOrDefault() as Button;
        Shown += (_, _) => _nameTextBox.Focus();
    }

    public string WinnerName { get; private set; } = string.Empty;

    private void BuildInterface(int score, TimeSpan elapsed)
    {
        var title = new Label
        {
            Text = "CONGRATULATIONS - YOU WIN!",
            AutoSize = true,
            Font = new Font("Segoe UI Semibold", 17F),
            ForeColor = Color.FromArgb(25, 47, 70)
        };

        var instruction = new Label
        {
            Text = "Enter your name to add this game to the high-score list.",
            AutoSize = true,
            ForeColor = Color.DimGray
        };

        var scoreLabel = new Label
        {
            Text = $"Score: {score:N0}    Time: {elapsed:mm\\:ss}",
            AutoSize = true,
            Font = new Font("Segoe UI Semibold", 12F),
            ForeColor = Color.FromArgb(40, 111, 168)
        };

        var okButton = new Button
        {
            Name = "btnOk",
            Text = "Save Score",
            Height = 42,
            Dock = DockStyle.Fill,
            BackColor = Color.FromArgb(40, 111, 168),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat
        };
        okButton.FlatAppearance.BorderSize = 0;
        okButton.Click += OkButton_Click;

        var cancelButton = new Button
        {
            Text = "Skip",
            Height = 42,
            Dock = DockStyle.Fill,
            DialogResult = DialogResult.Cancel,
            BackColor = Color.FromArgb(224, 228, 232),
            FlatStyle = FlatStyle.Flat
        };
        cancelButton.FlatAppearance.BorderSize = 0;

        var buttonPanel = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2 };
        buttonPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        buttonPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        buttonPanel.Controls.Add(okButton, 0, 0);
        buttonPanel.Controls.Add(cancelButton, 1, 0);

        var layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(28),
            ColumnCount = 1,
            RowCount = 7
        };
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 34));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 26));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 46));

        layout.Controls.Add(title, 0, 0);
        layout.Controls.Add(instruction, 0, 1);
        layout.Controls.Add(scoreLabel, 0, 2);
        layout.Controls.Add(new Label { Text = "Winner name", AutoSize = true, Font = new Font(Font, FontStyle.Bold) }, 0, 3);
        layout.Controls.Add(_nameTextBox, 0, 4);
        layout.Controls.Add(buttonPanel, 0, 6);
        Controls.Add(layout);
        CancelButton = cancelButton;
    }

    private void OkButton_Click(object? sender, EventArgs e)
    {
        string name = _nameTextBox.Text.Trim();
        if (string.IsNullOrWhiteSpace(name))
        {
            MessageBox.Show("Please enter your name before saving the score.", "Winner Name", MessageBoxButtons.OK, MessageBoxIcon.Information);
            _nameTextBox.Focus();
            return;
        }

        WinnerName = name;
        DialogResult = DialogResult.OK;
        Close();
    }
}
