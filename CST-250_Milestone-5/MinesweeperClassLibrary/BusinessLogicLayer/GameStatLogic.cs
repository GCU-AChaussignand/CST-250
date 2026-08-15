using MinesweeperClassLibrary.Models;

namespace MinesweeperClassLibrary.BusinessLogicLayer;

/// <summary>
/// Contains high-score operations that are independent of Windows Forms.
/// </summary>
public sealed class GameStatLogic
{
    public int GetNextId(IEnumerable<GameStat> scores)
    {
        ArgumentNullException.ThrowIfNull(scores);
        List<GameStat> scoreList = scores.ToList();
        return scoreList.Count == 0 ? 1 : scoreList.Max(score => score.Id) + 1;
    }

    public List<GameStat> SortByName(IEnumerable<GameStat> scores)
    {
        ArgumentNullException.ThrowIfNull(scores);
        return scores
            .OrderBy(score => score.Name, StringComparer.OrdinalIgnoreCase)
            .ThenByDescending(score => score.Score)
            .ThenByDescending(score => score.GameTime)
            .ToList();
    }

    public List<GameStat> SortByScore(IEnumerable<GameStat> scores)
    {
        ArgumentNullException.ThrowIfNull(scores);
        return scores
            .OrderByDescending(score => score.Score)
            .ThenBy(score => score.Name, StringComparer.OrdinalIgnoreCase)
            .ThenByDescending(score => score.GameTime)
            .ToList();
    }

    public List<GameStat> SortByDate(IEnumerable<GameStat> scores)
    {
        ArgumentNullException.ThrowIfNull(scores);
        return scores
            .OrderByDescending(score => score.GameTime)
            .ThenByDescending(score => score.Score)
            .ToList();
    }
}
