using MinesweeperClassLibrary.Models;

namespace MinesweeperClassLibrary.BusinessLogicLayer;

/// <summary>
/// Contains all game behavior for the Minesweeper board and its cells.
/// </summary>
public class BoardLogic : IBoardLogic
{
    private readonly Random random;

    public BoardLogic() : this(null)
    {
    }

    public BoardLogic(int? seed)
    {
        random = seed.HasValue ? new Random(seed.Value) : new Random();
    }

    public void SetupBombs(BoardModel board)
    {
        ValidateBoard(board);

        board.GameState = GameState.StillPlaying;
        board.RewardsRemaining = 0;
        board.StartTime = DateTime.Now;
        board.EndTime = DateTime.MinValue;

        for (int row = 0; row < board.Size; row++)
        {
            for (int column = 0; column < board.Size; column++)
            {
                board.Cells[row, column] = new CellModel(row, column);
            }
        }

        int totalCells = board.Size * board.Size;
        int maximumBombs = Math.Max(totalCells - 1, 0);
        int bombsToPlace = Math.Clamp(board.Difficulty, 0, maximumBombs);
        int bombsPlaced = 0;

        while (bombsPlaced < bombsToPlace)
        {
            int row = random.Next(board.Size);
            int column = random.Next(board.Size);

            if (board.Cells[row, column].IsBomb)
            {
                continue;
            }

            board.Cells[row, column].IsBomb = true;
            bombsPlaced++;
        }
    }

    public void CountBombsNearby(BoardModel board)
    {
        ValidateBoard(board);

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

                int bombCount = 0;

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

                        if (IsValidCell(board, neighborRow, neighborColumn) &&
                            board.Cells[neighborRow, neighborColumn].IsBomb)
                        {
                            bombCount++;
                        }
                    }
                }

                cell.NumberOfBombNeighbors = bombCount;
            }
        }
    }

    public bool PlaceRandomReward(BoardModel board, RewardType rewardType)
    {
        ValidateBoard(board);

        List<CellModel> eligibleCells = board.Cells
            .Cast<CellModel>()
            .Where(cell => !cell.IsBomb && !cell.HasSpecialReward)
            .ToList();

        if (eligibleCells.Count == 0 || rewardType == RewardType.None)
        {
            return false;
        }

        CellModel selectedCell = eligibleCells[random.Next(eligibleCells.Count)];
        return PlaceRewardCell(board, selectedCell.Row, selectedCell.Column, rewardType);
    }

    public bool PlaceRewardCell(BoardModel board, int row, int column, RewardType rewardType)
    {
        ValidateBoard(board);

        if (!IsValidCell(board, row, column) ||
            rewardType == RewardType.None ||
            board.Cells[row, column].IsBomb)
        {
            return false;
        }

        CellModel existingCell = board.Cells[row, column];
        board.Cells[row, column] = new RewardCellModel(row, column, rewardType)
        {
            IsVisited = existingCell.IsVisited,
            IsBomb = existingCell.IsBomb,
            IsFlagged = existingCell.IsFlagged,
            NumberOfBombNeighbors = existingCell.NumberOfBombNeighbors
        };

        return true;
    }

    public void RevealCell(BoardModel board, int row, int column)
    {
        ValidateBoard(board);

        if (!IsValidCell(board, row, column))
        {
            return;
        }

        CellModel cell = board.Cells[row, column];

        if (cell.IsFlagged || cell.IsVisited)
        {
            return;
        }

        cell.IsVisited = true;

        if (cell.HasSpecialReward)
        {
            board.RewardsRemaining++;
            cell.HasSpecialReward = false;

            if (cell is RewardCellModel rewardCell)
            {
                rewardCell.RewardType = RewardType.None;
            }
        }
    }

    public bool ToggleFlag(BoardModel board, int row, int column)
    {
        ValidateBoard(board);

        if (!IsValidCell(board, row, column) || board.Cells[row, column].IsVisited)
        {
            return false;
        }

        CellModel cell = board.Cells[row, column];
        cell.IsFlagged = !cell.IsFlagged;
        return cell.IsFlagged;
    }

    public bool UseSpecialBonus(BoardModel board, int row, int column)
    {
        ValidateBoard(board);

        if (!IsValidCell(board, row, column) || board.RewardsRemaining <= 0)
        {
            return false;
        }

        board.RewardsRemaining--;
        return board.Cells[row, column].IsBomb;
    }

    public GameState DetermineGameState(BoardModel board)
    {
        ValidateBoard(board);

        foreach (CellModel cell in board.Cells)
        {
            if (cell.IsBomb && cell.IsVisited)
            {
                board.GameState = GameState.Lost;
                board.EndTime = DateTime.Now;
                return board.GameState;
            }
        }

        bool allSafeCellsVisited = board.Cells
            .Cast<CellModel>()
            .Where(cell => !cell.IsBomb)
            .All(cell => cell.IsVisited);

        board.GameState = allSafeCellsVisited
            ? GameState.Won
            : GameState.StillPlaying;

        if (board.GameState == GameState.Won)
        {
            board.EndTime = DateTime.Now;
        }

        return board.GameState;
    }

    public int DetermineFinalScore(BoardModel board)
    {
        ValidateBoard(board);

        DateTime effectiveEndTime = board.EndTime == DateTime.MinValue
            ? DateTime.Now
            : board.EndTime;

        int elapsedSeconds = Math.Max(
            (int)(effectiveEndTime - board.StartTime).TotalSeconds,
            0);

        int boardScore = board.Size * board.Size * 10;
        int difficultyBonus = Math.Max(board.Difficulty, 0) * 25;
        int timePenalty = elapsedSeconds;

        return Math.Max(boardScore + difficultyBonus - timePenalty, 0);
    }

    private static bool IsValidCell(BoardModel board, int row, int column)
    {
        return row >= 0 && row < board.Size &&
               column >= 0 && column < board.Size;
    }

    private static void ValidateBoard(BoardModel board)
    {
        ArgumentNullException.ThrowIfNull(board);
        ArgumentNullException.ThrowIfNull(board.Cells);
    }
}
