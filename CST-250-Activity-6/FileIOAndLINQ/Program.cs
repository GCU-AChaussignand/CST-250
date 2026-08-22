using FileIOAndLINQ.PresentationLayer;

namespace FileIOAndLINQ;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new FrmVerseList());
    }
}
