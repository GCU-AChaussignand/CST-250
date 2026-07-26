/*
 * Aaron Chaussignand
 * CST-250: Programming in C# II
 * 07/20/2026
 * Chess Board Project - Activity 2
 * BoardLogic contains reusable chess movement rules for both presentation layers.
 */

using ChessBoardClassLibrary.Models;

namespace ChessBoardClassLibrary.Services.BusinessLogicLayer
{
    /// <summary>
    /// Applies chess movement rules to a BoardModel.
    /// </summary>
    public class BoardLogic
    {
        /// <summary>
        /// Marks all legal moves for the selected piece and cell.
        /// </summary>
        /// <param name="board">Board to update.</param>
        /// <param name="currentCell">Cell occupied by the selected piece.</param>
        /// <param name="chessPiece">King, Queen, Bishop, Knight, or Rook.</param>
        /// <returns>The updated board.</returns>
        public BoardModel MarkLegalMoves(
            BoardModel board,
            CellModel currentCell,
            string chessPiece)
        {
            ArgumentNullException.ThrowIfNull(board);
            ArgumentNullException.ThrowIfNull(currentCell);

            if (!IsOnBoard(board, currentCell.Row, currentCell.Column))
            {
                throw new ArgumentOutOfRangeException(
                    nameof(currentCell),
                    "The selected cell is outside the chessboard.");
            }

            board = ResetBoard(board);
            string normalizedPiece = chessPiece?.Trim().ToLowerInvariant() ?? string.Empty;

            switch (normalizedPiece)
            {
                case "knight":
                    board.Grid[currentCell.Row, currentCell.Column].PieceOccupyingCell = "N";
                    board = MarkValidKnightMoves(board, currentCell);
                    break;

                case "rook":
                    board.Grid[currentCell.Row, currentCell.Column].PieceOccupyingCell = "R";
                    board = MarkValidRookMoves(board, currentCell);
                    break;

                case "bishop":
                    board.Grid[currentCell.Row, currentCell.Column].PieceOccupyingCell = "B";
                    board = MarkValidBishopMoves(board, currentCell);
                    break;

                case "queen":
                    board.Grid[currentCell.Row, currentCell.Column].PieceOccupyingCell = "Q";
                    board = MarkValidQueenMoves(board, currentCell);
                    break;

                case "king":
                    board.Grid[currentCell.Row, currentCell.Column].PieceOccupyingCell = "K";
                    board = MarkValidKingMoves(board, currentCell);
                    break;

                default:
                    // An invalid piece leaves the reset board unchanged.
                    return board;
            }

            return board;
        }

        /// <summary>
        /// Clears every selected piece and legal move marker.
        /// </summary>
        /// <param name="board">Board to clear.</param>
        /// <returns>The cleared board.</returns>
        public BoardModel ClearBoard(BoardModel board)
        {
            ArgumentNullException.ThrowIfNull(board);
            return ResetBoard(board);
        }

        /// <summary>
        /// Checks whether a row and column are within the board boundaries.
        /// </summary>
        public bool IsValidCoordinate(BoardModel board, int row, int column)
        {
            ArgumentNullException.ThrowIfNull(board);
            return IsOnBoard(board, row, column);
        }

        /// <summary>
        /// Resets each cell to its default state.
        /// </summary>
        private BoardModel ResetBoard(BoardModel board)
        {
            foreach (CellModel cell in board.Grid)
            {
                cell.IsLegalNextMove = false;
                cell.PieceOccupyingCell = string.Empty;
            }

            return board;
        }

        /// <summary>
        /// Checks whether a row and column are on the board.
        /// </summary>
        private bool IsOnBoard(BoardModel board, int row, int column)
        {
            bool isRowSafe = row >= 0 && row < board.Size;
            bool isColumnSafe = column >= 0 && column < board.Size;
            return isRowSafe && isColumnSafe;
        }

        /// <summary>
        /// Marks the eight possible L-shaped knight destinations when they are on the board.
        /// </summary>
        private BoardModel MarkValidKnightMoves(BoardModel board, CellModel currentCell)
        {
            int[] knightRowMoves = { 2, 1, -1, -2, -2, -1, 1, 2 };
            int[] knightColumnMoves = { 1, 2, 2, 1, -1, -2, -2, -1 };

            for (int index = 0; index < knightRowMoves.Length; index++)
            {
                int targetRow = currentCell.Row + knightRowMoves[index];
                int targetColumn = currentCell.Column + knightColumnMoves[index];
                MarkCellIfOnBoard(board, targetRow, targetColumn);
            }

            return board;
        }

        /// <summary>
        /// Marks all horizontal and vertical rook destinations.
        /// </summary>
        private BoardModel MarkValidRookMoves(BoardModel board, CellModel currentCell)
        {
            MarkLine(board, currentCell, -1, 0);
            MarkLine(board, currentCell, 1, 0);
            MarkLine(board, currentCell, 0, -1);
            MarkLine(board, currentCell, 0, 1);
            return board;
        }

        /// <summary>
        /// Marks all diagonal bishop destinations.
        /// </summary>
        private BoardModel MarkValidBishopMoves(BoardModel board, CellModel currentCell)
        {
            MarkLine(board, currentCell, -1, -1);
            MarkLine(board, currentCell, -1, 1);
            MarkLine(board, currentCell, 1, -1);
            MarkLine(board, currentCell, 1, 1);
            return board;
        }

        /// <summary>
        /// Marks all queen destinations by combining rook and bishop movement.
        /// </summary>
        private BoardModel MarkValidQueenMoves(BoardModel board, CellModel currentCell)
        {
            board = MarkValidRookMoves(board, currentCell);
            board = MarkValidBishopMoves(board, currentCell);
            return board;
        }

        /// <summary>
        /// Marks one square in every direction for the king.
        /// </summary>
        private BoardModel MarkValidKingMoves(BoardModel board, CellModel currentCell)
        {
            for (int rowOffset = -1; rowOffset <= 1; rowOffset++)
            {
                for (int columnOffset = -1; columnOffset <= 1; columnOffset++)
                {
                    if (rowOffset == 0 && columnOffset == 0)
                    {
                        continue;
                    }

                    MarkCellIfOnBoard(
                        board,
                        currentCell.Row + rowOffset,
                        currentCell.Column + columnOffset);
                }
            }

            return board;
        }

        /// <summary>
        /// Marks every square from a starting cell in one direction until the board edge.
        /// </summary>
        private void MarkLine(
            BoardModel board,
            CellModel currentCell,
            int rowDirection,
            int columnDirection)
        {
            int row = currentCell.Row + rowDirection;
            int column = currentCell.Column + columnDirection;

            while (IsOnBoard(board, row, column))
            {
                board.Grid[row, column].IsLegalNextMove = true;
                row += rowDirection;
                column += columnDirection;
            }
        }

        /// <summary>
        /// Marks a destination only when it is inside the board.
        /// </summary>
        private void MarkCellIfOnBoard(BoardModel board, int row, int column)
        {
            if (IsOnBoard(board, row, column))
            {
                board.Grid[row, column].IsLegalNextMove = true;
            }
        }
    }
}
