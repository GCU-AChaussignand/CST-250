using MinesweeperClassLibrary.BusinessLogicLayer;
using MinesweeperClassLibrary.Models;
using MinesweeperConsoleApp.Utilities;

namespace MinesweeperConsoleApp;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.Title = "CST-250 Milestone 2 - Minesweeper";
        Console.WriteLine("Hello, welcome to Minesweeper");
        Console.WriteLine("Visit every safe cell without visiting a bomb.");
        Console.WriteLine();

        int boardSize = ConsoleInput.ReadInteger(
            "Enter a board size from 4 through 15: ",
            4,
            15);

        int maximumBombs = Math.Max(1, (boardSize * boardSize) / 4);
        int bombCount = ConsoleInput.ReadInteger(
            $"Enter the number of bombs from 1 through {maximumBombs}: ",
            1,
            maximumBombs);

        BoardModel board = new BoardModel(boardSize)
        {
            Difficulty = bombCount
        };

        IBoardLogic boardLogic = new BoardLogic();
        boardLogic.SetupBombs(board);
        boardLogic.CountBombsNearby(board);
        boardLogic.PlaceRandomReward(board, RewardType.BombDetector);

        // The milestone guide shows the answer key first for testing purposes.
        ShowAnswers(board);
        PrintBoard(board);

        while (board.GameState == GameState.StillPlaying)
        {
            PlayTurn(board, boardLogic);
            boardLogic.DetermineGameState(board);
            PrintBoard(board);
        }

        int finalScore = boardLogic.DetermineFinalScore(board);
        ConsoleRenderer.PrintGameResult(board, finalScore);

        Console.WriteLine();
        Console.WriteLine("Final answer key:");
        ShowAnswers(board);
        Console.WriteLine("Press Enter to close the program.");
        Console.ReadLine();
    }

    /// <summary>
    /// Displays the answer key used for testing and the screencast demonstration.
    /// </summary>
    private static void ShowAnswers(BoardModel board)
    {
        ConsoleRenderer.PrintAnswers(board);
    }

    /// <summary>
    /// Displays the playable board while preserving hidden-cell state.
    /// </summary>
    private static void PrintBoard(BoardModel board)
    {
        ConsoleRenderer.PrintBoard(board);
    }

    private static void PlayTurn(BoardModel board, IBoardLogic boardLogic)
    {
        int row = ConsoleInput.ReadInteger(
            $"Enter the row number (0-{board.Size - 1}): ",
            0,
            board.Size - 1);

        int column = ConsoleInput.ReadInteger(
            $"Enter the column number (0-{board.Size - 1}): ",
            0,
            board.Size - 1);

        Console.WriteLine("Choose an action:");
        Console.WriteLine("1 - Visit the cell");
        Console.WriteLine("2 - Flag or unflag the cell");
        Console.WriteLine("3 - Use a reward (bomb detector)");

        int action = ConsoleInput.ReadInteger("Enter 1, 2, or 3: ", 1, 3);
        CellModel selectedCell = board.Cells[row, column];

        switch (action)
        {
            case 1:
                VisitCell(board, boardLogic, selectedCell, row, column);
                break;

            case 2:
                FlagCell(board, boardLogic, selectedCell, row, column);
                break;

            case 3:
                UseReward(board, boardLogic, row, column);
                break;
        }

        Console.WriteLine();
    }

    private static void VisitCell(
        BoardModel board,
        IBoardLogic boardLogic,
        CellModel selectedCell,
        int row,
        int column)
    {
        if (selectedCell.IsFlagged)
        {
            Console.WriteLine("That cell is flagged. Unflag it before visiting it.");
            return;
        }

        if (selectedCell.IsVisited)
        {
            Console.WriteLine("That cell has already been visited.");
            return;
        }

        int rewardsBeforeMove = board.RewardsRemaining;
        boardLogic.RevealCell(board, row, column);

        if (board.RewardsRemaining > rewardsBeforeMove)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("You found a reward! You earned one bomb-detector use.");
            Console.ResetColor();
        }
    }

    private static void FlagCell(
        BoardModel board,
        IBoardLogic boardLogic,
        CellModel selectedCell,
        int row,
        int column)
    {
        if (selectedCell.IsVisited)
        {
            Console.WriteLine("A visited cell cannot be flagged.");
            return;
        }

        bool isNowFlagged = boardLogic.ToggleFlag(board, row, column);
        Console.WriteLine(isNowFlagged
            ? "The cell is now flagged."
            : "The flag was removed from the cell.");
    }

    private static void UseReward(
        BoardModel board,
        IBoardLogic boardLogic,
        int row,
        int column)
    {
        if (board.RewardsRemaining <= 0)
        {
            Console.WriteLine("You do not have a reward available yet.");
            return;
        }

        bool isBomb = boardLogic.UseSpecialBonus(board, row, column);
        Console.ForegroundColor = isBomb ? ConsoleColor.Red : ConsoleColor.Green;
        Console.WriteLine($"Bomb detector result: Is this cell a bomb? {isBomb}");
        Console.ResetColor();
    }
}
