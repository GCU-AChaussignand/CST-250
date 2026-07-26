/*
 * Aaron Chaussignand
 * CST-250: Programming in C# II
 * 07/20/2026
 * Chess Board Project - Activity 2
 * Windows Forms startup code.
 */

namespace ChessBoardGUIApp
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new FrmChessBoard());
        }
    }
}
