using MinesweeperClassLibrary.Models;

namespace MinesweeperConsoleApp.Utilities;

/// <summary>
/// Handles all console rendering for the Minesweeper application.
/// </summary>
public static class ConsoleRenderer
{
    public static void PrintAnswers(BoardModel board)
    {
        Console.WriteLine("Here is the answer key for testing");
        PrintHeader(board.Size);

        for (int row = 0; row < board.Size; row++)
        {
            PrintDivider(board.Size);
            Console.Write($"{row,3}");

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
        Console.WriteLine($"Rewards available: {board.RewardsRemaining}");
        PrintHeader(board.Size);

        for (int row = 0; row < board.Size; row++)
        {
            PrintDivider(board.Size);
            Console.Write($"{row,3}");

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

    public static void PrintGameResult(BoardModel board, int finalScore)
    {
        if (board.GameState == GameState.Won)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Congratulations! You won!");
            Console.WriteLine($"Final score: {finalScore}");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("You selected a bomb. Game over.");
        }

        Console.ResetColor();
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
        else if (cell.HasSpecialReward)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("R");
        }
        else if (cell.NumberOfBombNeighbors > 0)
        {
            SetNumberColor(cell.NumberOfBombNeighbors);
            Console.Write(cell.NumberOfBombNeighbors);
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
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

    private static void SetNumberColor(int number)
    {
        Console.ForegroundColor = number switch
        {
            1 => ConsoleColor.Cyan,
            2 => ConsoleColor.Green,
            3 => ConsoleColor.Red,
            4 => ConsoleColor.Magenta,
            _ => ConsoleColor.Yellow
        };
    }
}
