// Source: Author-created for CST-250 Activity 5 based on the Grand Canyon University Activity 5 guide (2025).
using WhackAMole.DataAccessLayer;
using WhackAMole.Models;

namespace WhackAMole.BusinessLogicLayer;

/// <summary>
/// Coordinates high-score operations between the UI and data-access layer.
/// </summary>
public class ScoreLogic
{
    private readonly ScoreDao scoreDao;

    public ScoreLogic(ScoreDao scoreDao)
    {
        this.scoreDao = scoreDao ?? throw new ArgumentNullException(nameof(scoreDao));
    }

    /// <summary>
    /// Saves the current completed game.
    /// </summary>
    public void SaveScore(GameStateModel state)
    {
        ArgumentNullException.ThrowIfNull(state);

        scoreDao.AddScore(new GameScoreModel
        {
            PlayerName = state.PlayerName,
            Score = state.Score,
            Level = state.Level,
            PlayedOn = DateTime.Now
        });
    }

    /// <summary>
    /// Returns the highest scoring games, with newer games breaking score ties.
    /// </summary>
    public IReadOnlyList<GameScoreModel> GetTopScores(int count = 10)
    {
        return scoreDao.GetScores()
            .OrderByDescending(score => score.Score)
            .ThenByDescending(score => score.Level)
            .ThenByDescending(score => score.PlayedOn)
            .Take(Math.Max(1, count))
            .ToList();
    }
}
