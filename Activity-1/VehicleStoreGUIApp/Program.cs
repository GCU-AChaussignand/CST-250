// Aaron Chaussignand
// CST-250 Activity 1 - VehicleStoreGUIApp
// Date: July 13, 2026
// References: CST-250 Activity 1 Guide, GCU coding guidelines, and Microsoft WinForms documentation.

namespace VehicleStoreGUIApp
{
    internal static class Program
    {
        /// <summary>
        /// Main entry point for the Windows Forms application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new FrmVehicleStore());
        }
    }
}
