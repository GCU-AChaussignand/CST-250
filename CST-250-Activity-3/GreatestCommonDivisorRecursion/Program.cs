/*
 * Aaron Chaussignand
 * CST-250: Programming in C# II
 * July 27, 2026
 * Greatest Common Divisor Recursion
 * Activity 3
 */

using System.Diagnostics;

namespace GreatestCommonDivisorRecursion;

internal static class Program
{
    private static void Main()
    {
        Console.Title = "Activity 3 - Greatest Common Divisor";
        Console.WriteLine("GREATEST COMMON DIVISOR");
        Console.WriteLine(new string('=', 48));

        List<long> values = Utility.ReadNumberList();

        Stopwatch recursiveTimer = Stopwatch.StartNew();
        long recursive = Utility.GreatestCommonDivisor(values);
        recursiveTimer.Stop();

        Stopwatch iterativeTimer = Stopwatch.StartNew();
        long iterative = Utility.GreatestCommonDivisorIterative(values);
        iterativeTimer.Stop();

        Console.WriteLine();
        Console.WriteLine($"Numbers: {string.Join(", ", values)}");
        Console.WriteLine($"Recursive GCD: {recursive}");
        Console.WriteLine($"Iterative GCD: {iterative}");
        Console.WriteLine($"Answers match: {recursive == iterative}");
        Console.WriteLine($"Recursive elapsed ticks: {recursiveTimer.ElapsedTicks}");
        Console.WriteLine($"Iterative elapsed ticks: {iterativeTimer.ElapsedTicks}");
    }
}

internal static class Utility
{
    /// <summary>Calculates the GCD of two values with the recursive Euclidean algorithm.</summary>
    internal static long GreatestCommonDivisor(long number1, long number2)
    {
        number1 = SafeAbs(number1);
        number2 = SafeAbs(number2);

        if (number2 == 0)
        {
            return number1;
        }

        long remainder = number1 % number2;
        Console.WriteLine($"Recursive step: gcd({number1}, {number2}), remainder {remainder}");
        return GreatestCommonDivisor(number2, remainder);
    }

    /// <summary>Extends recursive GCD calculation to three or more values.</summary>
    internal static long GreatestCommonDivisor(IReadOnlyList<long> values)
    {
        if (values.Count < 2)
        {
            throw new ArgumentException("At least two values are required.", nameof(values));
        }

        long result = GreatestCommonDivisor(values[0], values[1]);
        for (int index = 2; index < values.Count; index++)
        {
            result = GreatestCommonDivisor(result, values[index]);
        }
        return result;
    }

    /// <summary>Calculates the GCD iteratively for comparison with recursion.</summary>
    internal static long GreatestCommonDivisorIterative(IReadOnlyList<long> values)
    {
        if (values.Count < 2)
        {
            throw new ArgumentException("At least two values are required.", nameof(values));
        }

        long result = SafeAbs(values[0]);
        for (int index = 1; index < values.Count; index++)
        {
            long other = SafeAbs(values[index]);
            while (other != 0)
            {
                (result, other) = (other, result % other);
            }
        }
        return result;
    }

    internal static List<long> ReadNumberList()
    {
        while (true)
        {
            Console.Write("Enter two or more whole numbers separated by spaces: ");
            string[] parts = (Console.ReadLine() ?? string.Empty)
                .Split(new[] { ' ', ',', ';' }, StringSplitOptions.RemoveEmptyEntries);

            List<long> values = new();
            bool valid = parts.Length >= 2;
            foreach (string part in parts)
            {
                if (!long.TryParse(part, out long value))
                {
                    valid = false;
                    break;
                }
                values.Add(value);
            }

            if (valid && values.Any(value => value != 0))
            {
                return values;
            }

            Console.WriteLine("Enter at least two valid integers; they cannot all be zero.");
        }
    }

    private static long SafeAbs(long value) => value == long.MinValue ? long.MaxValue : Math.Abs(value);
}
