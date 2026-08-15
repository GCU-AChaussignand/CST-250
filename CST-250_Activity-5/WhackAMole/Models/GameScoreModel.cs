// Source: Author-created for CST-250 Activity 5 based on the Grand Canyon University Activity 5 guide (2025).
namespace WhackAMole.Models;

/// <summary>
/// Represents one completed Whack-A-Mole game for high-score persistence.
/// </summary>
public class GameScoreModel
{
    public string PlayerName { get; set; } = "Player";

    public int Score { get; set; }

    public int Level { get; set; }

    public DateTime PlayedOn { get; set; }
}
