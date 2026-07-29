/*
 * Aaron Chaussignand
 * CST-250: Programming in C# II
 * July 27, 2026
 * Flood Fill Recursion - Cell Model
 * Activity 3
 */

namespace FloodFillRecursion.Models;

internal sealed class CellModel
{
    public int Row { get; }
    public int Column { get; }
    public char Contents { get; set; }

    public CellModel(int row, int column, char contents)
    {
        Row = row;
        Column = column;
        Contents = contents;
    }
}
