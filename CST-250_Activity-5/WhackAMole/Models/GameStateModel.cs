// Source: Author-created for CST-250 Activity 5 based on the Grand Canyon University Activity 5 guide (2025).
namespace WhackAMole.Models;

/// <summary>
/// Stores the in-memory state of the current game session.
/// </summary>
public class GameStateModel
{
    public string PlayerName { get; set; } = "Player";

    public int Score { get; set; }

    public int Level { get; set; } = 1;

    public int Lives { get; set; } = 3;

    public TimeSpan TimeElapsed { get; set; } = TimeSpan.Zero;

    public bool IsRunning { get; set; }

    public HashSet<string> Rewards { get; } = new(StringComparer.OrdinalIgnoreCase);
}
