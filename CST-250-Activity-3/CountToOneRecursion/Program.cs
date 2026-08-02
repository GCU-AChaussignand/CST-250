/*
 * Aaron Chaussignand
 * CST-250: Programming in C# II
 * July 27, 2026
 * Count To One Recursion
 * Activity 3
 */

namespace CountToOneRecursion;

internal static class Program
{
    private static void Main()
    {
        Console.Title = "Activity 3 - Count To One Recursion";
        Console.WriteLine("COUNT TO ONE RECURSION");
        Console.WriteLine(new string('=', 48));

        long number = Utility.ReadLong("Enter any whole number: ");
        CountOptions options = Utility.ReadOptions();

        CountResult result = Utility.CountToOne(number, options);

        Console.WriteLine();
        Console.WriteLine(result.ReachedOne
            ? $"The sequence reached 1 in {result.Steps} recursive calls."
            : $"The sequence stopped after {result.Steps} calls: {result.StopReason}");
        Console.WriteLine($"Final value: {result.FinalValue}");
        Console.WriteLine("Sequence: " + string.Join(" -> ", result.Sequence));
    }
}

internal enum OddOperation
{
    AddOne = 1,
    SubtractOne = 2,
    AddTwo = 3
}

internal sealed record CountOptions(
    OddOperation OddOperation,
    bool PreferDivisionByFour,
    bool PreferDivisionByThree,
    bool MultiplyMultiplesOfFive,
    int MultiplicationFactor,
    int MaximumCalls);

internal sealed record CountResult(
    bool ReachedOne,
    int Steps,
    long FinalValue,
    string StopReason,
    IReadOnlyList<long> Sequence);

internal static class Utility
{
    /// <summary>
    /// Recursively transforms a whole number toward one while tracking calls and detecting cycles.
    /// The options demonstrate the Activity 3 Count To One challenge variations.
    /// </summary>
    internal static CountResult CountToOne(long number, CountOptions options)
    {
        List<long> sequence = new();
        HashSet<long> visited = new();
        return CountToOneRecursive(NormalizeInput(number), options, 0, sequence, visited);
    }

    private static CountResult CountToOneRecursive(
        long number,
        CountOptions options,
        int calls,
        List<long> sequence,
        HashSet<long> visited)
    {
        sequence.Add(number);
        Console.WriteLine($"Call {calls,3}: current number = {number}");

        if (number == 1)
        {
            return new CountResult(true, calls, number, "Reached the base case.", sequence);
        }

        if (calls >= options.MaximumCalls)
        {
            return new CountResult(false, calls, number, "Maximum-call safety limit reached.", sequence);
        }

        if (!visited.Add(number))
        {
            return new CountResult(false, calls, number, "A repeated value created a cycle.", sequence);
        }

        long next;
        try
        {
            checked
            {
                if (options.MultiplyMultiplesOfFive && number % 5 == 0)
                {
                    next = number * options.MultiplicationFactor;
                    Console.WriteLine($"         divisible by 5: multiply by {options.MultiplicationFactor}");
                }
                else if (options.PreferDivisionByFour && number % 4 == 0)
                {
                    next = number / 4;
                    Console.WriteLine("         divisible by 4: divide by 4");
                }
                else if (options.PreferDivisionByThree && number % 3 == 0)
                {
                    next = number / 3;
                    Console.WriteLine("         divisible by 3: divide by 3");
                }
                else if (number % 2 == 0)
                {
                    next = number / 2;
                    Console.WriteLine("         even: divide by 2");
                }
                else
                {
                    next = options.OddOperation switch
                    {
                        OddOperation.AddOne => number + 1,
                        OddOperation.SubtractOne => number - 1,
                        OddOperation.AddTwo => number + 2,
                        _ => number + 1
                    };
                    Console.WriteLine($"         odd: {DescribeOddOperation(options.OddOperation)}");
                }
            }
        }
        catch (OverflowException)
        {
            return new CountResult(false, calls, number, "The selected operation exceeded Int64 limits.", sequence);
        }

        if (next < 0)
        {
            next = Math.Abs(next);
        }

        return CountToOneRecursive(next, options, calls + 1, sequence, visited);
    }

    /// <summary>Supports zero and negative input by converting it to a positive starting value.</summary>
    private static long NormalizeInput(long number)
    {
        if (number == long.MinValue)
        {
            return long.MaxValue;
        }

        number = Math.Abs(number);
        return number == 0 ? 1 : number;
    }

    internal static long ReadLong(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            if (long.TryParse(Console.ReadLine(), out long value))
            {
                return value;
            }
            Console.WriteLine("Invalid input. Enter a whole number.");
        }
    }

    internal static CountOptions ReadOptions()
    {
        Console.WriteLine();
        Console.WriteLine("Odd-number operation:");
        Console.WriteLine("1 - Add 1 (original activity behavior)");
        Console.WriteLine("2 - Subtract 1");
        Console.WriteLine("3 - Add 2 (may create a cycle; cycle detection is enabled)");
        int oddChoice = ReadIntInRange("Choose 1-3: ", 1, 3);

        bool divide4 = ReadYesNo("Prefer division by 4 when possible? (y/n): ");
        bool divide3 = ReadYesNo("Prefer division by 3 when possible? (y/n): ");
        bool multiply = ReadYesNo("Multiply values divisible by 5? (y/n): ");
        int factor = multiply ? ReadIntInRange("Multiplication factor (2-5): ", 2, 5) : 2;

        return new CountOptions((OddOperation)oddChoice, divide4, divide3, multiply, factor, 500);
    }

    private static int ReadIntInRange(string prompt, int minimum, int maximum)
    {
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out int value) && value >= minimum && value <= maximum)
            {
                return value;
            }
            Console.WriteLine($"Enter a number from {minimum} through {maximum}.");
        }
    }

    private static bool ReadYesNo(string prompt)
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

    private static string DescribeOddOperation(OddOperation operation) => operation switch
    {
        OddOperation.AddOne => "add 1",
        OddOperation.SubtractOne => "subtract 1",
        OddOperation.AddTwo => "add 2",
        _ => "add 1"
    };
}
