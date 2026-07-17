using MinesweeperClassLibrary.Models;

namespace MinesweeperConsoleApp.Utilities;

/// <summary>
/// Handles console output for Minesweeper boards.
/// </summary>
public static class ConsoleRenderer
{
    public static void PrintAnswers(BoardModel board)
    {
        Console.WriteLine("Here is the answer key for the board");
        PrintHeader(board.Size);

        for (int row = 0; row < board.Size; row++)
        {
            PrintDivider(board.Size);
            Console.Write(row.ToString().PadLeft(3));

            for (int column = 0; column < board.Size; column++)
            {
                Console.Write(" | ");
                PrintAnswerCell(board.Cells[row, column]);
            }

            Console.WriteLine(" |");
        }

        PrintDivider(board.Size);
        Console.ResetColor();
        Console.WriteLine();
    }

    public static void PrintBoard(BoardModel board)
    {
        Console.WriteLine("Here is the current board");
        PrintHeader(board.Size);

        for (int row = 0; row < board.Size; row++)
        {
            PrintDivider(board.Size);
            Console.Write(row.ToString().PadLeft(3));

            for (int column = 0; column < board.Size; column++)
            {
                Console.Write(" | ");
                PrintVisibleCell(board.Cells[row, column]);
            }

            Console.WriteLine(" |");
        }

        PrintDivider(board.Size);
        Console.ResetColor();
        Console.WriteLine();
    }

    private static void PrintHeader(int size)
    {
        Console.Write("    ");
        for (int column = 0; column < size; column++)
        {
            Console.Write($" {column,2} ");
        }
        Console.WriteLine();
    }

    private static void PrintDivider(int size)
    {
        Console.Write("    ");
        for (int column = 0; column < size; column++)
        {
            Console.Write("+---");
        }
        Console.WriteLine("+");
    }

    private static void PrintAnswerCell(CellModel cell)
    {
        if (cell.IsBomb)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("B");
        }
        else if (cell.NumberOfBombNeighbors > 0)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(cell.NumberOfBombNeighbors);
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(".");
        }

        Console.ResetColor();
    }

    private static void PrintVisibleCell(CellModel cell)
    {
        if (cell.IsFlagged)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("F");
        }
        else if (!cell.IsVisited)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("?");
        }
        else
        {
            PrintAnswerCell(cell);
        }

        Console.ResetColor();
    }
}
