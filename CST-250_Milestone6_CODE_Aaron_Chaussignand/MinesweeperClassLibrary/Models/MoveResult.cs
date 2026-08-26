namespace MinesweeperClassLibrary.Models;

/// <summary>
/// Describes the result of one reveal operation.
/// </summary>
public sealed record MoveResult(GameState State, string Message, int RevealedSafeCells);
