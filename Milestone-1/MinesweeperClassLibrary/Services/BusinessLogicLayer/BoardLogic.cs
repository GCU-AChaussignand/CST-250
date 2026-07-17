using MinesweeperClassLibrary.Models;

namespace MinesweeperClassLibrary.Services.BusinessLogicLayer;

/// <summary>
/// Contains all business logic for board behavior and cell behavior.
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

        foreach (CellModel cell in board.Cells)
        {
            cell.IsBomb = false;
            cell.NumberOfBombNeighbors = 0;
        }

        int totalCells = board.Size * board.Size;
        int bombsToPlace = Math.Clamp(board.Difficulty, 1, totalCells);
        int bombsPlaced = 0;

        while (bombsPlaced < bombsToPlace)
        {
            int row = random.Next(board.Size);
            int column = random.Next(board.Size);

            if (!board.Cells[row, column].IsBomb)
            {
                board.Cells[row, column].IsBomb = true;
                bombsPlaced++;
            }
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

                        if (IsValidCell(board, neighborRow, neighborColumn) && board.Cells[neighborRow, neighborColumn].IsBomb)
                        {
                            bombCount++;
                        }
                    }
                }

                cell.NumberOfBombNeighbors = bombCount;
            }
        }
    }

    public void RevealCell(BoardModel board, int row, int column)
    {
        ValidateBoard(board);

        if (!IsValidCell(board, row, column))
        {
            return;
        }

        CellModel cell = board.Cells[row, column];

        if (!cell.IsFlagged)
        {
            cell.IsVisited = true;
        }
    }

    public GameState DetermineGameState(BoardModel board)
    {
        ValidateBoard(board);

        bool hasUnvisitedSafeCell = false;

        foreach (CellModel cell in board.Cells)
        {
            if (cell.IsBomb && cell.IsVisited)
            {
                board.GameState = GameState.Lost;
                board.EndTime = DateTime.Now;
                return board.GameState;
            }

            if (!cell.IsBomb && !cell.IsVisited)
            {
                hasUnvisitedSafeCell = true;
            }
        }

        board.GameState = hasUnvisitedSafeCell ? GameState.StillPlaying : GameState.Won;

        if (board.GameState == GameState.Won)
        {
            board.EndTime = DateTime.Now;
        }

        return board.GameState;
    }

    public bool UseSpecialBonus(BoardModel board, int row, int column)
    {
        ValidateBoard(board);

        if (!IsValidCell(board, row, column) || board.RewardsRemaining <= 0)
        {
            return false;
        }

        // Full reward behavior is saved for later milestones. This initial behavior
        // consumes a reward and tells the caller whether the selected cell is a bomb.
        board.RewardsRemaining--;
        return board.Cells[row, column].IsBomb;
    }

    public int DetermineFinalScore(BoardModel board)
    {
        ValidateBoard(board);

        TimeSpan elapsed = board.EndTime == DateTime.MinValue
            ? DateTime.Now - board.StartTime
            : board.EndTime - board.StartTime;

        int baseScore = board.Size * board.Size * Math.Max(board.Difficulty, 1);
        int timePenalty = Math.Max((int)elapsed.TotalSeconds, 0);
        int rewardPenalty = board.RewardsRemaining * 5;

        return Math.Max(baseScore - timePenalty + rewardPenalty, 0);
    }

    public void PlaceRewardCell(BoardModel board, int row, int column, RewardType rewardType)
    {
        ValidateBoard(board);

        if (!IsValidCell(board, row, column))
        {
            return;
        }

        CellModel existingCell = board.Cells[row, column];
        board.Cells[row, column] = new RewardCellModel(row, column, rewardType)
        {
            IsVisited = existingCell.IsVisited,
            IsBomb = existingCell.IsBomb,
            IsFlagged = existingCell.IsFlagged,
            NumberOfBombNeighbors = existingCell.NumberOfBombNeighbors
        };
    }

    private static bool IsValidCell(BoardModel board, int row, int column)
    {
        return row >= 0 && row < board.Size && column >= 0 && column < board.Size;
    }

    private static void ValidateBoard(BoardModel board)
    {
        ArgumentNullException.ThrowIfNull(board);
        ArgumentNullException.ThrowIfNull(board.Cells);
    }
}
