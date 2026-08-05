/*
 * Aaron Chaussignand
 * Grand Canyon University
 * CST-250: Programming in C# II
 * Instructor: Brian Grey
 * Activity 4 - Pizza Maker
 * August 4, 2026
 *
 * Source: Grand Canyon University. (2025). CST-250 Activity 4:
 * Building a Data-Driven Pizza Order System with N-Layer Architecture.
 */

namespace PizzaMaker
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new FrmPizzaMaker());
        }
    }
}
