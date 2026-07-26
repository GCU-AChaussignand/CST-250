/*
 * Aaron Chaussignand
 * CST-250: Programming in C# II
 * 07/20/2026
 * Chess Board Project - Activity 2
 * CellModel represents one square in the chessboard's two-dimensional array.
 */

namespace ChessBoardClassLibrary.Models
{
    /// <summary>
    /// Represents one square on the chessboard.
    /// </summary>
    public class CellModel
    {
        /// <summary>
        /// Gets the zero-based row for this cell.
        /// </summary>
        public int Row { get; private set; }

        /// <summary>
        /// Gets the zero-based column for this cell.
        /// </summary>
        public int Column { get; private set; }

        /// <summary>
        /// Gets or sets the chess notation for the piece occupying this cell.
        /// </summary>
        public string PieceOccupyingCell { get; set; }

        /// <summary>
        /// Gets or sets whether this cell is a legal next move.
        /// </summary>
        public bool IsLegalNextMove { get; set; }

        /// <summary>
        /// Initializes a cell at the supplied row and column.
        /// </summary>
        /// <param name="row">Zero-based row.</param>
        /// <param name="column">Zero-based column.</param>
        public CellModel(int row, int column)
        {
            Row = row;
            Column = column;
            PieceOccupyingCell = string.Empty;
            IsLegalNextMove = false;
        }
    }
}
