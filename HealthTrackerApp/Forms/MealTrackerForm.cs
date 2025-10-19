using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using HealthTrackerApp.Data;
using HealthTrackerApp.Models;
using HealthTrackerApp.Models.Enums;
using HealthTrackerApp.Services;

namespace HealthTrackerApp.Forms
{
    // Meal tracking form for logging food intake and viewing nutrition data
    // Integrates with USDA API for food search and nutrition information
    public partial class MealTrackerForm : Form
    {
        private readonly HealthTrackerContext _context;
        private readonly MealService _mealService;
        private readonly USDAFoodDataService _usdaService;
        private readonly User _currentUser;
        private List<Food> _searchResults;
        private Food _selectedFood;
        private Meal _currentMeal;

        public MealTrackerForm(User user)
        {
            InitializeComponent();
            _currentUser = user;
            _context = new HealthTrackerContext();
            _mealService = new MealService(_context);
            _usdaService = new USDAFoodDataService();

            this.Load += MealTrackerForm_Load;
        }

        private void MealTrackerForm_Load(object sender, EventArgs e)
        {
            // Populate meal type dropdown
            cboMealType.Items.AddRange(new string[] { "Breakfast", "Lunch", "Dinner", "Snack" });
            cboMealType.SelectedIndex = 0;

            // Set default meal name based on current time
            txtMealName.Text = GetDefaultMealName();

            // Load existing data
            LoadTodaysMeal();
            LoadMealHistory();

            // Wire up event handlers
            btnSearch.Click += btnSearch_Click;
            btnAddFood.Click += btnAddFood_Click;
            btnSaveMeal.Click += btnSaveMeal_Click;
            dgvSearchResults.SelectionChanged += dgvSearchResults_SelectionChanged;
        }

        // Get default meal name based on time of day
        private string GetDefaultMealName()
        {
            int hour = DateTime.Now.Hour;
            if (hour < 11) return "Morning Meal";
            if (hour < 15) return "Lunch";
            if (hour < 19) return "Dinner";
            return "Evening Snack";
        }

        // Search foods using USDA API
        private async void btnSearch_Click(object sender, EventArgs e)
        {
            string query = txtFoodSearch.Text.Trim();
            if (string.IsNullOrEmpty(query))
            {
                MessageBox.Show("Please enter a food name to search", "Validation");
                return;
            }

            try
            {
                lblSearchStatus.Text = "Searching...";
                btnSearch.Enabled = false;
                dgvSearchResults.DataSource = null;

                // Call USDA API service
                _searchResults = await _usdaService.SearchFoodsAsync(query, 20);

                if (_searchResults.Count == 0)
                {
                    lblSearchStatus.Text = "No foods found";
                    MessageBox.Show("No foods found. Try different search terms.", "No Results");
                    return;
                }

                // Display results in DataGridView
                var displayResults = _searchResults.Select(f => new
                {
                    f.FoodName,
                    Calories = $"{f.CaloriesPer100g:F0}",
                    Protein = $"{f.ProteinPer100g:F1}g",
                    Carbs = $"{f.CarbsPer100g:F1}g",
                    Fats = $"{f.FatsPer100g:F1}g",
                    f.Category
                }).ToList();

                dgvSearchResults.DataSource = displayResults;
                lblSearchStatus.Text = $"{_searchResults.Count} foods found";
            }
            catch (Exception ex)
            {
                lblSearchStatus.Text = "Search failed";
                MessageBox.Show($"Search error: {ex.Message}", "Error");
            }
            finally
            {
                btnSearch.Enabled = true;
            }
        }

