using ChessBoardClassLibrary.Models;
namespace ChessBoardClassLibrary.Services.BusinessLogicLayer;
public class BoardLogic
{
    public bool IsCoordinateValid(BoardModel board, int row, int column)
    {
        ArgumentNullException.ThrowIfNull(board);
        return row >= 0 && row < board.Size && column >= 0 && column < board.Size;
    }
    public void ResetBoard(BoardModel board)
    {
        ArgumentNullException.ThrowIfNull(board);
        foreach (CellModel cell in board.Cells) { cell.IsSelected = false; cell.IsLegalMove = false; cell.Piece = string.Empty; }
    }
    public int MarkLegalMoves(BoardModel board, ChessPieceType piece, int row, int column)
    {
        ArgumentNullException.ThrowIfNull(board);
        if (!IsCoordinateValid(board, row, column)) throw new ArgumentOutOfRangeException(nameof(row), "Coordinate is outside the board.");
        ResetBoard(board);
        board.Cells[row, column].IsSelected = true;
        board.Cells[row, column].Piece = piece.ToString();
        int count = 0;
        foreach ((int r, int c) in GetLegalMoveCoordinates(board, piece, row, column))
        {
            if (r == row && c == column) continue;
            board.Cells[r, c].IsLegalMove = true; count++;
        }
        return count;
    }
    public List<CellModel> GetLegalMoveCells(BoardModel board, ChessPieceType piece, int row, int column)
        => GetLegalMoveCoordinates(board, piece, row, column).Select(m => board.Cells[m.Row, m.Column]).ToList();
    public List<(int Row, int Column)> GetLegalMoveCoordinates(BoardModel board, ChessPieceType piece, int row, int column)
    {
        ArgumentNullException.ThrowIfNull(board);
        if (!IsCoordinateValid(board, row, column)) throw new ArgumentOutOfRangeException(nameof(row), "Coordinate is outside the board.");
        HashSet<(int Row, int Column)> moves = new();
        switch (piece)
        {
            case ChessPieceType.King: AddKing(board, row, column, moves); break;
            case ChessPieceType.Queen: AddLineSet(board,row,column,moves,true,true); break;
            case ChessPieceType.Rook: AddLineSet(board,row,column,moves,true,false); break;
            case ChessPieceType.Bishop: AddLineSet(board,row,column,moves,false,true); break;
            case ChessPieceType.Knight: AddKnight(board,row,column,moves); break;
            case ChessPieceType.Pawn: AddIfValid(board,row-1,column,moves); AddIfValid(board,row-1,column-1,moves); AddIfValid(board,row-1,column+1,moves); break;
        }
        return moves.OrderBy(m => m.Row).ThenBy(m => m.Column).ToList();
    }
    private void AddLineSet(BoardModel b, int r, int c, HashSet<(int Row,int Column)> moves, bool straight, bool diagonal)
    {
        if (straight) { AddLine(b,r,c,moves,1,0); AddLine(b,r,c,moves,-1,0); AddLine(b,r,c,moves,0,1); AddLine(b,r,c,moves,0,-1); }
        if (diagonal) { AddLine(b,r,c,moves,1,1); AddLine(b,r,c,moves,1,-1); AddLine(b,r,c,moves,-1,1); AddLine(b,r,c,moves,-1,-1); }
    }
    private void AddLine(BoardModel b, int r, int c, HashSet<(int Row,int Column)> moves, int dr, int dc)
    { int nr = r + dr, nc = c + dc; while (IsCoordinateValid(b,nr,nc)) { moves.Add((nr,nc)); nr += dr; nc += dc; } }
    private void AddKing(BoardModel b, int r, int c, HashSet<(int Row,int Column)> moves)
    { for (int dr=-1; dr<=1; dr++) for (int dc=-1; dc<=1; dc++) if (dr != 0 || dc != 0) AddIfValid(b,r+dr,c+dc,moves); }
    private void AddKnight(BoardModel b, int r, int c, HashSet<(int Row,int Column)> moves)
    { int[,] o={{-2,-1},{-2,1},{-1,-2},{-1,2},{1,-2},{1,2},{2,-1},{2,1}}; for(int i=0;i<o.GetLength(0);i++) AddIfValid(b,r+o[i,0],c+o[i,1],moves); }
    private void AddIfValid(BoardModel b, int r, int c, HashSet<(int Row,int Column)> moves) { if (IsCoordinateValid(b,r,c)) moves.Add((r,c)); }
}
