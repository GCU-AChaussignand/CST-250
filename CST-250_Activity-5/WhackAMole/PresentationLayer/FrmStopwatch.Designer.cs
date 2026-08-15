// Source: Author-created for CST-250 Activity 5 based on the Grand Canyon University Activity 5 guide (2025).
using System.Drawing;
using System.Windows.Forms;
namespace WhackAMole.PresentationLayer;

partial class FrmStopwatch
{
    private System.ComponentModel.IContainer? components = null;
    private Panel pnlHeader = null!;
    private Panel pnlControls = null!;
    private Label lblTimeElapsed = null!;
    private Label lblScore = null!;
    private Label lblLevel = null!;
    private Label lblLives = null!;
    private Label lblPlayer = null!;
    private Label lblStatus = null!;
    private Label lblReward = null!;
    private Button btnStart = null!;
    private Button btnStop = null!;
    private Button btnReset = null!;
    private Button btnTarget = null!;
    private Button btnDecoy = null!;
    private Button btnHighScores = null!;
    private Button btnPlayerName = null!;
    private System.Windows.Forms.Timer tmrStopwatch = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components is not null)
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        pnlHeader = new Panel();
        pnlControls = new Panel();
        lblTimeElapsed = new Label();
        lblScore = new Label();
        lblLevel = new Label();
        lblLives = new Label();
        lblPlayer = new Label();
        lblStatus = new Label();
        lblReward = new Label();
        btnStart = new Button();
        btnStop = new Button();
        btnReset = new Button();
        btnTarget = new Button();
        btnDecoy = new Button();
        btnHighScores = new Button();
        btnPlayerName = new Button();
        tmrStopwatch = new System.Windows.Forms.Timer(components);
        pnlHeader.SuspendLayout();
        pnlControls.SuspendLayout();
        SuspendLayout();

        pnlHeader.BackColor = Color.FromArgb(24, 34, 52);
        pnlHeader.Dock = DockStyle.Top;
        pnlHeader.Height = 92;
        pnlHeader.Padding = new Padding(14, 10, 14, 8);

        lblPlayer.AutoSize = true;
        lblPlayer.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblPlayer.ForeColor = Color.White;
        lblPlayer.Location = new Point(16, 12);
        lblPlayer.Text = "Player: Player";

        lblTimeElapsed.AutoSize = true;
        lblTimeElapsed.Font = new Font("Segoe UI", 19F, FontStyle.Bold);
        lblTimeElapsed.ForeColor = Color.White;
        lblTimeElapsed.Location = new Point(16, 40);
        lblTimeElapsed.Text = "00:00:00";

        lblScore.AutoSize = true;
        lblScore.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblScore.ForeColor = Color.White;
        lblScore.Location = new Point(250, 20);
        lblScore.Text = "Score: 0";

        lblLevel.AutoSize = true;
        lblLevel.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblLevel.ForeColor = Color.White;
        lblLevel.Location = new Point(250, 50);
        lblLevel.Text = "Level: 1";

        lblLives.AutoSize = true;
        lblLives.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblLives.ForeColor = Color.White;
        lblLives.Location = new Point(360, 20);
        lblLives.Text = "Lives: 3";

        lblReward.AutoSize = true;
        lblReward.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblReward.ForeColor = Color.Gold;
        lblReward.Location = new Point(360, 50);
        lblReward.Text = "Rewards: none";

        pnlHeader.Controls.Add(lblPlayer);
        pnlHeader.Controls.Add(lblTimeElapsed);
        pnlHeader.Controls.Add(lblScore);
        pnlHeader.Controls.Add(lblLevel);
        pnlHeader.Controls.Add(lblLives);
        pnlHeader.Controls.Add(lblReward);

        pnlControls.BackColor = Color.FromArgb(24, 34, 52);
        pnlControls.Dock = DockStyle.Bottom;
        pnlControls.Height = 92;

        btnStart.Location = new Point(16, 14);
        btnStart.Size = new Size(92, 34);
        btnStart.Text = "Start";
        btnStart.UseVisualStyleBackColor = true;
        btnStart.Click += BtnStartClickEH;

        btnStop.Location = new Point(114, 14);
        btnStop.Size = new Size(92, 34);
        btnStop.Text = "Stop";
        btnStop.UseVisualStyleBackColor = true;
        btnStop.Click += BtnStopClickEH;

        btnReset.Location = new Point(212, 14);
        btnReset.Size = new Size(92, 34);
        btnReset.Text = "Reset";
        btnReset.UseVisualStyleBackColor = true;
        btnReset.Click += BtnResetClickEH;

        btnPlayerName.Location = new Point(320, 14);
        btnPlayerName.Size = new Size(115, 34);
        btnPlayerName.Text = "Player Name";
        btnPlayerName.UseVisualStyleBackColor = true;
        btnPlayerName.Click += BtnPlayerNameClickEH;

        btnHighScores.Location = new Point(441, 14);
        btnHighScores.Size = new Size(115, 34);
        btnHighScores.Text = "High Scores";
        btnHighScores.UseVisualStyleBackColor = true;
        btnHighScores.Click += BtnHighScoresClickEH;

        lblStatus.AutoSize = false;
        lblStatus.ForeColor = Color.Gainsboro;
        lblStatus.Location = new Point(16, 57);
        lblStatus.Size = new Size(760, 24);
        lblStatus.Text = "Press Start to begin. Click the target; avoid the red decoy.";

        pnlControls.Controls.Add(btnStart);
        pnlControls.Controls.Add(btnStop);
        pnlControls.Controls.Add(btnReset);
        pnlControls.Controls.Add(btnPlayerName);
        pnlControls.Controls.Add(btnHighScores);
        pnlControls.Controls.Add(lblStatus);

        btnTarget.BackColor = Color.MediumSeaGreen;
        btnTarget.FlatAppearance.BorderSize = 2;
        btnTarget.FlatStyle = FlatStyle.Flat;
        btnTarget.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnTarget.ForeColor = Color.White;
        btnTarget.Location = new Point(280, 180);
        btnTarget.Size = new Size(92, 92);
        btnTarget.Text = "TARGET";
        btnTarget.UseVisualStyleBackColor = false;
        btnTarget.Visible = false;
        btnTarget.Click += BtnTargetClickEH;

        btnDecoy.BackColor = Color.Firebrick;
        btnDecoy.FlatAppearance.BorderSize = 2;
        btnDecoy.FlatStyle = FlatStyle.Flat;
        btnDecoy.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnDecoy.ForeColor = Color.White;
        btnDecoy.Location = new Point(480, 250);
        btnDecoy.Size = new Size(78, 78);
        btnDecoy.Text = "DECOY";
        btnDecoy.UseVisualStyleBackColor = false;
        btnDecoy.Visible = false;
        btnDecoy.Click += BtnDecoyClickEH;

        tmrStopwatch.Enabled = true;
        tmrStopwatch.Interval = 1000;
        tmrStopwatch.Tick += TmrStopwatchTickEH;

        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(12, 18, 29);
        ClientSize = new Size(800, 560);
        Controls.Add(btnTarget);
        Controls.Add(btnDecoy);
        Controls.Add(pnlHeader);
        Controls.Add(pnlControls);
        Font = new Font("Segoe UI", 9F);
        MinimumSize = new Size(680, 500);
        Name = "FrmStopwatch";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Whack-A-Mole Stopwatch Challenge";
        Click += FrmStopwatchClickEH;
        Resize += FrmStopwatchResizeEH;
        pnlHeader.ResumeLayout(false);
        pnlHeader.PerformLayout();
        pnlControls.ResumeLayout(false);
        ResumeLayout(false);
    }
}
