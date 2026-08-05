using MinesweeperClassLibrary.BusinessLogicLayer;
using MinesweeperClassLibrary.Models;
namespace MinesweeperClassLibrary.Tests;

public class BoardLogicTests
{
    [Fact]
    public void InitializeBoard_PlacesExpectedBombCount()
    {
        var board = new BoardModel(10, 15); IBoardLogic logic = new BoardLogic(100); logic.InitializeBoard(board);
        Assert.Equal(board.BombCount, board.Cells.Cast<CellModel>().Count(c => c.IsBomb));
    }

    [Fact]
    public void ToggleFlag_ChangesFlagStateOnUnvisitedCell()
    {
        var board = new BoardModel(5, 10); IBoardLogic logic = new BoardLogic(1); logic.InitializeBoard(board);
        Assert.True(logic.ToggleFlag(board, 0, 0)); Assert.True(board.Cells[0, 0].IsFlagged);
        Assert.False(logic.ToggleFlag(board, 0, 0)); Assert.False(board.Cells[0, 0].IsFlagged);
    }

    [Fact]
    public void FloodFill_RevealsConnectedSafeRegion()
    {
        var board = new BoardModel(5, 5); IBoardLogic logic = new BoardLogic(2); logic.InitializeBoard(board);
        CellModel zero = board.Cells.Cast<CellModel>().First(c => !c.IsBomb && c.NumberOfBombNeighbors == 0);
        logic.FloodFill(board, zero.Row, zero.Column);
        Assert.True(zero.IsVisited); Assert.True(logic.CountRevealedSafeCells(board) > 1);
    }

    [Fact]
    public void RevealCell_OnBomb_ReturnsLost()
    {
        var board = new BoardModel(5, 10); IBoardLogic logic = new BoardLogic(3); logic.InitializeBoard(board);
        CellModel bomb = board.Cells.Cast<CellModel>().First(c => c.IsBomb);
        Assert.Equal(GameState.Lost, logic.RevealCell(board, bomb.Row, bomb.Column).State);
    }

    [Fact]
    public void CalculateScore_DecreasesAsElapsedTimeIncreases()
    {
        var board = new BoardModel(10, 15); IBoardLogic logic = new BoardLogic(4);
        Assert.True(logic.CalculateScore(board, TimeSpan.FromSeconds(30)) > logic.CalculateScore(board, TimeSpan.FromSeconds(90)));
    }
    [Fact]
    public void RevealAllSafeCells_ReturnsWon()
    {
        var board = new BoardModel(5, 10);
        IBoardLogic logic = new BoardLogic(5);
        logic.InitializeBoard(board);

        MoveResult lastResult = new(GameState.StillPlaying, string.Empty, 0);
        foreach (CellModel cell in board.Cells)
        {
            if (!cell.IsBomb && !cell.IsVisited)
            {
                lastResult = logic.RevealCell(board, cell.Row, cell.Column);
            }
        }

        Assert.Equal(GameState.Won, lastResult.State);
        Assert.Equal(board.SafeCellCount, logic.CountRevealedSafeCells(board));
    }

    [Fact]
    public void RevealCell_WithOutOfRangeCoordinates_Throws()
    {
        var board = new BoardModel(5, 10);
        IBoardLogic logic = new BoardLogic(6);
        logic.InitializeBoard(board);

        Assert.Throws<ArgumentOutOfRangeException>(() => logic.RevealCell(board, -1, 0));
        Assert.Throws<ArgumentOutOfRangeException>(() => logic.RevealCell(board, 0, board.Size));
    }

}
