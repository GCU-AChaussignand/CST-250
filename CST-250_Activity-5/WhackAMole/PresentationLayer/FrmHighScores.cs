// Source: Author-created for CST-250 Activity 5 based on the Grand Canyon University Activity 5 guide (2025).
using System.Drawing;
using System.Windows.Forms;
using WhackAMole.Models;

namespace WhackAMole.PresentationLayer;

/// <summary>
/// Displays the high-score Hall of Fame.
/// </summary>
public class FrmHighScores : Form
{
    public FrmHighScores(IReadOnlyList<GameScoreModel> scores)
    {
        Text = "High Score Hall of Fame";
        StartPosition = FormStartPosition.CenterParent;
        ClientSize = new Size(620, 420);
        MinimumSize = new Size(560, 360);
        BackColor = Color.FromArgb(18, 25, 38);
        Font = new Font("Segoe UI", 10F);

        Label title = new()
        {
            Text = "HIGH SCORE HALL OF FAME",
            ForeColor = Color.White,
            Font = new Font("Segoe UI", 16F, FontStyle.Bold),
            AutoSize = true,
            Location = new Point(20, 18)
        };

        ListView list = new()
        {
            View = View.Details,
            FullRowSelect = true,
            GridLines = true,
            Location = new Point(20, 62),
            Size = new Size(580, 292),
            Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
        };

        list.Columns.Add("#", 45);
        list.Columns.Add("Player", 170);
        list.Columns.Add("Score", 80);
        list.Columns.Add("Level", 70);
        list.Columns.Add("Played", 190);

        int rank = 1;
        foreach (GameScoreModel score in scores)
        {
            ListViewItem item = new(rank.ToString());
            item.SubItems.Add(score.PlayerName);
            item.SubItems.Add(score.Score.ToString());
            item.SubItems.Add(score.Level.ToString());
            item.SubItems.Add(score.PlayedOn.ToString("g"));
            list.Items.Add(item);
            rank++;
        }

        if (scores.Count == 0)
        {
            ListViewItem empty = new("-");
            empty.SubItems.Add("No scores saved yet");
            empty.SubItems.Add("-");
            empty.SubItems.Add("-");
            empty.SubItems.Add("-");
            list.Items.Add(empty);
        }

        Button close = new()
        {
            Text = "Close",
            Size = new Size(90, 34),
            Location = new Point(510, 368),
            Anchor = AnchorStyles.Bottom | AnchorStyles.Right
        };
        close.Click += (_, _) => Close();

        Controls.Add(title);
        Controls.Add(list);
        Controls.Add(close);
    }
}
