// Source: Author-created for CST-250 Activity 5 based on the Grand Canyon University Activity 5 guide (2025).
using System.Globalization;
using System.Text;
using WhackAMole.Models;

namespace WhackAMole.DataAccessLayer;

/// <summary>
/// Reads and writes Whack-A-Mole high scores to a text file.
/// </summary>
public class ScoreDao
{
    private readonly string scoreFilePath;

    /// <summary>
    /// Creates the DAO and prepares a writable application-data folder.
    /// </summary>
    public ScoreDao()
    {
        string dataDirectory = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "WhackAMole");

        Directory.CreateDirectory(dataDirectory);
        scoreFilePath = Path.Combine(dataDirectory, "highscores.txt");
    }

    /// <summary>
    /// Appends one score to the persistent score file.
    /// </summary>
    public void AddScore(GameScoreModel score)
    {
        ArgumentNullException.ThrowIfNull(score);

        string safeName = score.PlayerName
            .Replace("|", string.Empty, StringComparison.Ordinal)
            .Replace(Environment.NewLine, " ", StringComparison.Ordinal)
            .Trim();

        string line = string.Join(
            "|",
            safeName,
            score.Score.ToString(CultureInfo.InvariantCulture),
            score.Level.ToString(CultureInfo.InvariantCulture),
            score.PlayedOn.ToString("O", CultureInfo.InvariantCulture));

        File.AppendAllText(scoreFilePath, line + Environment.NewLine, Encoding.UTF8);
    }

    /// <summary>
    /// Loads every valid score record from the text file.
    /// </summary>
    public List<GameScoreModel> GetScores()
    {
        List<GameScoreModel> scores = new();

        if (!File.Exists(scoreFilePath))
        {
            return scores;
        }

        foreach (string line in File.ReadLines(scoreFilePath, Encoding.UTF8))
        {
            string[] parts = line.Split('|');
            if (parts.Length != 4)
            {
                continue;
            }

            bool scoreValid = int.TryParse(parts[1], NumberStyles.Integer, CultureInfo.InvariantCulture, out int score);
            bool levelValid = int.TryParse(parts[2], NumberStyles.Integer, CultureInfo.InvariantCulture, out int level);
            bool dateValid = DateTime.TryParse(parts[3], CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out DateTime playedOn);

            if (!scoreValid || !levelValid || !dateValid)
            {
                continue;
            }

            scores.Add(new GameScoreModel
            {
                PlayerName = string.IsNullOrWhiteSpace(parts[0]) ? "Player" : parts[0],
                Score = score,
                Level = level,
                PlayedOn = playedOn
            });
        }

        return scores;
    }
}
