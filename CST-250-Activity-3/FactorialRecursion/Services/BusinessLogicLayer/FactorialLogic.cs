/*
 * Aaron Chaussignand
 * CST-250: Programming in C# II
 * July 27, 2026
 * Factorial Recursion - Business Logic
 * Activity 3
 */

using System.Numerics;

namespace FactorialRecursion.Services.BusinessLogicLayer;

internal sealed class FactorialLogic
{
    /// <summary>Solves a factorial using iteration.</summary>
    internal BigInteger SolveIterativeFactorial(int factorial)
    {
        ValidateInput(factorial);
        BigInteger result = BigInteger.One;
        for (int i = factorial; i >= 1; i--)
        {
            result *= i;
        }
        return result;
    }

    /// <summary>Solves a factorial using recursion.</summary>
    internal BigInteger SolveRecursiveFactorial(int factorial)
    {
        ValidateInput(factorial);
        return factorial <= 1
            ? BigInteger.One
            : factorial * SolveRecursiveFactorial(factorial - 1);
    }

    private static void ValidateInput(int factorial)
    {
        if (factorial < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(factorial), "Factorial input cannot be negative.");
        }

        // A practical recursion-depth guard for a classroom console application.
        if (factorial > 2000)
        {
            throw new ArgumentOutOfRangeException(nameof(factorial), "Enter 2000 or less for the recursive demonstration.");
        }
    }
}
