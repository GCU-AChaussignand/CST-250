// Source: Author-created for CST-250 Activity 5 based on the Grand Canyon University Activity 5 guide (2025).
using System.Drawing;
using WhackAMole.Models;

namespace WhackAMole.BusinessLogicLayer;

/// <summary>
/// Contains game rules so scoring and difficulty logic stay outside the presentation layer.
/// </summary>
public class GameLogic
{
    public const int GameLengthSeconds = 60;

    /// <summary>
    /// Registers a successful target click and applies level and reward rules.
    /// </summary>
    /// <param name="state">Current game state.</param>
    /// <returns>A reward message when a new reward is unlocked; otherwise an empty string.</returns>
    public string RegisterTargetHit(GameStateModel state)
    {
        ArgumentNullException.ThrowIfNull(state);

        state.Score += 1;
        state.Level = Math.Max(1, (state.Score / 5) + 1);

        if (state.Score == 10 && state.Rewards.Add("Sharpshooter"))
        {
            state.Lives += 1;
            return "Reward unlocked: Sharpshooter (+1 life)";
        }

        if (state.Score == 20 && state.Rewards.Add("Mole Master"))
        {
            state.Lives += 1;
            return "Reward unlocked: Mole Master (+1 life)";
        }

        if (state.Score == 30 && state.Rewards.Add("Lightning Reflexes"))
        {
            return "Reward unlocked: Lightning Reflexes";
        }

        return string.Empty;
    }

    /// <summary>
    /// Applies the missed-click penalty.
    /// </summary>
    public void RegisterMiss(GameStateModel state)
    {
        ArgumentNullException.ThrowIfNull(state);
        state.Score = Math.Max(0, state.Score - 1);
        state.Lives = Math.Max(0, state.Lives - 1);
    }

    /// <summary>
    /// Applies a stronger penalty for clicking the decoy target.
    /// </summary>
    public void RegisterDecoyHit(GameStateModel state)
    {
        ArgumentNullException.ThrowIfNull(state);
        state.Score = Math.Max(0, state.Score - 2);
        state.Lives = Math.Max(0, state.Lives - 1);
    }

    /// <summary>
    /// Determines how frequently the target should relocate as levels increase.
    /// </summary>
    public int GetMoveIntervalSeconds(int level)
    {
        return level switch
        {
            <= 1 => 3,
            2 => 2,
            _ => 1
        };
    }

    /// <summary>
    /// Returns a smaller target size for higher levels.
    /// </summary>
    public Size GetTargetSize(int level)
    {
        int size = Math.Max(54, 92 - ((Math.Max(1, level) - 1) * 8));
        return new Size(size, size);
    }

    /// <summary>
    /// Determines whether the game should end because time or lives have run out.
    /// </summary>
    public bool ShouldEndGame(GameStateModel state)
    {
        ArgumentNullException.ThrowIfNull(state);
        return state.Lives <= 0 || state.TimeElapsed.TotalSeconds >= GameLengthSeconds;
    }

    /// <summary>
    /// Creates a fresh game while preserving the current player name.
    /// </summary>
    public GameStateModel CreateNewGame(string playerName)
    {
        return new GameStateModel
        {
            PlayerName = string.IsNullOrWhiteSpace(playerName) ? "Player" : playerName.Trim()
        };
    }
}