        // Handle food selection from search results
        private void dgvSearchResults_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSearchResults.SelectedRows.Count > 0)
            {
                int selectedIndex = dgvSearchResults.SelectedRows[0].Index;
                if (selectedIndex >= 0 && selectedIndex < _searchResults.Count)
                {
                    _selectedFood = _searchResults[selectedIndex];
                    lblSelectedFood.Text = $"Selected: {_selectedFood.FoodName}";
                    txtPortionSize.Focus();
                }
            }
        }

        // Add selected food to current meal
        private void btnAddFood_Click(object sender, EventArgs e)
        {
            if (_selectedFood == null)
            {
                MessageBox.Show("Please select a food from search results", "Validation");
                return;
            }

            if (!double.TryParse(txtPortionSize.Text, out double portionSize) || portionSize <= 0)
            {
                MessageBox.Show("Please enter valid portion size in grammes", "Validation");
                txtPortionSize.Focus();
                return;
            }

            // Create meal if not exists
            if (_currentMeal == null)
            {
                MealType mealType = (MealType)Enum.Parse(typeof(MealType), cboMealType.SelectedItem.ToString());
                _currentMeal = _mealService.CreateMeal(_currentUser.UserId, DateTime.Now, mealType, txtMealName.Text);
            }

            // Add food to meal with portion calculation
            _mealService.AddFoodToMeal(_currentMeal.MealId, _selectedFood, portionSize, "g");

            // Refresh display
            LoadTodaysMeal();
            txtPortionSize.Clear();
            lblSelectedFood.Text = "Food added successfully";
        }

        // Save meal and close form
        private void btnSaveMeal_Click(object sender, EventArgs e)
        {
            if (_currentMeal == null)
            {
                MessageBox.Show("No meal to save. Add foods first.", "Information");
                return;
            }

            MessageBox.Show($"Meal '{_currentMeal.MealName}' saved with {_currentMeal.TotalCalories:F0} calories",
                "Meal Saved");
            this.Close();
        }

        // Load today's meal summary
        private void LoadTodaysMeal()
        {
            var todaysMeals = _mealService.GetTodaysMeals(_currentUser.UserId);

            // Calculate daily totals using LINQ
            double totalCalories = todaysMeals.Sum(m => m.TotalCalories);
            double totalProtein = todaysMeals.Sum(m => m.Protein);
            double totalCarbs = todaysMeals.Sum(m => m.Carbs);
            double totalFats = todaysMeals.Sum(m => m.Fats);

            // Display summary
            lblTodaySummary.Text = $"Today's Total: {totalCalories:F0} kcal | " +
                                   $"Protein: {totalProtein:F1}g | " +
                                   $"Carbs: {totalCarbs:F1}g | " +
                                   $"Fats: {totalFats:F1}g";

            // Display current meal foods if exists
            if (_currentMeal != null)
            {
                var currentMealRefreshed = _mealService.GetMealById(_currentMeal.MealId);
                if (currentMealRefreshed != null)
                {
                    _currentMeal = currentMealRefreshed;
                    lblCurrentMeal.Text = $"Current Meal: {_currentMeal.TotalCalories:F0} kcal | " +
                                          $"P: {_currentMeal.Protein:F1}g | " +
                                          $"C: {_currentMeal.Carbs:F1}g | " +
                                          $"F: {_currentMeal.Fats:F1}g";

                    var mealFoods = _mealService.GetMealFoods(_currentMeal.MealId);
                    var foodDisplay = mealFoods.Select(mf =>
                    {
                        var food = _context.Foods.Find(mf.FoodId);
                        return new
                        {
                            Food = food?.FoodName ?? "Unknown",
                            Portion = $"{mf.PortionSize:F0}g",
                            Calories = $"{mf.Calories:F0}",
                            Protein = $"{mf.Protein:F1}g",
                            Carbs = $"{mf.Carbs:F1}g",
                            Fats = $"{mf.Fats:F1}g"
                        };
                    }).ToList();

                    dgvCurrentMealFoods.DataSource = foodDisplay;
                }
            }
        }

        // Load meal history
        private void LoadMealHistory()
        {
            var meals = _mealService.GetUserMeals(_currentUser.UserId).Take(20);

            var mealDisplay = meals.Select(m => new
            {
                Date = m.MealDate.ToString("dd/MM/yyyy HH:mm"),
                Type = m.MealType.ToString(),
                Name = m.MealName,
                Calories = $"{m.TotalCalories:F0}",
                Protein = $"{m.Protein:F1}g",
                Carbs = $"{m.Carbs:F1}g",
                Fats = $"{m.Fats:F1}g"
            }).ToList();

            dgvMealHistory.DataSource = mealDisplay;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            _context?.Dispose();
        }
    }
}