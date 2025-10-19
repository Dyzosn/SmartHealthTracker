using System;
using System.Linq;
using System.Windows.Forms;
using HealthTrackerApp.Data;
using HealthTrackerApp.Models;
using HealthTrackerApp.Services;

namespace HealthTrackerApp.Forms
{
    // Main dashboard form showing health summary and navigation
    // Displays BMI status, daily nutrition, weekly statistics, and navigation buttons
    public partial class DashboardForm : Form
    {
        private readonly User _currentUser;
        private readonly HealthTrackerContext _context;
        private readonly HealthMetricService _healthMetricService;
        private readonly LoginForm _loginForm;

        // Constructor accepts user and login form reference for logout functionality
        public DashboardForm(User user, LoginForm loginForm)
        {
            InitializeComponent();
            _currentUser = user;
            _loginForm = loginForm;
            _context = new HealthTrackerContext();
            _healthMetricService = new HealthMetricService(_context);

            this.Load += DashboardForm_Load;
        }

        private void DashboardForm_Load(object sender, EventArgs e)
        {
            // Display welcome message with username
            lblWelcome.Text = $"Welcome, {_currentUser.Username}!";

            // Load all dashboard data
            LoadBMIStatus();
            LoadDailySummary();
            LoadWeeklySummary();
        }

        // Calculate and display current BMI status with colour coding
        private void LoadBMIStatus()
        {
            // Get latest weight record from database
            var latestWeight = _healthMetricService.GetLatestHealthMetric(_currentUser.UserId, "Weight");

            if (latestWeight != null && latestWeight.WeightKg.HasValue)
            {
                double weight = latestWeight.WeightKg.Value;
                double height = _currentUser.Height;

                // Calculate BMI using calculator class for consistent validation
                var bmiCalculator = new BusinessLogic.Calculators.BMICalculator
                {
                    WeightKg = weight,
                    HeightM = height
                };

                double bmi = bmiCalculator.Calculate();
                string category = bmiCalculator.GetCategory();
                string colourName = bmiCalculator.GetCategoryColour();

                // Map category colour name to display colour
                System.Drawing.Color categoryColour;
                switch (colourName)
                {
                    case "Blue":
                        categoryColour = System.Drawing.Color.Blue;
                        break;
                    case "Green":
                        categoryColour = System.Drawing.Color.Green;
                        break;
                    case "Orange":
                        categoryColour = System.Drawing.Color.Orange;
                        break;
                    case "Red":
                        categoryColour = System.Drawing.Color.Red;
                        break;
                    default:
                        categoryColour = System.Drawing.Color.Black;
                        break;
                }

                // Display BMI value and category with appropriate colour
                lblBMIValue.Text = $"BMI: {bmi:F1}";
                lblBMICategory.Text = category;
                lblBMICategory.ForeColor = categoryColour;
                lblCurrentWeight.Text = $"Current Weight: {weight:F1} kg";
            }
            else
            {
                // No weight data available
                lblBMIValue.Text = "BMI: N/A";
                lblBMICategory.Text = "No weight data recorded";
                lblBMICategory.ForeColor = System.Drawing.Color.Gray;
                lblCurrentWeight.Text = "Weight: N/A";
            }
        }

        // Load today's nutrition summary and calorie progress
        private void LoadDailySummary()
        {
            DateTime today = DateTime.Today;

            // Get all meals logged today using LINQ
            var todayMeals = _context.Meals
                .Where(m => m.UserId == _currentUser.UserId && m.MealDate.Date == today)
                .ToList();

            // Get all exercises logged today using LINQ
            var todayExercises = _context.Exercises
                .Where(e => e.UserId == _currentUser.UserId && e.ExerciseDate.Date == today)
                .ToList();

            // Calculate calorie totals
            double caloriesConsumed = todayMeals.Sum(m => m.TotalCalories);
            double caloriesBurned = todayExercises.Sum(e => e.CaloriesBurned);
            double netCalories = caloriesConsumed - caloriesBurned;

            // Calculate macronutrient totals
            double protein = todayMeals.Sum(m => m.Protein);
            double carbs = todayMeals.Sum(m => m.Carbs);
            double fats = todayMeals.Sum(m => m.Fats);

            // Display calorie information
            lblCaloriesConsumed.Text = $"Consumed: {caloriesConsumed:F0} kcal";
            lblCaloriesBurned.Text = $"Burned: {caloriesBurned:F0} kcal";
            lblNetCalories.Text = $"Net: {netCalories:F0} kcal";

            // Display macronutrient breakdown
            lblProtein.Text = $"Protein: {protein:F0}g";
            lblCarbs.Text = $"Carbs: {carbs:F0}g";
            lblFats.Text = $"Fats: {fats:F0}g";

            // Update progress bar for daily calorie target
            double targetCalories = 2000;
            double progress = (caloriesConsumed / targetCalories) * 100;
            if (progress > 100) progress = 100;

            progressBarCalories.Value = (int)progress;
            lblCalorieProgress.Text = $"{caloriesConsumed:F0} / {targetCalories:F0} kcal";
        }

        // Load weekly summary statistics
        private void LoadWeeklySummary()
        {
            // Calculate start and end of current week
            DateTime startOfWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
            DateTime endOfWeek = startOfWeek.AddDays(7);

            // Get all meals this week using LINQ
            var weeklyMeals = _context.Meals
                .Where(m => m.UserId == _currentUser.UserId &&
                           m.MealDate >= startOfWeek &&
                           m.MealDate < endOfWeek)
                .ToList();

            // Get all exercises this week using LINQ
            var weeklyExercises = _context.Exercises
                .Where(e => e.UserId == _currentUser.UserId &&
                           e.ExerciseDate >= startOfWeek &&
                           e.ExerciseDate < endOfWeek)
                .ToList();

            // Calculate weekly statistics
            int mealsLogged = weeklyMeals.Count;
            int workoutsCompleted = weeklyExercises.Count;
            double avgDailyCalories = weeklyMeals.Count > 0 ?
                weeklyMeals.Average(m => m.TotalCalories) : 0;

            // Display weekly summary
            lblWeeklyMeals.Text = $"Meals Logged: {mealsLogged}";
            lblWeeklyWorkouts.Text = $"Workouts: {workoutsCompleted}";
            lblAvgCalories.Text = $"Avg Calories: {avgDailyCalories:F0} kcal/day";
        }

        // Button click handlers for navigation to other forms
        private void btnMealTracker_Click(object sender, EventArgs e)
        {
            // Open Meal Tracker form as modal dialogue
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
            // Open Health Metrics form as modal dialogue
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
            // Open Reports & Analytics form as modal dialogue
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
                // Hide dashboard first to prevent visual flicker
                this.Hide();

                // Show login form again for next user
                _loginForm.ShowLoginAgain();

                // Close dashboard now that login form is visible
                this.Close();
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