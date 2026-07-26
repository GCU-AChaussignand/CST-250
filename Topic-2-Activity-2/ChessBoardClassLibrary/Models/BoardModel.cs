/*
 * Aaron Chaussignand
 * CST-250: Programming in C# II
 * 07/20/2026
 * Chess Board Project - Activity 2
 * BoardModel stores the square board as a two-dimensional CellModel array.
 */

namespace ChessBoardClassLibrary.Models
{
    /// <summary>
    /// Represents a square chessboard made from a two-dimensional array of cells.
    /// </summary>
    public class BoardModel
    {
        /// <summary>
        /// Gets the number of rows and columns in the square board.
        /// </summary>
        public int Size { get; private set; }

        /// <summary>
        /// Gets the two-dimensional array containing every board cell.
        /// </summary>
        public CellModel[,] Grid { get; private set; }

        /// <summary>
        /// Initializes a square board and creates every cell in the grid.
        /// </summary>
        /// <param name="size">Board width and height.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when size is less than one.
        /// </exception>
        public BoardModel(int size)
        {
            if (size < 1)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(size),
                    "Board size must be at least one.");
            }

            Size = size;
            Grid = new CellModel[Size, Size];
            InitializeBoard();
        }

        /// <summary>
        /// Creates a CellModel for every row and column in the grid.
        /// The private access modifier encapsulates the initialization details.
        /// </summary>
        private void InitializeBoard()
        {
            for (int row = 0; row < Size; row++)
            {
                for (int column = 0; column < Size; column++)
                {
                    Grid[row, column] = new CellModel(row, column);
                }
            }
        }
    }
}
