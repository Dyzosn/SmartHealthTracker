using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using HealthTrackerApp.Data;
using HealthTrackerApp.Forms;

namespace HealthTrackerApp
{
    // Main entry point for the Health Tracker application
    internal static class Program
    {
        // The main entry point for the application
        [STAThread]
        static void Main()
        {
            // Set application culture to Australian English (en-AU)
            // This for makes decimal separator is dot (.) not comma (,)
            // Date format: dd/MM/yyyy, Currency: AUD
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-AU");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-AU");
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-AU");
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en-AU");

            // Enable visual styles for modern Windows appearance
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Initialise database and seed initial data
            InitialiseDatabase();

            // Startup with login form
            Application.Run(new LoginForm());
        }

        // Initialise database with Entity Framework
        private static void InitialiseDatabase()
        {
            try
            {
                using (var context = new HealthTrackerContext())
                {
                    // Ensure database is created and initialise seed data
                    DbInitializer.Initialise(context);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database initialisation error: {ex.Message}\n\n" +
                    "Please ensure SQLite provider is installed correctly.",
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            }
        }
    }
}