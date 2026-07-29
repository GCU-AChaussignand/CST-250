using MinesweeperClassLibrary.Models;

namespace MinesweeperClassLibrary.BusinessLogicLayer;

/// <summary>
/// Defines the board operations used by the console application.
/// </summary>
public interface IBoardLogic
{
    void SetupBombs(BoardModel board);

    void CountBombsNearby(BoardModel board);

    bool PlaceRandomReward(BoardModel board, RewardType rewardType);

    bool PlaceRewardCell(BoardModel board, int row, int column, RewardType rewardType);

    void RevealCell(BoardModel board, int row, int column);

    void FloodFill(BoardModel board, int row, int column);

    bool ToggleFlag(BoardModel board, int row, int column);

    bool UseSpecialBonus(BoardModel board, int row, int column);

    GameState DetermineGameState(BoardModel board);

    int DetermineFinalScore(BoardModel board);
}
