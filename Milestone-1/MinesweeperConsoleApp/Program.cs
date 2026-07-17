using MinesweeperClassLibrary.Models;
using MinesweeperClassLibrary.Services.BusinessLogicLayer;
using MinesweeperConsoleApp.Utilities;

namespace MinesweeperConsoleApp;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, welcome to Minesweeper");
        Console.WriteLine();

        IBoardLogic firstBoardLogic = new BoardLogic(seed: 2501);
        BoardModel firstBoard = new BoardModel(10)
        {
            Difficulty = 12
        };

        firstBoardLogic.SetupBombs(firstBoard);
        firstBoardLogic.CountBombsNearby(firstBoard);
        Console.WriteLine("First test board: size 10");
        ConsoleRenderer.PrintAnswers(firstBoard);

        IBoardLogic secondBoardLogic = new BoardLogic(seed: 2502);
        BoardModel secondBoard = new BoardModel(15)
        {
            Difficulty = 25
        };

        secondBoardLogic.SetupBombs(secondBoard);
        secondBoardLogic.CountBombsNearby(secondBoard);
        Console.WriteLine("Second test board: size 15");
        ConsoleRenderer.PrintAnswers(secondBoard);

        Console.WriteLine("Milestone 1 complete. The board model, BLL, console renderer, and xUnit project are included.");
    }
}
