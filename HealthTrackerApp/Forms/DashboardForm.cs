using System;
using System.Linq;
using System.Windows.Forms;
using HealthTrackerApp.Data;
using HealthTrackerApp.Models;
using HealthTrackerApp.Utilities;

namespace HealthTrackerApp.Forms
{
    // Main dashboard form - central hub of the application
    // Displays user overview including BMI, daily calories, and quick stats
    public partial class DashboardForm : Form
    {
        private readonly HealthTrackerContext _context;
        private readonly User _currentUser;

        public DashboardForm(User user)
        {
            InitializeComponent();
            _currentUser = user;
            _context = new HealthTrackerContext();

            // Load dashboard data when form loads
            this.Load += DashboardForm_Load;
        }

        private void DashboardForm_Load(object sender, EventArgs e)
        {
            // Display user welcome message
            lblWelcome.Text = $"Welcome back, {_currentUser.Username}!";

            // Load and display current statistics
            LoadBMIStatus();
            LoadDailySummary();
            LoadWeeklySummary();
        }

        // Load and display current BMI status with colour-coded category
        private void LoadBMIStatus()
        {
            // Get latest weight record from database
            var latestWeight = _context.HealthMetrics
                .Where(h => h.UserId == _currentUser.UserId && h.MetricType == "Weight")
                .OrderByDescending(h => h.RecordedDate)
                .FirstOrDefault();

            if (latestWeight != null && latestWeight.WeightKg.HasValue)
            {
                // Calculate BMI using user's height and latest weight
                double heightInMetres = _currentUser.Height;
                double weightKg = latestWeight.WeightKg.Value;
                double bmi = weightKg / (heightInMetres * heightInMetres);

                // Display BMI value
                lblBMIValue.Text = $"{bmi:F1}";

                // Determine BMI category and set appropriate colour
                string category;
                System.Drawing.Color categoryColour;

                if (bmi < Constants.BMI_UNDERWEIGHT)
                {
                    category = "Underweight";
                    categoryColour = System.Drawing.Color.Blue;
                }
                else if (bmi < Constants.BMI_NORMAL)
                {
                    category = "Normal";
                    categoryColour = System.Drawing.Color.Green;
                }
                else if (bmi < Constants.BMI_OVERWEIGHT)
                {
                    category = "Overweight";
                    categoryColour = System.Drawing.Color.Orange;
                }
                else
                {
                    category = "Obese";
                    categoryColour = System.Drawing.Color.Red;
                }

                lblBMICategory.Text = category;
                lblBMICategory.ForeColor = categoryColour;
                lblCurrentWeight.Text = $"Current Weight: {weightKg:F1} kg";
            }
            else
            {
                // No weight data available
                lblBMIValue.Text = "N/A";
                lblBMICategory.Text = "No data";
                lblCurrentWeight.Text = "Please log your weight";
            }
        }

        // Load today's calorie intake summary
        private void LoadDailySummary()
        {
            DateTime today = DateTime.Today;

            // Get today's meals and calculate total calories
            var todaysMeals = _context.Meals
                .Where(m => m.UserId == _currentUser.UserId && m.MealDate.Date == today)
                .ToList();

            double totalCalories = todaysMeals.Sum(m => m.TotalCalories);
            double totalProtein = todaysMeals.Sum(m => m.Protein);
            double totalCarbs = todaysMeals.Sum(m => m.Carbs);
            double totalFats = todaysMeals.Sum(m => m.Fats);

            // Get today's exercises and calculate calories burned
            var todaysExercises = _context.Exercises
                .Where(e => e.UserId == _currentUser.UserId && e.ExerciseDate.Date == today)
                .ToList();

            double caloriesBurned = todaysExercises.Sum(e => e.CaloriesBurned);

            // Display daily summary
            lblCaloriesConsumed.Text = $"{totalCalories:F0} kcal";
            lblCaloriesBurned.Text = $"{caloriesBurned:F0} kcal";

            // Calculate net calories (consumed - burned)
            double netCalories = totalCalories - caloriesBurned;
            lblNetCalories.Text = $"{netCalories:F0} kcal";

            // Display macronutrient breakdown
            lblProtein.Text = $"Protein: {totalProtein:F1}g";
            lblCarbs.Text = $"Carbs: {totalCarbs:F1}g";
            lblFats.Text = $"Fats: {totalFats:F1}g";

            // Calculate progress towards daily target
            double targetCalories = Constants.DEFAULT_CALORIE_TARGET;
            double progress = (totalCalories / targetCalories) * 100;

            // Update progress bar (max 100%)
            if (progress > 100)
                progress = 100;

            progressBarCalories.Value = (int)progress;
            lblCalorieProgress.Text = $"{progress:F0}% of daily target";
        }

        // Load weekly summary statistics
        private void LoadWeeklySummary()
        {
            DateTime weekStart = DateTime.Today.AddDays(-7);

            // Get meals from last 7 days
            var weeklyMeals = _context.Meals
                .Where(m => m.UserId == _currentUser.UserId && m.MealDate >= weekStart)
                .ToList();

            // Get exercises from last 7 days
            var weeklyExercises = _context.Exercises
                .Where(e => e.UserId == _currentUser.UserId && e.ExerciseDate >= weekStart)
                .ToList();

            // Calculate weekly statistics
            int mealsLogged = weeklyMeals.Count;
            int workoutsCompleted = weeklyExercises.Count;
            double avgDailyCalories = weeklyMeals.Any() ? weeklyMeals.Average(m => m.TotalCalories) : 0;

            // Display weekly summary
            lblWeeklyMeals.Text = $"Meals Logged: {mealsLogged}";
            lblWeeklyWorkouts.Text = $"Workouts: {workoutsCompleted}";
            lblAvgCalories.Text = $"Avg Calories: {avgDailyCalories:F0} kcal/day";
        }

        // Button click handlers for navigation to other forms
        private void btnMealTracker_Click(object sender, EventArgs e)
        {
            MealTrackerForm mealTracker = new MealTrackerForm(_currentUser);
            mealTracker.ShowDialog();

            // Refresh dashboard after returning
            LoadDailySummary();
            LoadWeeklySummary();
        }

        private void btnExerciseLogger_Click(object sender, EventArgs e)
        {
            // Open Exercise Logger form
            // Implementation will be done by Member 3
            MessageBox.Show("Exercise Logger - To be implemented by Member 3", "Navigation");
        }

        private void btnHealthMetrics_Click(object sender, EventArgs e)
        {
            // Open Health Metrics form
            HealthMetricsForm healthMetricsForm = new HealthMetricsForm(_currentUser);
            healthMetricsForm.ShowDialog();

            // Refresh dashboard after returning from Health Metrics
            LoadBMIStatus();
            LoadDailySummary();
        }

        private void btnGoals_Click(object sender, EventArgs e)
        {
            // Open Goals Progress form
            // Implementation will be done by Member 3
            MessageBox.Show("Goals Progress - To be implemented by Member 3", "Navigation");
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            ReportsAnalyticsForm reports = new ReportsAnalyticsForm(_currentUser);
            reports.ShowDialog();
        }

        // Logout and return to login screen
        private void btnLogout_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to logout?",
                "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Close();

                // Show login form again
                LoginForm loginForm = new LoginForm();
                loginForm.Show();
            }
        }

        // Clean up database context when form closes
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            _context?.Dispose();
        }
    }
}