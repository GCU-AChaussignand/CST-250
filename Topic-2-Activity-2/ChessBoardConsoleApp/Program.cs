/*
 * Aaron Chaussignand
 * CST-250: Programming in C# II
 * 07/20/2026
 * Chess Board Project - Activity 2
 * Console presentation layer for the shared chessboard class library.
 */

using ChessBoardClassLibrary.Models;
using ChessBoardClassLibrary.Services.BusinessLogicLayer;
using ChessBoardConsoleApp;

Console.Title = "CST-250 Activity 2 - Chess Board";
Console.WriteLine("Hello, Chess Players!");
Console.WriteLine("This application displays legal moves for a selected chess piece.");

bool runAgain;

do
{
    BoardModel board = new(8);
    BoardLogic boardLogic = new();

    Console.WriteLine();
    Console.WriteLine("Empty chessboard:");
    Utility.PrintBoard(board);

    string piece = Utility.GetChessPiece();
    Tuple<int, int> result = Utility.GetRowAndCol(board);

    board = boardLogic.MarkLegalMoves(
        board,
        board.Grid[result.Item1, result.Item2],
        piece);

    Console.WriteLine();
    Console.WriteLine($"{piece} placed at row {result.Item1}, column {result.Item2}.");
    Utility.PrintBoard(board);

    runAgain = Utility.GetYesNo("Try another piece? (Y/N): ");
}
while (runAgain);

Console.WriteLine();
Console.WriteLine("Thank you for using the chessboard application.");
