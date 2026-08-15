// Citation: Grand Canyon University. (2025). Activity 5: Creating a timer-driven game with dynamic UI controls [Course activity guide].
using System.Drawing;
using System.Windows.Forms;
using WhackAMole.BusinessLogicLayer;
using WhackAMole.DataAccessLayer;
using WhackAMole.Models;

namespace WhackAMole.PresentationLayer;

/// <summary>
/// Main presentation layer for the stopwatch and Whack-A-Mole game.
/// </summary>
public partial class FrmStopwatch : Form
{
    private readonly Random random = new();
    private readonly GameLogic gameLogic = new();
    private readonly ScoreLogic scoreLogic = new(new ScoreDao());
    private readonly Color[] targetColors =
    {
        Color.MediumSeaGreen,
        Color.RoyalBlue,
        Color.DarkOrange,
        Color.MediumPurple,
        Color.DeepPink
    };

    private TimeSpan timeElapsed = new();
    private GameStateModel gameState = new();
    private bool scoreSaved;

    public FrmStopwatch()
    {
        InitializeComponent();
        UpdateDisplay();
    }

    /// <summary>
    /// Starts or resumes the stopwatch and game.
    /// </summary>
    private void BtnStartClickEH(object? sender, EventArgs e)
    {
        if (gameLogic.ShouldEndGame(gameState))
        {
            ResetGame();
        }

        gameState.IsRunning = true;
        tmrStopwatch.Start();
        lblStatus.Text = "Game running. Click TARGET and avoid the red DECOY.";
        RelocateTargets();
    }

    /// <summary>
    /// Stops the stopwatch and temporarily pauses the game.
    /// </summary>
    private void BtnStopClickEH(object? sender, EventArgs e)
    {
        gameState.IsRunning = false;
        tmrStopwatch.Stop();
        btnTarget.Visible = false;
        btnDecoy.Visible = false;
        lblStatus.Text = "Paused. Press Start to continue.";
    }

    /// <summary>
    /// Resets the timer and all current game statistics.
    /// </summary>
    private void BtnResetClickEH(object? sender, EventArgs e)
    {
        ResetGame();
        lblStatus.Text = "Game reset. Press Start to begin.";
    }

    /// <summary>
    /// Updates elapsed time and periodically relocates the target controls.
    /// </summary>
    private void TmrStopwatchTickEH(object? sender, EventArgs e)
    {
        if (!gameState.IsRunning)
        {
            return;
        }

        timeElapsed = timeElapsed.Add(TimeSpan.FromMilliseconds(tmrStopwatch.Interval));
        gameState.TimeElapsed = timeElapsed;
        UpdateDisplay();

        if (gameLogic.ShouldEndGame(gameState))
        {
            string reason = gameState.Lives <= 0 ? "No lives remaining." : "The 60-second round is complete.";
            EndGame(reason);
            return;
        }

        int moveEverySeconds = gameLogic.GetMoveIntervalSeconds(gameState.Level);
        if ((int)timeElapsed.TotalSeconds % moveEverySeconds == 0)
        {
            RelocateTargets();
        }
    }

    /// <summary>
    /// Awards points when the moving target is clicked.
    /// </summary>
    private void BtnTargetClickEH(object? sender, EventArgs e)
    {
        if (!gameState.IsRunning || !btnTarget.Visible)
        {
            return;
        }

        int previousLevel = gameState.Level;
        string rewardMessage = gameLogic.RegisterTargetHit(gameState);
        btnTarget.Visible = false;

        if (gameState.Level > previousLevel)
        {
            lblStatus.Text = $"Level {gameState.Level}! The target is smaller and moves faster.";
        }
        else
        {
            lblStatus.Text = "Hit! +1 point.";
        }

        if (!string.IsNullOrEmpty(rewardMessage))
        {
            lblStatus.Text = rewardMessage;
        }

        UpdateDisplay();
    }

    /// <summary>
    /// Applies a penalty when the player clicks the red decoy.
    /// </summary>
    private void BtnDecoyClickEH(object? sender, EventArgs e)
    {
        if (!gameState.IsRunning || !btnDecoy.Visible)
        {
            return;
        }

        gameLogic.RegisterDecoyHit(gameState);
        btnDecoy.Visible = false;
        lblStatus.Text = "Decoy hit: -2 points and -1 life.";
        UpdateDisplay();

        if (gameLogic.ShouldEndGame(gameState))
        {
            EndGame("No lives remaining.");
        }
    }

    /// <summary>
    /// Penalizes a missed click on the open game area.
    /// </summary>
    private void FrmStopwatchClickEH(object? sender, EventArgs e)
    {
        if (!gameState.IsRunning)
        {
            return;
        }

        gameLogic.RegisterMiss(gameState);
        lblStatus.Text = "Missed click: -1 point and -1 life.";
        UpdateDisplay();

        if (gameLogic.ShouldEndGame(gameState))
        {
            EndGame("No lives remaining.");
        }
    }

    /// <summary>
    /// Keeps dynamic controls inside the playable region when the form is resized.
    /// </summary>
    private void FrmStopwatchResizeEH(object? sender, EventArgs e)
    {
        KeepControlInsidePlayArea(btnTarget);
        KeepControlInsidePlayArea(btnDecoy);
        lblStatus.Width = Math.Max(240, pnlControls.ClientSize.Width - 32);
    }

