/*
 * Aaron Chaussignand
 * CST-250: Programming in C# II
 * July 27, 2026
 * Flood Fill Recursion
 * Activity 3
 */

using FloodFillRecursion.Models;

namespace FloodFillRecursion;

internal static class Program
{
    private static void Main()
    {
        Console.Title = "Activity 3 - Flood Fill Recursion";
        Console.WriteLine("FLOOD FILL RECURSION");
        Console.WriteLine(new string('=', 48));

        int size = Utility.ReadInt("Board size (8-40): ", 8, 40);
        int shapeCount = Utility.ReadInt("Number of wall patterns (0-20): ", 0, 20);
        ShapeMode shapeMode = (ShapeMode)Utility.ReadInt(
            "Shape mode: 1 squares, 2 triangles, 3 random points, 4 mixed: ", 1, 4);
        bool eightWay = Utility.ReadYesNo("Use eight-way fill? (y/n): ");
        DirectionOrder order = (DirectionOrder)Utility.ReadInt(
            "Starting direction: 1 North, 2 East, 3 South, 4 West: ", 1, 4);

        BoardModel board = new(size, shapeCount, shapeMode);
        Utility.PrintBoard(board);

        int startRow = Utility.ReadInt($"Starting row (1-{size}): ", 1, size) - 1;
        int startColumn = Utility.ReadInt($"Starting column (1-{size}): ", 1, size) - 1;

        int filled = Utility.FloodFill(board, startRow, startColumn, eightWay, order);

        Console.WriteLine();
        Utility.PrintBoard(board);
        Console.WriteLine($"Filled cells: {filled}");
        Console.WriteLine($"Mode: {(eightWay ? "eight-way" : "four-way")}; first direction: {order}");
    }
}

internal enum DirectionOrder
{
    North = 1,
    East = 2,
    South = 3,
    West = 4
}

internal static class Utility
{
    private static readonly (int Row, int Column, string Name)[] CardinalDirections =
    {
        (-1, 0, "North"), (0, 1, "East"), (1, 0, "South"), (0, -1, "West")
    };

    private static readonly (int Row, int Column, string Name)[] DiagonalDirections =
    {
        (-1, -1, "Northwest"), (-1, 1, "Northeast"),
        (1, 1, "Southeast"), (1, -1, "Southwest")
    };

    /// <summary>Recursively fills connected empty cells and returns the number filled.</summary>
    internal static int FloodFill(
        BoardModel board,
        int row,
        int column,
        bool eightWay,
        DirectionOrder startingDirection)
    {
        List<(int Row, int Column, string Name)> directions = BuildDirectionOrder(eightWay, startingDirection);
        return FloodFillRecursive(board, row, column, directions);
    }

    private static int FloodFillRecursive(
        BoardModel board,
        int row,
        int column,
        IReadOnlyList<(int Row, int Column, string Name)> directions)
    {
        if (row < 0 || row >= board.Size || column < 0 || column >= board.Size)
        {
            return 0;
        }

        CellModel cell = board.Grid[row, column];
        if (cell.Contents is 'W' or 'F')
        {
            return 0;
        }

        cell.Contents = 'F';
        int filled = 1;

        foreach ((int rowOffset, int columnOffset, string _) in directions)
        {
            filled += FloodFillRecursive(board, row + rowOffset, column + columnOffset, directions);
        }

        return filled;
    }

    private static List<(int Row, int Column, string Name)> BuildDirectionOrder(
        bool eightWay,
        DirectionOrder startingDirection)
    {
        int startIndex = (int)startingDirection - 1;
        List<(int Row, int Column, string Name)> ordered = new();
        for (int offset = 0; offset < CardinalDirections.Length; offset++)
        {
            ordered.Add(CardinalDirections[(startIndex + offset) % CardinalDirections.Length]);
        }
        if (eightWay)
        {
            ordered.AddRange(DiagonalDirections);
        }
        return ordered;
    }

    /// <summary>Prints coordinates and custom symbols/colors for empty, wall, and filled cells.</summary>
    internal static void PrintBoard(BoardModel board)
    {
        Console.ResetColor();
        Console.Write("   ");
        for (int column = 1; column <= board.Size; column++) Console.Write($"{column,2}");
        Console.WriteLine();

        for (int row = 0; row < board.Size; row++)
        {
            Console.ResetColor();
            Console.Write($"{row + 1,2} ");
            for (int column = 0; column < board.Size; column++)
            {
                char contents = board.Grid[row, column].Contents;
                Console.ForegroundColor = contents switch
                {
                    'W' => ConsoleColor.Red,
                    'F' => ConsoleColor.Cyan,
                    _ => ConsoleColor.DarkGray
                };
                Console.Write(contents switch { 'W' => " #", 'F' => " ~", _ => " ." });
            }
            Console.WriteLine();
        }
        Console.ResetColor();
    }

    internal static int ReadInt(string prompt, int minimum, int maximum)
    {
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out int value) && value >= minimum && value <= maximum)
            {
                return value;
            }
            Console.WriteLine($"Enter a whole number from {minimum} through {maximum}.");
        }
    }

    internal static bool ReadYesNo(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine()?.Trim().ToLowerInvariant();
            if (input is "y" or "yes") return true;
            if (input is "n" or "no") return false;
            Console.WriteLine("Enter y or n.");
        }
    }
}
