namespace HealthTrackerApp
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // TODO: Replace with LoginForm after creating it
            // Application.Run(new LoginForm());

            // Temporary: Run empty form for testing
            Application.Run(new Form());
        }
    }
}