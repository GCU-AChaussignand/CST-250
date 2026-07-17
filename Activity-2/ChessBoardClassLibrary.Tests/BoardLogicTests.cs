using ChessBoardClassLibrary.Models;
using ChessBoardClassLibrary.Services.BusinessLogicLayer;
using Xunit;
namespace ChessBoardClassLibrary.Tests;
public class BoardLogicTests
{
    [Fact] public void BoardModel_Constructor_CreatesCellsWithCoordinates() { BoardModel b = new(8); Assert.Equal(8,b.Size); Assert.Equal(0,b.Cells[0,0].Row); Assert.Equal(7,b.Cells[7,7].Column); }
    [Fact] public void RookInCorner_MarksFourteenMoves() { BoardModel b = new(8); BoardLogic l = new(); Assert.Equal(14,l.MarkLegalMoves(b,ChessPieceType.Rook,0,0)); Assert.True(b.Cells[0,7].IsLegalMove); Assert.True(b.Cells[7,0].IsLegalMove); }
    [Fact] public void KnightInCorner_MarksTwoMoves() { BoardModel b = new(8); BoardLogic l = new(); Assert.Equal(2,l.MarkLegalMoves(b,ChessPieceType.Knight,0,0)); Assert.True(b.Cells[1,2].IsLegalMove); Assert.True(b.Cells[2,1].IsLegalMove); }
    [Fact] public void QueenNearCenter_MarksTwentySevenMoves() { BoardModel b = new(8); BoardLogic l = new(); Assert.Equal(27,l.MarkLegalMoves(b,ChessPieceType.Queen,3,3)); Assert.True(b.Cells[7,7].IsLegalMove); }
    [Fact] public void KingInCenter_ReturnsEightCells() { BoardModel b = new(8); BoardLogic l = new(); Assert.Equal(8,l.GetLegalMoveCells(b,ChessPieceType.King,4,4).Count); }
    [Fact] public void ResetBoard_ClearsState() { BoardModel b = new(8); BoardLogic l = new(); l.MarkLegalMoves(b,ChessPieceType.Bishop,3,3); l.ResetBoard(b); foreach(CellModel c in b.Cells){Assert.False(c.IsSelected); Assert.False(c.IsLegalMove);} }
    [Fact] public void InvalidCoordinate_ThrowsException() { BoardModel b = new(8); BoardLogic l = new(); Assert.Throws<ArgumentOutOfRangeException>(()=>l.MarkLegalMoves(b,ChessPieceType.Rook,9,0)); }
    [Fact] public void InvalidSize_ThrowsException() { Assert.Throws<ArgumentOutOfRangeException>(()=>new BoardModel(3)); }
}
