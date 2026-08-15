// Source: Author-created for CST-250 Activity 5 based on the Grand Canyon University Activity 5 guide (2025).
using System.Windows.Forms;
using WhackAMole.PresentationLayer;

namespace WhackAMole;

internal static class Program
{
    /// <summary>
    /// Application entry point.
    /// </summary>
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new FrmStopwatch());
    }
}
