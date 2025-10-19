using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using HealthTrackerApp.Data;
using HealthTrackerApp.Models;
using HealthTrackerApp.Services;

namespace HealthTrackerApp.Forms
{
    // Reports and analytics form for statistical analysis of nutrition data
    // Displays meal summaries, trends, and top foods using DataGridView
    public partial class ReportsAnalyticsForm : Form
    {
        private readonly HealthTrackerContext _context;
        private readonly MealService _mealService;
        private readonly User _currentUser;

        public ReportsAnalyticsForm(User user)
        {
            InitializeComponent();
            _currentUser = user;
            _context = new HealthTrackerContext();
            _mealService = new MealService(_context);

            this.Load += ReportsAnalyticsForm_Load;
        }

        private void ReportsAnalyticsForm_Load(object sender, EventArgs e)
        {
            // Set default date range to last 30 days
            dtpStartDate.Value = DateTime.Today.AddDays(-30);
            dtpEndDate.Value = DateTime.Today;

            btnGenerateReport.Click += btnGenerateReport_Click;

            GenerateReport();
        }

        // Generate statistical report for selected date range
        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            GenerateReport();
        }

        private void GenerateReport()
        {
            try
            {
                DateTime startDate = dtpStartDate.Value.Date;
                DateTime endDate = dtpEndDate.Value.Date;

                if (startDate > endDate)
                {
                    MessageBox.Show("Start date must be before end date", "Validation Error");
                    return;
                }

                // Get meals for date range
                var meals = _mealService.GetMealsByDateRange(_currentUser.UserId, startDate, endDate);

                if (meals.Count == 0)
                {
                    MessageBox.Show("No meals found for selected date range", "No Data");
                    return;
                }

                // Calculate summary statistics using LINQ
                int totalMeals = meals.Count;
                double totalCalories = meals.Sum(m => m.TotalCalories);
                double avgCalories = meals.Average(m => m.TotalCalories);
                double totalProtein = meals.Sum(m => m.Protein);
                double totalCarbs = meals.Sum(m => m.Carbs);
                double totalFats = meals.Sum(m => m.Fats);

                int dayCount = (endDate - startDate).Days + 1;
                double avgCaloriesPerDay = totalCalories / dayCount;

                // Display summary statistics
                lblSummaryStats.Text = $"Period: {startDate:dd/MM/yyyy} to {endDate:dd/MM/yyyy}\n" +
                                      $"Total Meals: {totalMeals}\n" +
                                      $"Total Calories: {totalCalories:F0} kcal\n" +
                                      $"Avg Calories/Day: {avgCaloriesPerDay:F0} kcal\n" +
                                      $"Avg Calories/Meal: {avgCalories:F0} kcal\n" +
                                      $"Total Protein: {totalProtein:F1}g\n" +
                                      $"Total Carbs: {totalCarbs:F1}g\n" +
                                      $"Total Fats: {totalFats:F1}g";

                // Daily breakdown analysis
                var dailyStats = meals
                    .GroupBy(m => m.MealDate.Date)
                    .Select(g => new
                    {
                        Date = g.Key,
                        MealCount = g.Count(),
                        TotalCalories = g.Sum(m => m.TotalCalories),
                        AvgCalories = g.Average(m => m.TotalCalories)
                    })
                    .OrderBy(x => x.Date)
                    .ToList();

                // Populate daily breakdown DataGridView
                dgvDailyBreakdown.Rows.Clear();
                if (dgvDailyBreakdown.Columns.Count == 0)
                {
                    dgvDailyBreakdown.Columns.Add("Date", "Date");
                    dgvDailyBreakdown.Columns.Add("Meals", "Meals");
                    dgvDailyBreakdown.Columns.Add("TotalCal", "Total Calories");
                    dgvDailyBreakdown.Columns.Add("AvgCal", "Avg Calories");
                }

                foreach (var day in dailyStats)
                {
                    dgvDailyBreakdown.Rows.Add(
                        day.Date.ToString("dd/MM/yyyy"),
                        day.MealCount,
                        $"{day.TotalCalories:F0}",
                        $"{day.AvgCalories:F0}"
                    );
                }

                // Meal type analysis
                var mealTypeStats = meals
                    .GroupBy(m => m.MealType)
                    .Select(g => new
                    {
                        MealType = g.Key.ToString(),
                        Count = g.Count(),
                        AvgCalories = g.Average(m => m.TotalCalories),
                        AvgProtein = g.Average(m => m.Protein)
                    })
                    .OrderByDescending(x => x.Count)
                    .ToList();

                // Populate meal type analysis DataGridView
                dgvMealTypeAnalysis.Rows.Clear();
                if (dgvMealTypeAnalysis.Columns.Count == 0)
                {
                    dgvMealTypeAnalysis.Columns.Add("Type", "Meal Type");
                    dgvMealTypeAnalysis.Columns.Add("Count", "Count");
                    dgvMealTypeAnalysis.Columns.Add("AvgCal", "Avg Calories");
                    dgvMealTypeAnalysis.Columns.Add("AvgProtein", "Avg Protein");
                }

                foreach (var type in mealTypeStats)
                {
                    dgvMealTypeAnalysis.Rows.Add(
                        type.MealType,
                        type.Count,
                        $"{type.AvgCalories:F0}",
                        $"{type.AvgProtein:F1}g"
                    );
                }

                // Top foods analysis
                var mealFoods = meals
                    .SelectMany(m => _context.MealFoods.Where(mf => mf.MealId == m.MealId))
                    .ToList();

                var topFoods = mealFoods
                    .GroupBy(mf => mf.FoodId)
                    .Select(g => new
                    {
                        FoodId = g.Key,
                        TimesLogged = g.Count(),
                        TotalCalories = g.Sum(mf => mf.Calories)
                    })
                    .OrderByDescending(x => x.TimesLogged)
                    .Take(10)
                    .ToList();

                // Populate top foods DataGridView
                dgvTopFoods.Rows.Clear();
                if (dgvTopFoods.Columns.Count == 0)
                {
                    dgvTopFoods.Columns.Add("Food", "Food Name");
                    dgvTopFoods.Columns.Add("Times", "Times Logged");
                    dgvTopFoods.Columns.Add("Calories", "Total Calories");
                }

                foreach (var food in topFoods)
                {
                    var foodEntity = _context.Foods.Find(food.FoodId);
                    dgvTopFoods.Rows.Add(
                        foodEntity.FoodName,
                        food.TimesLogged,
                        $"{food.TotalCalories:F0}"
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating report: {ex.Message}", "Error");
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            _context?.Dispose();
        }
    }
}