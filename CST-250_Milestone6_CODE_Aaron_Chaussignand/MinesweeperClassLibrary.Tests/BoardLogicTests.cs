using MinesweeperClassLibrary.BusinessLogicLayer;
using MinesweeperClassLibrary.Models;

namespace MinesweeperClassLibrary.Tests;

public class BoardLogicTests
{
    [Fact]
    public void InitializeBoard_PlacesExpectedBombCount()
    {
        var board = new BoardModel(10, 15);
        IBoardLogic logic = new BoardLogic(100);
        logic.InitializeBoard(board);

        Assert.Equal(board.BombCount, board.Cells.Cast<CellModel>().Count(cell => cell.IsBomb));
    }

    [Fact]
    public void InitializeBoard_CalculatesCorrectNeighborCounts()
    {
        var board = new BoardModel(8, 20);
        IBoardLogic logic = new BoardLogic(101);
        logic.InitializeBoard(board);

        foreach (CellModel cell in board.Cells)
        {
            if (cell.IsBomb)
            {
                Assert.Equal(9, cell.NumberOfBombNeighbors);
                continue;
            }

            int expected = 0;
            for (int rowOffset = -1; rowOffset <= 1; rowOffset++)
            {
                for (int columnOffset = -1; columnOffset <= 1; columnOffset++)
                {
                    if (rowOffset == 0 && columnOffset == 0)
                    {
                        continue;
                    }

                    int row = cell.Row + rowOffset;
                    int column = cell.Column + columnOffset;
                    if (row >= 0 && row < board.Size && column >= 0 && column < board.Size && board.Cells[row, column].IsBomb)
                    {
                        expected++;
                    }
                }
            }

            Assert.Equal(expected, cell.NumberOfBombNeighbors);
        }
    }

    [Fact]
    public void ToggleFlag_ChangesFlagStateOnUnvisitedCell()
    {
        var board = new BoardModel(5, 10);
        IBoardLogic logic = new BoardLogic(1);
        logic.InitializeBoard(board);

        Assert.True(logic.ToggleFlag(board, 0, 0));
        Assert.True(board.Cells[0, 0].IsFlagged);
        Assert.False(logic.ToggleFlag(board, 0, 0));
        Assert.False(board.Cells[0, 0].IsFlagged);
    }

    [Fact]
    public void RevealCell_FlaggedSafeCell_RemainsHidden()
    {
        var board = new BoardModel(5, 10);
        IBoardLogic logic = new BoardLogic(102);
        logic.InitializeBoard(board);
        CellModel safeCell = board.Cells.Cast<CellModel>().First(cell => !cell.IsBomb);
        logic.ToggleFlag(board, safeCell.Row, safeCell.Column);

        MoveResult result = logic.RevealCell(board, safeCell.Row, safeCell.Column);

        Assert.Equal(GameState.StillPlaying, result.State);
        Assert.False(safeCell.IsVisited);
    }

    [Fact]
    public void FloodFill_RevealsConnectedSafeRegion()
    {
        var board = new BoardModel(5, 5);
        IBoardLogic logic = new BoardLogic(2);
        logic.InitializeBoard(board);
        CellModel zero = board.Cells.Cast<CellModel>().First(cell => !cell.IsBomb && cell.NumberOfBombNeighbors == 0);

        logic.FloodFill(board, zero.Row, zero.Column);

        Assert.True(zero.IsVisited);
        Assert.True(logic.CountRevealedSafeCells(board) > 1);
    }

    [Fact]
    public void RevealCell_OnBomb_ReturnsLostAndRevealsBombs()
    {
        var board = new BoardModel(5, 10);
        IBoardLogic logic = new BoardLogic(3);
        logic.InitializeBoard(board);
        CellModel bomb = board.Cells.Cast<CellModel>().First(cell => cell.IsBomb);

        MoveResult result = logic.RevealCell(board, bomb.Row, bomb.Column);

        Assert.Equal(GameState.Lost, result.State);
        Assert.All(board.Cells.Cast<CellModel>().Where(cell => cell.IsBomb), cell => Assert.True(cell.IsVisited));
    }

    [Fact]
    public void CalculateScore_DecreasesAsElapsedTimeIncreases()
    {
        var board = new BoardModel(10, 15);
        IBoardLogic logic = new BoardLogic(4);

        Assert.True(logic.CalculateScore(board, TimeSpan.FromSeconds(30)) > logic.CalculateScore(board, TimeSpan.FromSeconds(90)));
    }

    [Fact]
    public void CalculateScore_IncreasesWithDifficulty()
    {
        IBoardLogic logic = new BoardLogic(4);
        var easy = new BoardModel(10, 10);
        var hard = new BoardModel(10, 25);

        Assert.True(logic.CalculateScore(hard, TimeSpan.FromSeconds(60)) > logic.CalculateScore(easy, TimeSpan.FromSeconds(60)));
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
    public void RevealCell_WithOutOfRangeCoordinates_ThrowsCorrectParameter()
    {
        var board = new BoardModel(5, 10);
        IBoardLogic logic = new BoardLogic(6);
        logic.InitializeBoard(board);

        ArgumentOutOfRangeException rowException = Assert.Throws<ArgumentOutOfRangeException>(() => logic.RevealCell(board, -1, 0));
        ArgumentOutOfRangeException columnException = Assert.Throws<ArgumentOutOfRangeException>(() => logic.RevealCell(board, 0, board.Size));

        Assert.Equal("row", rowException.ParamName);
        Assert.Equal("column", columnException.ParamName);
    }
}
