using MinesweeperClassLibrary.Models;

namespace MinesweeperClassLibrary.BusinessLogicLayer;

/// <summary>
/// Contains all Minesweeper rules independently of the user interface.
/// </summary>
public sealed class BoardLogic : IBoardLogic
{
    private readonly Random _random;

    public BoardLogic(int? seed = null)
    {
        _random = seed.HasValue ? new Random(seed.Value) : new Random();
    }

    public void InitializeBoard(BoardModel board)
    {
        ArgumentNullException.ThrowIfNull(board);
        ResetCells(board);
        PlaceBombs(board);
        CountBombNeighbors(board);
    }

    public MoveResult RevealCell(BoardModel board, int row, int column)
    {
        ArgumentNullException.ThrowIfNull(board);
        ValidateCoordinates(board, row, column);
        CellModel cell = board.Cells[row, column];

        if (cell.IsFlagged)
        {
            return new MoveResult(
                GameState.StillPlaying,
                "Flagged cells must be unflagged before revealing.",
                CountRevealedSafeCells(board));
        }

        if (cell.IsVisited)
        {
            return new MoveResult(
                DetermineGameState(board),
                "That cell is already revealed.",
                CountRevealedSafeCells(board));
        }

        if (cell.IsBomb)
        {
            cell.IsVisited = true;
            RevealAllBombs(board);
            return new MoveResult(
                GameState.Lost,
                "You hit a bomb. Game over.",
                CountRevealedSafeCells(board));
        }

        if (cell.NumberOfBombNeighbors == 0)
        {
            FloodFill(board, row, column);
        }
        else
        {
            cell.IsVisited = true;
        }

        GameState state = DetermineGameState(board);
        string message = state == GameState.Won
            ? "Congratulations! You cleared every safe cell."
            : "Safe move.";

        return new MoveResult(state, message, CountRevealedSafeCells(board));
    }

    public bool ToggleFlag(BoardModel board, int row, int column)
    {
        ArgumentNullException.ThrowIfNull(board);
        ValidateCoordinates(board, row, column);
        CellModel cell = board.Cells[row, column];

        if (cell.IsVisited)
        {
            return false;
        }

        cell.IsFlagged = !cell.IsFlagged;
        return cell.IsFlagged;
    }

    public void FloodFill(BoardModel board, int row, int column)
    {
        ArgumentNullException.ThrowIfNull(board);

        if (!IsInside(board, row, column))
        {
            return;
        }

        CellModel cell = board.Cells[row, column];
        if (cell.IsVisited || cell.IsFlagged || cell.IsBomb)
        {
            return;
        }

        cell.IsVisited = true;
        if (cell.NumberOfBombNeighbors > 0)
        {
            return;
        }

        for (int rowOffset = -1; rowOffset <= 1; rowOffset++)
        {
            for (int columnOffset = -1; columnOffset <= 1; columnOffset++)
            {
                if (rowOffset != 0 || columnOffset != 0)
                {
                    FloodFill(board, row + rowOffset, column + columnOffset);
                }
            }
        }
    }

    public GameState DetermineGameState(BoardModel board)
    {
        ArgumentNullException.ThrowIfNull(board);

        foreach (CellModel cell in board.Cells)
        {
            if (cell.IsBomb && cell.IsVisited)
            {
                return GameState.Lost;
            }
        }

        return CountRevealedSafeCells(board) == board.SafeCellCount
            ? GameState.Won
            : GameState.StillPlaying;
    }

    public int CountRevealedSafeCells(BoardModel board)
    {
        ArgumentNullException.ThrowIfNull(board);
        int count = 0;

        foreach (CellModel cell in board.Cells)
        {
            if (cell.IsVisited && !cell.IsBomb)
            {
                count++;
            }
        }

        return count;
    }

    public int CalculateScore(BoardModel board, TimeSpan elapsed)
    {
        ArgumentNullException.ThrowIfNull(board);
        double seconds = Math.Max(1, elapsed.TotalSeconds);
        double difficultyFactor = 1 + board.DifficultyPercent / 10.0;
        return Math.Max(0, (int)Math.Round(board.Size * board.Size * difficultyFactor * 100 / seconds));
    }

    private static void ResetCells(BoardModel board)
    {
        foreach (CellModel cell in board.Cells)
        {
            cell.IsBomb = false;
            cell.IsVisited = false;
            cell.IsFlagged = false;
            cell.NumberOfBombNeighbors = 0;
        }
    }

    private void PlaceBombs(BoardModel board)
    {
        int placed = 0;
        while (placed < board.BombCount)
        {
            int row = _random.Next(board.Size);
            int column = _random.Next(board.Size);

            if (board.Cells[row, column].IsBomb)
            {
                continue;
            }

            board.Cells[row, column].IsBomb = true;
            placed++;
        }
    }

    private static void CountBombNeighbors(BoardModel board)
    {
        for (int row = 0; row < board.Size; row++)
        {
            for (int column = 0; column < board.Size; column++)
            {
                CellModel cell = board.Cells[row, column];
                if (cell.IsBomb)
                {
                    cell.NumberOfBombNeighbors = 9;
                    continue;
                }

                int count = 0;
                for (int rowOffset = -1; rowOffset <= 1; rowOffset++)
                {
                    for (int columnOffset = -1; columnOffset <= 1; columnOffset++)
                    {
                        if (rowOffset == 0 && columnOffset == 0)
                        {
                            continue;
                        }

                        int neighborRow = row + rowOffset;
                        int neighborColumn = column + columnOffset;
                        if (IsInside(board, neighborRow, neighborColumn) && board.Cells[neighborRow, neighborColumn].IsBomb)
                        {
                            count++;
                        }
                    }
                }

                cell.NumberOfBombNeighbors = count;
            }
        }
    }

    private static void RevealAllBombs(BoardModel board)
    {
        foreach (CellModel cell in board.Cells)
        {
            if (cell.IsBomb)
            {
                cell.IsVisited = true;
            }
        }
    }

    private static bool IsInside(BoardModel board, int row, int column)
    {
        return row >= 0 && row < board.Size && column >= 0 && column < board.Size;
    }

    private static void ValidateCoordinates(BoardModel board, int row, int column)
    {
        if (row < 0 || row >= board.Size)
        {
            throw new ArgumentOutOfRangeException(nameof(row), "Row must be inside the board.");
        }

        if (column < 0 || column >= board.Size)
        {
            throw new ArgumentOutOfRangeException(nameof(column), "Column must be inside the board.");
        }
    }
}
