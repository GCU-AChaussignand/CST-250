/*
 * Aaron Chaussignand
 * CST-250: Programming in C# II
 * 07/20/2026
 * Chess Board Project - Activity 2
 * Automated tests for board creation, movement rules, and validation.
 */

using ChessBoardClassLibrary.Models;
using ChessBoardClassLibrary.Services.BusinessLogicLayer;

namespace ChessBoardClassLibrary.Tests
{
    public class BoardLogicTests
    {
        private readonly BoardLogic _logic = new();

        [Fact]
        public void BoardModelConstructorCreatesEveryCellWithCorrectCoordinates()
        {
            BoardModel board = new(8);

            Assert.Equal(8, board.Size);
            Assert.Equal(0, board.Grid[0, 0].Row);
            Assert.Equal(0, board.Grid[0, 0].Column);
            Assert.Equal(7, board.Grid[7, 7].Row);
            Assert.Equal(7, board.Grid[7, 7].Column);
        }

        [Fact]
        public void KnightInCornerMarksTwoLegalMoves()
        {
            BoardModel board = new(8);
            _logic.MarkLegalMoves(board, board.Grid[0, 0], "Knight");

            Assert.Equal(2, CountLegalMoves(board));
            Assert.True(board.Grid[1, 2].IsLegalNextMove);
            Assert.True(board.Grid[2, 1].IsLegalNextMove);
            Assert.Equal("N", board.Grid[0, 0].PieceOccupyingCell);
        }

        [Fact]
        public void RookInCornerMarksFourteenLegalMoves()
        {
            BoardModel board = new(8);
            _logic.MarkLegalMoves(board, board.Grid[0, 0], "Rook");

            Assert.Equal(14, CountLegalMoves(board));
            Assert.True(board.Grid[0, 7].IsLegalNextMove);
            Assert.True(board.Grid[7, 0].IsLegalNextMove);
        }

        [Fact]
        public void BishopNearCenterMarksThirteenLegalMoves()
        {
            BoardModel board = new(8);
            _logic.MarkLegalMoves(board, board.Grid[3, 3], "Bishop");

            Assert.Equal(13, CountLegalMoves(board));
            Assert.True(board.Grid[0, 0].IsLegalNextMove);
            Assert.True(board.Grid[7, 7].IsLegalNextMove);
        }

        [Fact]
        public void QueenNearCenterMarksTwentySevenLegalMoves()
        {
            BoardModel board = new(8);
            _logic.MarkLegalMoves(board, board.Grid[3, 3], "Queen");

            Assert.Equal(27, CountLegalMoves(board));
            Assert.True(board.Grid[3, 7].IsLegalNextMove);
            Assert.True(board.Grid[7, 7].IsLegalNextMove);
        }

        [Fact]
        public void KingNearCenterMarksEightLegalMoves()
        {
            BoardModel board = new(8);
            _logic.MarkLegalMoves(board, board.Grid[3, 3], "King");

            Assert.Equal(8, CountLegalMoves(board));
        }

        [Fact]
        public void ClearBoardRemovesPieceAndLegalMoves()
        {
            BoardModel board = new(8);
            _logic.MarkLegalMoves(board, board.Grid[3, 3], "Queen");
            _logic.ClearBoard(board);

            Assert.Equal(0, CountLegalMoves(board));
            Assert.All(
                board.Grid.Cast<CellModel>(),
                cell => Assert.Equal(string.Empty, cell.PieceOccupyingCell));
        }

        [Fact]
        public void InvalidPieceReturnsAResetBoardWithoutCrashing()
        {
            BoardModel board = new(8);
            _logic.MarkLegalMoves(board, board.Grid[3, 3], "Dragon");

            Assert.Equal(0, CountLegalMoves(board));
            Assert.Equal(string.Empty, board.Grid[3, 3].PieceOccupyingCell);
        }

        [Fact]
        public void CoordinateValidationRejectsOutOfBoundsValues()
        {
            BoardModel board = new(8);

            Assert.False(_logic.IsValidCoordinate(board, -1, 0));
            Assert.False(_logic.IsValidCoordinate(board, 0, 8));
            Assert.True(_logic.IsValidCoordinate(board, 7, 7));
        }

        private static int CountLegalMoves(BoardModel board)
        {
            return board.Grid.Cast<CellModel>().Count(cell => cell.IsLegalNextMove);
        }
    }
}
