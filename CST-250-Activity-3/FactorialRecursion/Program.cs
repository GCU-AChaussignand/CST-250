/*
 * Aaron Chaussignand
 * CST-250: Programming in C# II
 * July 27, 2026
 * Factorial Recursion
 * Activity 3
 */

using System.Diagnostics;
using System.Numerics;
using FactorialRecursion.Services.BusinessLogicLayer;

namespace FactorialRecursion;

internal static class Program
{
    private static void Main()
    {
        Console.Title = "Activity 3 - Factorial Recursion";
        Console.WriteLine("FACTORIAL: ITERATION AND RECURSION");
        Console.WriteLine(new string('=', 48));

        int input = Utility.ReadNonnegativeInt("Enter a nonnegative integer (0-2000): ", 2000);
        FactorialLogic logic = new();

        Stopwatch iterativeTimer = Stopwatch.StartNew();
        BigInteger iterativeAnswer = logic.SolveIterativeFactorial(input);
        iterativeTimer.Stop();

        Stopwatch recursiveTimer = Stopwatch.StartNew();
        BigInteger recursiveAnswer = logic.SolveRecursiveFactorial(input);
        recursiveTimer.Stop();

        Console.WriteLine();
        Console.WriteLine($"Iterative answer: {iterativeAnswer}");
        Console.WriteLine($"Recursive answer: {recursiveAnswer}");
        Console.WriteLine($"Answers match: {iterativeAnswer == recursiveAnswer}");
        Console.WriteLine($"Iterative elapsed ticks: {iterativeTimer.ElapsedTicks}");
        Console.WriteLine($"Recursive elapsed ticks: {recursiveTimer.ElapsedTicks}");
    }
}

internal static class Utility
{
    /// <summary>Reads a validated nonnegative integer from the console.</summary>
    internal static int ReadNonnegativeInt(string prompt, int maximum)
    {
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out int value) && value >= 0 && value <= maximum)
            {
                return value;
            }
            Console.WriteLine($"Invalid input. Enter a whole number from 0 through {maximum}.");
        }
    }
}
