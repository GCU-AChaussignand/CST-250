using MinesweeperClassLibrary.Models;

namespace MinesweeperClassLibrary.BusinessLogicLayer;

/// <summary>
/// Converts a live BoardModel to and from a serializable SavedGameModel.
/// This keeps board reconstruction and validation out of the presentation layer.
/// </summary>
public sealed class GameSaveLogic
{
    public SavedGameModel CreateSnapshot(BoardModel board, TimeSpan elapsed, string? themeName)
    {
        ArgumentNullException.ThrowIfNull(board);

        var snapshot = new SavedGameModel
        {
            Size = board.Size,
            DifficultyPercent = board.DifficultyPercent,
            ElapsedSeconds = Math.Max(0, elapsed.TotalSeconds),
            ThemeName = string.IsNullOrWhiteSpace(themeName) ? "Classic" : themeName.Trim(),
            SavedAtUtc = DateTime.UtcNow
        };

        foreach (CellModel cell in board.Cells)
        {
            snapshot.Cells.Add(new SavedCellModel
            {
                Row = cell.Row,
                Column = cell.Column,
                IsBomb = cell.IsBomb,
                IsVisited = cell.IsVisited,
                IsFlagged = cell.IsFlagged,
                NumberOfBombNeighbors = cell.NumberOfBombNeighbors
            });
        }

        return snapshot;
    }

    public BoardModel RestoreBoard(SavedGameModel snapshot)
    {
        ValidateSnapshot(snapshot);
        var board = new BoardModel(snapshot.Size, snapshot.DifficultyPercent);

        foreach (SavedCellModel savedCell in snapshot.Cells)
        {
            CellModel cell = board.Cells[savedCell.Row, savedCell.Column];
            cell.IsBomb = savedCell.IsBomb;
            cell.IsVisited = savedCell.IsVisited;
            cell.IsFlagged = savedCell.IsFlagged;
            cell.NumberOfBombNeighbors = savedCell.NumberOfBombNeighbors;
        }

        return board;
    }

    public void ValidateSnapshot(SavedGameModel snapshot)
    {
        ArgumentNullException.ThrowIfNull(snapshot);

        if (snapshot.Size is < 5 or > 20)
        {
            throw new InvalidDataException("Saved board size is outside the supported range.");
        }

        if (snapshot.DifficultyPercent is < 5 or > 35)
        {
            throw new InvalidDataException("Saved difficulty is outside the supported range.");
        }

        int expectedCells = snapshot.Size * snapshot.Size;
        if (snapshot.Cells is null || snapshot.Cells.Count != expectedCells)
        {
            throw new InvalidDataException($"Saved game must contain exactly {expectedCells} cells.");
        }

        var coordinates = new HashSet<(int Row, int Column)>();
        foreach (SavedCellModel cell in snapshot.Cells)
        {
            if (cell.Row < 0 || cell.Row >= snapshot.Size || cell.Column < 0 || cell.Column >= snapshot.Size)
            {
                throw new InvalidDataException("A saved cell contains invalid coordinates.");
            }

            if (!coordinates.Add((cell.Row, cell.Column)))
            {
                throw new InvalidDataException("The saved game contains duplicate cell coordinates.");
            }

            if (cell.NumberOfBombNeighbors is < 0 or > 9)
            {
                throw new InvalidDataException("A saved cell has an invalid neighboring-bomb count.");
            }
        }
    }
}
