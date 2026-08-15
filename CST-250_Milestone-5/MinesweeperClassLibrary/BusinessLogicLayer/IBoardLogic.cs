using MinesweeperClassLibrary.Models;
namespace MinesweeperClassLibrary.BusinessLogicLayer;

public interface IBoardLogic
{
    void InitializeBoard(BoardModel board);
    MoveResult RevealCell(BoardModel board, int row, int column);
    bool ToggleFlag(BoardModel board, int row, int column);
    void FloodFill(BoardModel board, int row, int column);
    GameState DetermineGameState(BoardModel board);
    int CountRevealedSafeCells(BoardModel board);
    int CalculateScore(BoardModel board, TimeSpan elapsed);
}
