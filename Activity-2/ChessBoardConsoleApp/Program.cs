using ChessBoardClassLibrary.Models;
using ChessBoardClassLibrary.Services.BusinessLogicLayer;
namespace ChessBoardConsoleApp;
internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("CST-250 Activity 2 - ChessBoard Application");
        BoardLogic logic = new();
        BoardModel board = new(Utility.ReadBoardSize());
        bool again = true;
        while (again)
        {
            Utility.PrintBoard(board);
            ChessPieceType piece = Utility.ReadPieceSelection();
            int row = Utility.ReadCoordinate("row", board.Size);
            int col = Utility.ReadCoordinate("column", board.Size);
            int count = logic.MarkLegalMoves(board, piece, row, col);
            Console.WriteLine($"{piece} selected at ({row}, {col}). {count} legal moves were marked.");
            Utility.PrintBoard(board);
            Console.Write("Run again? Y/N: ");
            again = string.Equals(Console.ReadLine(), "Y", StringComparison.OrdinalIgnoreCase);
        }
    }
}
