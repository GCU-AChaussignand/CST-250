/*
 * Aaron Chaussignand
 * CST-250: Programming in C# II
 * July 27, 2026
 * Flood Fill Recursion - Board Model
 * Activity 3
 */

namespace FloodFillRecursion.Models;

internal enum ShapeMode
{
    Squares = 1,
    Triangles = 2,
    RandomPoints = 3,
    Mixed = 4
}

internal sealed class BoardModel
{
    public int Size { get; }
    public CellModel[,] Grid { get; }
    public int NumShapes { get; }
    public ShapeMode ShapeMode { get; }

    public BoardModel(int size, int numShapes, ShapeMode shapeMode, int seed = 250)
    {
        if (size is < 8 or > 40) throw new ArgumentOutOfRangeException(nameof(size));
        if (numShapes is < 0 or > 20) throw new ArgumentOutOfRangeException(nameof(numShapes));

        Size = size;
        NumShapes = numShapes;
        ShapeMode = shapeMode;
        Grid = new CellModel[size, size];

        for (int row = 0; row < Size; row++)
        {
            for (int column = 0; column < Size; column++)
            {
                Grid[row, column] = new CellModel(row, column, 'E');
            }
        }

        PlaceShapes(new Random(seed));
    }

    /// <summary>Places square, triangular, random-point, or mixed wall patterns.</summary>
    private void PlaceShapes(Random random)
    {
        for (int shape = 0; shape < NumShapes; shape++)
        {
            ShapeMode selected = ShapeMode == ShapeMode.Mixed
                ? (ShapeMode)random.Next(1, 4)
                : ShapeMode;

            switch (selected)
            {
                case ShapeMode.Squares:
                    PlaceSquare(random);
                    break;
                case ShapeMode.Triangles:
                    PlaceTriangle(random);
                    break;
                case ShapeMode.RandomPoints:
                    PlaceRandomPoints(random);
                    break;
            }
        }
    }

    private void PlaceSquare(Random random)
    {
        int side = random.Next(3, Math.Max(4, Size / 3));
        int row = random.Next(0, Size - side);
        int column = random.Next(0, Size - side);

        for (int offset = 0; offset < side; offset++)
        {
            SetWall(row, column + offset);
            SetWall(row + side - 1, column + offset);
            SetWall(row + offset, column);
            SetWall(row + offset, column + side - 1);
        }
    }

    private void PlaceTriangle(Random random)
    {
        int height = random.Next(3, Math.Max(4, Size / 3));
        int topRow = random.Next(0, Size - height);
        int centerColumn = random.Next(height, Size - height);

        for (int level = 0; level < height; level++)
        {
            SetWall(topRow + level, centerColumn - level);
            SetWall(topRow + level, centerColumn + level);
        }
        for (int column = centerColumn - height + 1; column <= centerColumn + height - 1; column++)
        {
            SetWall(topRow + height - 1, column);
        }
    }

    private void PlaceRandomPoints(Random random)
    {
        int count = Math.Max(3, Size / 2);
        for (int index = 0; index < count; index++)
        {
            SetWall(random.Next(Size), random.Next(Size));
        }
    }

    private void SetWall(int row, int column)
    {
        if (row >= 0 && row < Size && column >= 0 && column < Size)
        {
            Grid[row, column].Contents = 'W';
        }
    }
}
