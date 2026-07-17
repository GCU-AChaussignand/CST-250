using MinesweeperClassLibrary.Models;
using MinesweeperClassLibrary.Services.BusinessLogicLayer;
using Xunit;

namespace MinesweeperClassLibrary.Tests;

public class BoardLogicTests
{
    [Fact]
    public void BoardModelConstructor_ShouldInitializeCells_WhenValidSizeGiven()
    {
        BoardModel board = new BoardModel(4);

        Assert.Equal(4, board.Size);
        Assert.Equal(16, board.Cells.Length);
        Assert.Equal(0, board.Cells[0, 0].Row);
        Assert.Equal(0, board.Cells[0, 0].Column);
        Assert.Equal(3, board.Cells[3, 3].Row);
        Assert.Equal(3, board.Cells[3, 3].Column);
    }

    [Fact]
    public void SetupBombs_ShouldPlaceExpectedBombCount_WhenDifficultyIsBombCount()
    {
        BoardModel board = new BoardModel(5)
        {
            Difficulty = 6
        };
        IBoardLogic logic = new BoardLogic(seed: 100);

        logic.SetupBombs(board);

        int bombCount = board.Cells.Cast<CellModel>().Count(cell => cell.IsBomb);
        Assert.Equal(6, bombCount);
    }

    [Fact]
    public void CountBombsNearby_ShouldSetCenterNeighborCount_WhenBombsSurroundCenter()
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
    public void RevealCell_ShouldMarkCellVisited_WhenCellIsNotFlagged()
    {
        BoardModel board = new BoardModel(3);
        IBoardLogic logic = new BoardLogic(seed: 100);

        logic.RevealCell(board, 1, 1);

        Assert.True(board.Cells[1, 1].IsVisited);
    }

    [Fact]
    public void RevealCell_ShouldNotMarkCellVisited_WhenCellIsFlagged()
    {
        BoardModel board = new BoardModel(3);
        IBoardLogic logic = new BoardLogic(seed: 100);
        board.Cells[1, 1].IsFlagged = true;

        logic.RevealCell(board, 1, 1);

        Assert.False(board.Cells[1, 1].IsVisited);
    }

    [Fact]
    public void DetermineGameState_ShouldReturnLost_WhenBombCellIsVisited()
    {
        BoardModel board = new BoardModel(2);
        IBoardLogic logic = new BoardLogic(seed: 100);
        board.Cells[0, 0].IsBomb = true;
        board.Cells[0, 0].IsVisited = true;

        GameState result = logic.DetermineGameState(board);

        Assert.Equal(GameState.Lost, result);
    }

    [Fact]
    public void DetermineGameState_ShouldReturnWon_WhenAllSafeCellsVisited()
    {
        BoardModel board = new BoardModel(2);
        IBoardLogic logic = new BoardLogic(seed: 100);
        board.Cells[0, 0].IsBomb = true;
        board.Cells[0, 1].IsVisited = true;
        board.Cells[1, 0].IsVisited = true;
        board.Cells[1, 1].IsVisited = true;

        GameState result = logic.DetermineGameState(board);

        Assert.Equal(GameState.Won, result);
    }

    [Fact]
    public void DetermineGameState_ShouldReturnStillPlaying_WhenSafeCellsRemain()
    {
        BoardModel board = new BoardModel(2);
        IBoardLogic logic = new BoardLogic(seed: 100);
        board.Cells[0, 0].IsBomb = true;
        board.Cells[0, 1].IsVisited = true;

        GameState result = logic.DetermineGameState(board);

        Assert.Equal(GameState.StillPlaying, result);
    }

    [Fact]
    public void PlaceRewardCell_ShouldStoreRewardCellAsCellModel_WhenRewardIsPlaced()
    {
        BoardModel board = new BoardModel(3);
        BoardLogic logic = new BoardLogic(seed: 100);

        logic.PlaceRewardCell(board, 1, 1, RewardType.Hint);

        Assert.True(board.Cells[1, 1].HasSpecialReward);
        Assert.IsAssignableFrom<CellModel>(board.Cells[1, 1]);
        Assert.IsType<RewardCellModel>(board.Cells[1, 1]);
    }
}
