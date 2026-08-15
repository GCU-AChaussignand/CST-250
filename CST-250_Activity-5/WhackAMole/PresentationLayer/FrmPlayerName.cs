// Source: Author-created for CST-250 Activity 5 based on the Grand Canyon University Activity 5 guide (2025).
using System.Drawing;
using System.Windows.Forms;
namespace WhackAMole.PresentationLayer;

/// <summary>
/// Collects the player's name for high-score tracking.
/// </summary>
public class FrmPlayerName : Form
{
    private readonly TextBox txtPlayerName = new();
    private readonly Button btnOk = new();
    private readonly Button btnCancel = new();

    public string PlayerName => txtPlayerName.Text.Trim();

    public FrmPlayerName(string currentPlayerName)
    {
        Text = "Player Name";
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        ClientSize = new Size(360, 155);
        Font = new Font("Segoe UI", 10F);

        Label prompt = new()
        {
            AutoSize = true,
            Location = new Point(20, 20),
            Text = "Enter the player name used for high scores:"
        };

        txtPlayerName.Location = new Point(20, 50);
        txtPlayerName.Size = new Size(320, 27);
        txtPlayerName.MaxLength = 30;
        txtPlayerName.Text = currentPlayerName;
        txtPlayerName.SelectAll();

        btnOk.Text = "OK";
        btnOk.Location = new Point(174, 99);
        btnOk.Size = new Size(80, 32);
        btnOk.Click += BtnOkClickEH;

        btnCancel.Text = "Cancel";
        btnCancel.Location = new Point(260, 99);
        btnCancel.Size = new Size(80, 32);
        btnCancel.DialogResult = DialogResult.Cancel;

        AcceptButton = btnOk;
        CancelButton = btnCancel;

        Controls.Add(prompt);
        Controls.Add(txtPlayerName);
        Controls.Add(btnOk);
        Controls.Add(btnCancel);
    }

    /// <summary>
    /// Validates the player name before closing the dialog.
    /// </summary>
    private void BtnOkClickEH(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtPlayerName.Text))
        {
            MessageBox.Show(this, "Please enter a player name.", "Player Name", MessageBoxButtons.OK, MessageBoxIcon.Information);
            txtPlayerName.Focus();
            return;
        }

        DialogResult = DialogResult.OK;
        Close();
    }
}
