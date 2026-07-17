using MinesweeperClassLibrary.Models;

namespace MinesweeperClassLibrary.Services.BusinessLogicLayer;

/// <summary>
/// Provides an abstraction for Minesweeper board operations.
/// </summary>
public interface IBoardLogic
{
    void SetupBombs(BoardModel board);

    void CountBombsNearby(BoardModel board);

    void RevealCell(BoardModel board, int row, int column);

    GameState DetermineGameState(BoardModel board);

    bool UseSpecialBonus(BoardModel board, int row, int column);

    int DetermineFinalScore(BoardModel board);
}