    /// <summary>
    /// Opens a second form to update the high-score player name.
    /// </summary>
    private void BtnPlayerNameClickEH(object? sender, EventArgs e)
    {
        using FrmPlayerName nameForm = new(gameState.PlayerName);
        if (nameForm.ShowDialog(this) == DialogResult.OK)
        {
            gameState.PlayerName = nameForm.PlayerName;
            UpdateDisplay();
        }
    }

    /// <summary>
    /// Opens the Hall of Fame form with scores loaded through the business layer.
    /// </summary>
    private void BtnHighScoresClickEH(object? sender, EventArgs e)
    {
        try
        {
            using FrmHighScores highScoresForm = new(scoreLogic.GetTopScores());
            highScoresForm.ShowDialog(this);
        }
        catch (IOException ex)
        {
            MessageBox.Show(this, $"High scores could not be read.\n\n{ex.Message}", "High Scores", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    /// <summary>
    /// Generates random positions and custom target colors within the playable area.
    /// </summary>
    private void RelocateTargets()
    {
        Rectangle playArea = GetPlayArea();
        Size targetSize = gameLogic.GetTargetSize(gameState.Level);
        btnTarget.Size = targetSize;
        btnTarget.BackColor = targetColors[random.Next(targetColors.Length)];
        btnTarget.Location = GetRandomLocation(playArea, btnTarget.Size);
        btnTarget.Visible = true;
        btnTarget.BringToFront();

        if (gameState.Level >= 2)
        {
            int decoySize = Math.Max(50, targetSize.Width - 10);
            btnDecoy.Size = new Size(decoySize, decoySize);
            Point decoyLocation = GetRandomLocation(playArea, btnDecoy.Size);

            int attempts = 0;
            while (new Rectangle(decoyLocation, btnDecoy.Size).IntersectsWith(btnTarget.Bounds) && attempts < 20)
            {
                decoyLocation = GetRandomLocation(playArea, btnDecoy.Size);
                attempts++;
            }

            btnDecoy.Location = decoyLocation;
            btnDecoy.Visible = true;
            btnDecoy.BringToFront();
        }
        else
        {
            btnDecoy.Visible = false;
        }
    }

    /// <summary>
    /// Updates all labels from the current game model.
    /// </summary>
    private void UpdateDisplay()
    {
        lblTimeElapsed.Text = timeElapsed.ToString(@"hh\:mm\:ss");
        lblScore.Text = $"Score: {gameState.Score}";
        lblLevel.Text = $"Level: {gameState.Level}";
        lblLives.Text = $"Lives: {gameState.Lives}";
        lblPlayer.Text = $"Player: {gameState.PlayerName}";
        lblReward.Text = gameState.Rewards.Count == 0
            ? "Rewards: none"
            : $"Rewards: {string.Join(", ", gameState.Rewards)}";
    }

    /// <summary>
    /// Ends the current round, persists the result once, and reports the final score.
    /// </summary>
    private void EndGame(string reason)
    {
        gameState.IsRunning = false;
        tmrStopwatch.Stop();
        btnTarget.Visible = false;
        btnDecoy.Visible = false;

        if (!scoreSaved)
        {
            try
            {
                scoreLogic.SaveScore(gameState);
                scoreSaved = true;
            }
            catch (IOException ex)
            {
                MessageBox.Show(this, $"The score could not be saved.\n\n{ex.Message}", "Save Score", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        lblStatus.Text = $"Game over. Final score: {gameState.Score}.";
        MessageBox.Show(
            this,
            $"{reason}\n\nPlayer: {gameState.PlayerName}\nScore: {gameState.Score}\nLevel: {gameState.Level}",
            "Game Over",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);
    }

    /// <summary>
    /// Restores the stopwatch and game to their initial state while preserving the player name.
    /// </summary>
    private void ResetGame()
    {
        tmrStopwatch.Stop();
        string playerName = gameState.PlayerName;
        gameState = gameLogic.CreateNewGame(playerName);
        timeElapsed = new TimeSpan();
        scoreSaved = false;
        btnTarget.Visible = false;
        btnDecoy.Visible = false;
        UpdateDisplay();
    }

    /// <summary>
    /// Calculates the rectangle available for random target movement.
    /// </summary>
    private Rectangle GetPlayArea()
    {
        const int margin = 12;
        int top = pnlHeader.Bottom + margin;
        int bottom = pnlControls.Top - margin;
        int height = Math.Max(80, bottom - top);
        int width = Math.Max(120, ClientSize.Width - (margin * 2));
        return new Rectangle(margin, top, width, height);
    }

    /// <summary>
    /// Returns a random location that keeps the control inside the provided rectangle.
    /// </summary>
    private Point GetRandomLocation(Rectangle area, Size controlSize)
    {
        int maxX = Math.Max(area.Left, area.Right - controlSize.Width);
        int maxY = Math.Max(area.Top, area.Bottom - controlSize.Height);
        int x = random.Next(area.Left, maxX + 1);
        int y = random.Next(area.Top, maxY + 1);
        return new Point(x, y);
    }

    /// <summary>
    /// Repositions a control only when a resize would leave it outside the play area.
    /// </summary>
    private void KeepControlInsidePlayArea(Control control)
    {
        Rectangle area = GetPlayArea();
        int x = Math.Clamp(control.Left, area.Left, Math.Max(area.Left, area.Right - control.Width));
        int y = Math.Clamp(control.Top, area.Top, Math.Max(area.Top, area.Bottom - control.Height));
        control.Location = new Point(x, y);
    }
}
