/*
 * Aaron Chaussignand
 * CST-250: Programming in C# II
 * 07/20/2026
 * Chess Board Project - Activity 2
 * Utility methods for validated console input and formatted board output.
 */

using ChessBoardClassLibrary.Models;

namespace ChessBoardConsoleApp
{
    /// <summary>
    /// Provides reusable console input and output methods.
    /// </summary>
    public static class Utility
    {
        private static readonly string[] ValidPieces =
        {
            "King",
            "Queen",
            "Bishop",
            "Knight",
            "Rook"
        };

        /// <summary>
        /// Reads and validates one of the five required chess pieces.
        /// </summary>
        public static string GetChessPiece()
        {
            while (true)
            {
                Console.Write("Enter a piece (King, Queen, Bishop, Knight, Rook): ");
                string input = Console.ReadLine()?.Trim() ?? string.Empty;

                string? matchingPiece = ValidPieces.FirstOrDefault(
                    piece => piece.Equals(input, StringComparison.OrdinalIgnoreCase));

                if (matchingPiece is not null)
                {
                    return matchingPiece;
                }

                Console.WriteLine("Invalid piece. Choose King, Queen, Bishop, Knight, or Rook.");
            }
        }

        /// <summary>
        /// Gets a validated row and column and returns them as a tuple.
        /// </summary>
        public static Tuple<int, int> GetRowAndCol(BoardModel board)
        {
            ArgumentNullException.ThrowIfNull(board);

            int row = ReadCoordinate("row", board.Size);
            int column = ReadCoordinate("column", board.Size);
            return Tuple.Create(row, column);
        }

        /// <summary>
        /// Prints the board with row numbers, column headers, and outlined cells.
        /// </summary>
        public static void PrintBoard(BoardModel board)
        {
            ArgumentNullException.ThrowIfNull(board);

            Console.WriteLine();
            Console.Write("    ");

            for (int column = 0; column < board.Size; column++)
            {
                Console.Write($" {column,2} ");
            }

            Console.WriteLine();
            PrintHorizontalBorder(board.Size);

            for (int row = 0; row < board.Size; row++)
            {
                Console.Write($"{row,2}  |");

                for (int column = 0; column < board.Size; column++)
                {
                    CellModel cell = board.Grid[row, column];
                    Console.Write($" {GetCellSymbol(cell)} |");
                }

                Console.WriteLine();
                PrintHorizontalBorder(board.Size);
            }

            Console.WriteLine("Legend: + = legal move; K/Q/B/N/R = selected piece; . = empty");
            Console.WriteLine();
        }

        /// <summary>
        /// Reads a Y or N response without allowing invalid input to crash the program.
        /// </summary>
        public static bool GetYesNo(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine()?.Trim() ?? string.Empty;

                if (input.Equals("Y", StringComparison.OrdinalIgnoreCase)
                    || input.Equals("Yes", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }

                if (input.Equals("N", StringComparison.OrdinalIgnoreCase)
                    || input.Equals("No", StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }

                Console.WriteLine("Please enter Y or N.");
            }
        }

        /// <summary>
        /// Reads one coordinate and ensures it remains within the board.
        /// </summary>
        private static int ReadCoordinate(string coordinateName, int boardSize)
        {
            while (true)
            {
                Console.Write($"Enter the {coordinateName} number (0-{boardSize - 1}): ");
                string? input = Console.ReadLine();

                if (int.TryParse(input, out int coordinate)
                    && coordinate >= 0
                    && coordinate < boardSize)
                {
                    return coordinate;
                }

                Console.WriteLine(
                    $"Invalid {coordinateName}. Enter a whole number from 0 through {boardSize - 1}.");
            }
        }

        /// <summary>
        /// Selects the symbol displayed for one board cell.
        /// </summary>
        private static string GetCellSymbol(CellModel cell)
        {
            if (!string.IsNullOrWhiteSpace(cell.PieceOccupyingCell))
            {
                return cell.PieceOccupyingCell;
            }

            return cell.IsLegalNextMove ? "+" : ".";
        }

        /// <summary>
        /// Prints one horizontal board separator.
        /// </summary>
        private static void PrintHorizontalBorder(int boardSize)
        {
            Console.Write("    +");

            for (int column = 0; column < boardSize; column++)
            {
                Console.Write("---+");
            }

            Console.WriteLine();
        }
    }
}
