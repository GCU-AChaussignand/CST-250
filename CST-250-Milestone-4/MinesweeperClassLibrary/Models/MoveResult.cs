namespace MinesweeperClassLibrary.Models;
public sealed record MoveResult(GameState State, string Message, int RevealedSafeCells);
