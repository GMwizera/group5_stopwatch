namespace Timer
{
    /// <summary>
    /// Holds the program's entry point and starts the Windows Forms application.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application. Configures Windows Forms and
        ///  opens the main <see cref="Form1"/> window.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}