namespace MinesweeperConsoleApp.Utilities;

/// <summary>
/// Handles validated console input so Program remains focused on game flow.
/// </summary>
public static class ConsoleInput
{
    public static int ReadInteger(string prompt, int minimum, int maximum)
    {
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine();

            if (int.TryParse(input, out int value) &&
                value >= minimum &&
                value <= maximum)
            {
                return value;
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Please enter a whole number from {minimum} through {maximum}.");
            Console.ResetColor();
        }
    }
}
