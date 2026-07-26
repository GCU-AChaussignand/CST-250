using MinesweeperClassLibrary.BusinessLogicLayer;
using MinesweeperClassLibrary.Models;
using Xunit;

namespace MinesweeperClassLibrary.Tests;

/// <summary>
/// Verifies the business rules used by the interactive Milestone 2 game.
/// </summary>
public class BoardLogicTests
{
    [Fact]
    public void BoardModelConstructor_ShouldInitializeEveryCell()
    {
        BoardModel board = new BoardModel(4);

        Assert.Equal(4, board.Size);
        Assert.Equal(16, board.Cells.Length);
        Assert.Equal(0, board.Cells[0, 0].Row);
        Assert.Equal(0, board.Cells[0, 0].Column);
        Assert.Equal(3, board.Cells[3, 3].Row);
        Assert.Equal(3, board.Cells[3, 3].Column);
        Assert.Equal(GameState.StillPlaying, board.GameState);
        Assert.Equal(0, board.RewardsRemaining);
    }

    [Fact]
    public void SetupBombs_ShouldPlaceTheRequestedNumberOfBombs()
    {
        BoardModel board = new BoardModel(5)
        {
            Difficulty = 6
        };
        IBoardLogic logic = new BoardLogic(seed: 100);

        logic.SetupBombs(board);

        int bombCount = board.Cells
            .Cast<CellModel>()
            .Count(cell => cell.IsBomb);

        Assert.Equal(6, bombCount);
    }

    [Fact]
    public void SetupBombs_ShouldResetVisitedFlaggedAndRewardState()
    {
        BoardModel board = new BoardModel(3)
        {
            Difficulty = 2,
            RewardsRemaining = 2,
            GameState = GameState.Won
        };
        IBoardLogic logic = new BoardLogic(seed: 100);
        board.Cells[0, 0].IsVisited = true;
        board.Cells[0, 1].IsFlagged = true;
        logic.PlaceRewardCell(board, 1, 1, RewardType.BombDetector);

        logic.SetupBombs(board);

        Assert.Equal(GameState.StillPlaying, board.GameState);
        Assert.Equal(0, board.RewardsRemaining);
        Assert.Equal(DateTime.MinValue, board.EndTime);
        Assert.All(board.Cells.Cast<CellModel>(), cell =>
        {
            Assert.False(cell.IsVisited);
            Assert.False(cell.IsFlagged);
            Assert.False(cell.HasSpecialReward);
        });
    }

    [Fact]
    public void SetupBombs_ShouldClampBombCountSoOneSafeCellRemains()
    {
        BoardModel board = new BoardModel(2)
        {
            Difficulty = 99
        };
        IBoardLogic logic = new BoardLogic(seed: 100);

        logic.SetupBombs(board);

        int bombCount = board.Cells
            .Cast<CellModel>()
            .Count(cell => cell.IsBomb);

        Assert.Equal(3, bombCount);
    }

    [Fact]
    public void CountBombsNearby_ShouldCountAllAdjacentBombs()
    {
        BoardModel board = new BoardModel(3);
        IBoardLogic logic = new BoardLogic(seed: 100);
        board.Cells[0, 0].IsBomb = true;
        board.Cells[0, 1].IsBomb = true;
        board.Cells[2, 2].IsBomb = true;

        logic.CountBombsNearby(board);

        Assert.Equal(3, board.Cells[1, 1].NumberOfBombNeighbors);
        Assert.Equal(9, board.Cells[0, 0].NumberOfBombNeighbors);
        Assert.Equal(1, board.Cells[2, 1].NumberOfBombNeighbors);
    }

    [Fact]
    public void PlaceRandomReward_ShouldPlaceOneRewardOnASafeCell()
    {
        BoardModel board = new BoardModel(4)
        {
            Difficulty = 3
        };
        IBoardLogic logic = new BoardLogic(seed: 250);
        logic.SetupBombs(board);

        bool rewardPlaced = logic.PlaceRandomReward(board, RewardType.BombDetector);

        List<CellModel> rewardCells = board.Cells
            .Cast<CellModel>()
            .Where(cell => cell.HasSpecialReward)
            .ToList();

        Assert.True(rewardPlaced);
        Assert.Single(rewardCells);
        Assert.False(rewardCells[0].IsBomb);
        Assert.IsType<RewardCellModel>(rewardCells[0]);
    }

    [Fact]
    public void PlaceRewardCell_ShouldRejectABombCell()
    {
        BoardModel board = new BoardModel(3);
        IBoardLogic logic = new BoardLogic(seed: 100);
        board.Cells[1, 1].IsBomb = true;

        bool result = logic.PlaceRewardCell(
            board,
            1,
            1,
            RewardType.BombDetector);

        Assert.False(result);
        Assert.False(board.Cells[1, 1].HasSpecialReward);
        Assert.IsNotType<RewardCellModel>(board.Cells[1, 1]);
    }

    [Fact]
    public void RevealCell_ShouldCollectTheRewardOnlyOnce()
    {
        BoardModel board = new BoardModel(3);
        IBoardLogic logic = new BoardLogic(seed: 100);
        logic.PlaceRewardCell(board, 1, 1, RewardType.BombDetector);

        logic.RevealCell(board, 1, 1);
        logic.RevealCell(board, 1, 1);

        Assert.True(board.Cells[1, 1].IsVisited);
        Assert.False(board.Cells[1, 1].HasSpecialReward);
        Assert.Equal(1, board.RewardsRemaining);
    }

