using MinesweeperClassLibrary.BusinessLogicLayer;
using MinesweeperClassLibrary.Models;
using Xunit;

namespace MinesweeperClassLibrary.Tests;

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
        board.Cells[0, 0].IsFlagged = true;
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
    public void FloodFill_ShouldRevealConnectedZerosAndNumberedBoundary()
    {
        BoardModel board = new BoardModel(5);
        IBoardLogic logic = new BoardLogic(seed: 100);
        board.Cells[0, 4].IsBomb = true;
        logic.CountBombsNearby(board);

        logic.FloodFill(board, 4, 0);

        Assert.True(board.Cells[4, 0].IsVisited);
        Assert.True(board.Cells[1, 3].IsVisited); // numbered boundary
        Assert.False(board.Cells[0, 4].IsVisited); // bomb remains hidden
    }

    [Fact]
    public void FloodFill_ShouldStopAtFlaggedCell()
    {
        BoardModel board = new BoardModel(4);
        IBoardLogic logic = new BoardLogic(seed: 100);
        board.Cells[2, 2].IsFlagged = true;

        logic.FloodFill(board, 0, 0);

        Assert.False(board.Cells[2, 2].IsVisited);
        Assert.True(board.Cells[0, 0].IsVisited);
    }

    [Fact]
    public void FloodFill_ShouldIgnoreOutOfBoundsCoordinates()
    {
        BoardModel board = new BoardModel(3);
        IBoardLogic logic = new BoardLogic(seed: 100);

        Exception? exception = Record.Exception(() => logic.FloodFill(board, -1, 3));

        Assert.Null(exception);
        Assert.All(board.Cells.Cast<CellModel>(), cell => Assert.False(cell.IsVisited));
    }

    [Fact]
    public void RevealCell_ShouldUseFloodFillWhenSelectedCellHasNoBombNeighbors()
    {
        BoardModel board = new BoardModel(4);
        IBoardLogic logic = new BoardLogic(seed: 100);
        board.Cells[0, 3].IsBomb = true;
        logic.CountBombsNearby(board);

        logic.RevealCell(board, 3, 0);

        int visitedCount = board.Cells.Cast<CellModel>().Count(cell => cell.IsVisited);
        Assert.True(visitedCount > 1);
        Assert.False(board.Cells[0, 3].IsVisited);
    }

    [Fact]
    public void RevealCell_ShouldRevealOnlyOneNumberedCell()
    {
        BoardModel board = new BoardModel(3);
        IBoardLogic logic = new BoardLogic(seed: 100);
        board.Cells[0, 0].IsBomb = true;
        logic.CountBombsNearby(board);

        logic.RevealCell(board, 0, 1);

        Assert.True(board.Cells[0, 1].IsVisited);
        Assert.Equal(1, board.Cells.Cast<CellModel>().Count(cell => cell.IsVisited));
    }

}
