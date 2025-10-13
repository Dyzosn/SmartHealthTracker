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
            // This ensures decimal separator is dot (.) not comma (,)
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

            // Start the application with login form
            // Note: LoginForm will be implemented by Member 2 (Stanley Ng)
            // For testing purposes during development, you can temporarily use:
            // Application.Run(new DashboardForm(GetTestUser()));

            // Production startup (uncomment when LoginForm is ready):
            // Application.Run(new LoginForm());

            // Temporary development startup with test user:
            try
            {
                var testUser = GetTestUser();
                Application.Run(new DashboardForm(testUser));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Application startup error: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        // Get or create test user for development purposes
        // This method is temporary for testing Member 1's components
        // Will be removed once LoginForm is implemented by Member 2
        private static Models.User GetTestUser()
        {
            using (var context = new HealthTrackerContext())
            {
                // Check if test user already exists
                var existingUser = context.Users
                    .FirstOrDefault(u => u.Username == "testuser");

                if (existingUser != null)
                {
                    return existingUser;
                }

                // Create new test user
                var testUser = new Models.User
                {
                    Username = "testuser",
                    Password = "test123",
                    Email = "test@healthtracker.com",
                    DateOfBirth = new DateTime(1990, 1, 1),
                    Height = 1.75, // 175cm in metres
                    Gender = "Male",
                    CreatedDate = DateTime.Now
                };

                context.Users.Add(testUser);
                context.SaveChanges();

                return testUser;
            }
        }
    }
}