    [Fact]
    public void RevealCell_ShouldNotVisitAFlaggedCell()
    {
        BoardModel board = new BoardModel(3);
        IBoardLogic logic = new BoardLogic(seed: 100);
        board.Cells[1, 1].IsFlagged = true;

        logic.RevealCell(board, 1, 1);

        Assert.False(board.Cells[1, 1].IsVisited);
    }

    [Fact]
    public void ToggleFlag_ShouldFlagAndThenUnflagAnUnvisitedCell()
    {
        BoardModel board = new BoardModel(3);
        IBoardLogic logic = new BoardLogic(seed: 100);

        bool firstResult = logic.ToggleFlag(board, 1, 1);
        bool secondResult = logic.ToggleFlag(board, 1, 1);

        Assert.True(firstResult);
        Assert.False(secondResult);
        Assert.False(board.Cells[1, 1].IsFlagged);
    }

    [Fact]
    public void ToggleFlag_ShouldNotFlagAVisitedCell()
    {
        BoardModel board = new BoardModel(3);
        IBoardLogic logic = new BoardLogic(seed: 100);
        board.Cells[1, 1].IsVisited = true;

        bool result = logic.ToggleFlag(board, 1, 1);

        Assert.False(result);
        Assert.False(board.Cells[1, 1].IsFlagged);
    }

    [Fact]
    public void UseSpecialBonus_ShouldDetectABombAndConsumeOneReward()
    {
        BoardModel board = new BoardModel(2)
        {
            RewardsRemaining = 1
        };
        IBoardLogic logic = new BoardLogic(seed: 100);
        board.Cells[0, 0].IsBomb = true;

        bool isBomb = logic.UseSpecialBonus(board, 0, 0);

        Assert.True(isBomb);
        Assert.Equal(0, board.RewardsRemaining);
        Assert.False(board.Cells[0, 0].IsVisited);
    }

    [Fact]
    public void UseSpecialBonus_ShouldReportSafeCellAndConsumeOneReward()
    {
        BoardModel board = new BoardModel(2)
        {
            RewardsRemaining = 1
        };
        IBoardLogic logic = new BoardLogic(seed: 100);

        bool isBomb = logic.UseSpecialBonus(board, 0, 0);

        Assert.False(isBomb);
        Assert.Equal(0, board.RewardsRemaining);
        Assert.False(board.Cells[0, 0].IsVisited);
    }

    [Fact]
    public void UseSpecialBonus_ShouldNotChangeBoardWithoutAReward()
    {
        BoardModel board = new BoardModel(2);
        IBoardLogic logic = new BoardLogic(seed: 100);
        board.Cells[0, 0].IsBomb = true;

        bool result = logic.UseSpecialBonus(board, 0, 0);

        Assert.False(result);
        Assert.Equal(0, board.RewardsRemaining);
        Assert.False(board.Cells[0, 0].IsVisited);
    }

    [Fact]
    public void DetermineGameState_ShouldReturnLostWhenAVisitedCellIsABomb()
    {
        BoardModel board = new BoardModel(2);
        IBoardLogic logic = new BoardLogic(seed: 100);
        board.Cells[0, 0].IsBomb = true;
        board.Cells[0, 0].IsVisited = true;

        GameState result = logic.DetermineGameState(board);

        Assert.Equal(GameState.Lost, result);
        Assert.NotEqual(DateTime.MinValue, board.EndTime);
    }

    [Fact]
    public void DetermineGameState_ShouldReturnWonWhenEverySafeCellIsVisited()
    {
        BoardModel board = new BoardModel(2);
        IBoardLogic logic = new BoardLogic(seed: 100);
        board.Cells[0, 0].IsBomb = true;
        board.Cells[0, 1].IsVisited = true;
        board.Cells[1, 0].IsVisited = true;
        board.Cells[1, 1].IsVisited = true;

        GameState result = logic.DetermineGameState(board);

        Assert.Equal(GameState.Won, result);
        Assert.NotEqual(DateTime.MinValue, board.EndTime);
    }

    [Fact]
    public void DetermineGameState_ShouldReturnStillPlayingWhenSafeCellsRemain()
    {
        BoardModel board = new BoardModel(2);
        IBoardLogic logic = new BoardLogic(seed: 100);
        board.Cells[0, 0].IsBomb = true;
        board.Cells[0, 1].IsVisited = true;

        GameState result = logic.DetermineGameState(board);

        Assert.Equal(GameState.StillPlaying, result);
        Assert.Equal(DateTime.MinValue, board.EndTime);
    }

    [Fact]
    public void DetermineGameState_ShouldNotTreatAnIncorrectFlagAsAVisitedSafeCell()
    {
        BoardModel board = new BoardModel(2);
        IBoardLogic logic = new BoardLogic(seed: 100);
        board.Cells[0, 0].IsBomb = true;
        board.Cells[0, 1].IsVisited = true;
        board.Cells[1, 0].IsVisited = true;
        board.Cells[1, 1].IsFlagged = true;

        GameState result = logic.DetermineGameState(board);

        Assert.Equal(GameState.StillPlaying, result);
        Assert.Equal(DateTime.MinValue, board.EndTime);
    }
}
