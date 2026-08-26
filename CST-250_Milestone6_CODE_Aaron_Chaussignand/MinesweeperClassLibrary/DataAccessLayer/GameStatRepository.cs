using System.Globalization;
using MinesweeperClassLibrary.Models;

namespace MinesweeperClassLibrary.DataAccessLayer;

/// <summary>
/// Reads and writes high scores to a pipe-delimited text file.
/// </summary>
public sealed class GameStatRepository
{
    public void Save(string filePath, IEnumerable<GameStat> scores)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentException("A file path is required.", nameof(filePath));
        }

        ArgumentNullException.ThrowIfNull(scores);

        string? directory = Path.GetDirectoryName(filePath);
        if (!string.IsNullOrWhiteSpace(directory))
        {
            Directory.CreateDirectory(directory);
        }

        IEnumerable<string> lines = scores.Select(score => string.Join("|",
            score.Id.ToString(CultureInfo.InvariantCulture),
            SanitizeName(score.Name),
            Math.Max(0, score.Score).ToString(CultureInfo.InvariantCulture),
            score.GameTime.ToString("O", CultureInfo.InvariantCulture)));

        File.WriteAllLines(filePath, lines);
    }

    public List<GameStat> Load(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentException("A file path is required.", nameof(filePath));
        }

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("The selected high-score file was not found.", filePath);
        }

        var scores = new List<GameStat>();

        foreach (string line in File.ReadLines(filePath))
        {
            string[] fields = line.Split('|', StringSplitOptions.TrimEntries);
            if (fields.Length != 4 || string.IsNullOrWhiteSpace(fields[1]))
            {
                continue;
            }

            if (!int.TryParse(fields[0], NumberStyles.Integer, CultureInfo.InvariantCulture, out int id) || id < 0)
            {
                continue;
            }

            if (!int.TryParse(fields[2], NumberStyles.Integer, CultureInfo.InvariantCulture, out int score) || score < 0)
            {
                continue;
            }

            if (!DateTime.TryParse(fields[3], CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out DateTime gameTime))
            {
                continue;
            }

            scores.Add(new GameStat
            {
                Id = id,
                Name = fields[1],
                Score = score,
                GameTime = gameTime
            });
        }

        return scores;
    }

    private static string SanitizeName(string? name)
    {
        return (name ?? string.Empty).Trim().Replace('|', '/');
    }
}
