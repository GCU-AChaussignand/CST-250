using ChessBoardClassLibrary.Models;

namespace ChessBoardConsoleApp;

public static class Utility
{
    public static int ReadBoardSize()
    {
        while (true)
        {
            Console.Write("Enter board size 4-16, or press Enter for 8: ");
            string? input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                return 8;
            }

            if (int.TryParse(input, out int size) && size >= 4 && size <= 16)
            {
                return size;
            }

            Console.WriteLine("Invalid size.");
        }
    }

    public static ChessPieceType ReadPieceSelection()
    {
        while (true)
        {
            foreach (ChessPieceType piece in Enum.GetValues<ChessPieceType>())
            {
                Console.WriteLine($"{(int)piece}. {piece}");
            }

            Console.Write("Piece number: ");

            if (int.TryParse(Console.ReadLine(), out int value) &&
                Enum.IsDefined(typeof(ChessPieceType), value))
            {
                return (ChessPieceType)value;
            }

            Console.WriteLine("Invalid piece.");
        }
    }

    public static int ReadCoordinate(string label, int boardSize)
    {
        while (true)
        {
            Console.Write($"Enter {label} 0-{boardSize - 1}: ");

            if (int.TryParse(Console.ReadLine(), out int coordinate) &&
                coordinate >= 0 &&
                coordinate < boardSize)
            {
                return coordinate;
            }

            Console.WriteLine("Invalid coordinate.");
        }
    }

    public static void PrintBoard(BoardModel board)
    {
        Console.WriteLine();
        Console.WriteLine("S = selected, * = legal move, . = empty");

        Console.Write("    ");

        for (int column = 0; column < board.Size; column++)
        {
            Console.Write($" {column,2} ");
        }

        Console.WriteLine();

        for (int row = 0; row < board.Size; row++)
        {
            Console.Write($"{row,2} | ");

            for (int column = 0; column < board.Size; column++)
            {
                CellModel cell = board.Cells[row, column];

                if (cell.IsSelected)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("S");
                }
                else if (cell.IsLegalMove)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("*");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write(".");
                }

                Console.ResetColor();
                Console.Write(" | ");
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }
}
