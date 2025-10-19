using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using HealthTrackerApp.Data;
using HealthTrackerApp.Models;
using HealthTrackerApp.Models.Enums;
using HealthTrackerApp.Services;
using Microsoft.EntityFrameworkCore;

namespace HealthTrackerApp.Forms
{
    // Meal tracking form for searching foods via USDA API and logging meals
    // Handles food search, portion calculation, and meal history
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
            // Populate meal type combo box with enum values
            cboMealType.Items.AddRange(new string[] { "Breakfast", "Lunch", "Dinner", "Snack" });
            cboMealType.SelectedIndex = 2;

            // Set default date to current date and time
            dtpMealDate.Value = DateTime.Now;
            txtMealName.Text = $"{cboMealType.SelectedItem} - {DateTime.Now:dd/MM/yyyy}";

            // Update meal name when meal type changes
            cboMealType.SelectedIndexChanged += (s, args) =>
            {
                txtMealName.Text = $"{cboMealType.SelectedItem} - {dtpMealDate.Value:dd/MM/yyyy}";
            };

            // Wire up button events
            btnSearch.Click += btnSearch_Click;
            btnAddFood.Click += btnAddFood_Click;
            btnSaveMeal.Click += btnSaveMeal_Click;
            btnDeleteMeal.Click += btnDeleteMeal_Click;
            dgvSearchResults.SelectionChanged += dgvSearchResults_SelectionChanged;
            dgvMealHistory.SelectionChanged += dgvMealHistory_SelectionChanged;

            LoadTodaysMeals();
            LoadMealHistory();
        }

        // Search foods using USDA API
        private async void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string query = txtFoodSearch.Text.Trim();
                if (string.IsNullOrEmpty(query))
                {
                    MessageBox.Show("Please enter a food name to search", "Validation Error");
                    return;
                }

                // Show loading message to user
                lblSearchStatus.Text = "Searching...";
                btnSearch.Enabled = false;

                // Call USDA API service to get food results
                _searchResults = await _usdaService.SearchFoodsAsync(query, 20);

                if (_searchResults.Count == 0)
                {
                    MessageBox.Show("No foods found. Try different search terms.", "No Results");
                    lblSearchStatus.Text = "No results";
                    return;
                }

                // Populate search results DataGridView
                dgvSearchResults.Rows.Clear();
                if (dgvSearchResults.Columns.Count == 0)
                {
                    dgvSearchResults.Columns.Add("FoodName", "Food Name");
                    dgvSearchResults.Columns.Add("Calories", "Calories/100g");
                    dgvSearchResults.Columns.Add("Protein", "Protein/100g");
                    dgvSearchResults.Columns.Add("Carbs", "Carbs/100g");
                    dgvSearchResults.Columns.Add("Fats", "Fats/100g");
                }

                foreach (var food in _searchResults)
                {
                    dgvSearchResults.Rows.Add(
                        food.FoodName,
                        $"{food.CaloriesPer100g:F0}",
                        $"{food.ProteinPer100g:F1}g",
                        $"{food.CarbsPer100g:F1}g",
                        $"{food.FatsPer100g:F1}g"
                    );
                }

                lblSearchStatus.Text = $"Found {_searchResults.Count} foods";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Search error: {ex.Message}", "Error");
                lblSearchStatus.Text = "Search failed";
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
                _selectedFood = _searchResults[selectedIndex];
                lblSelectedFood.Text = $"Selected: {_selectedFood.FoodName}";
            }
        }

        // Add selected food to current meal
        private void btnAddFood_Click(object sender, EventArgs e)
        {
            if (_selectedFood == null)
            {
                MessageBox.Show("Please select a food from search results", "Validation Error");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPortionSize.Text))
            {
                MessageBox.Show("Please enter portion size in grammes", "Validation Error");
                return;
            }

            if (!double.TryParse(txtPortionSize.Text, out double portionSize) || portionSize <= 0)
            {
                MessageBox.Show("Please enter a valid portion size", "Validation Error");
                return;
            }

            try
            {
                // Check if food already exists in database to avoid duplicates
                // Using AsNoTracking to prevent entity tracking issues when querying
                var existingFood = _context.Foods
                    .AsNoTracking()
                    .FirstOrDefault(f => f.FdcId == _selectedFood.FdcId);

                Food foodToUse;
                if (existingFood == null)
                {
                    // Food doesn't exist in database yet, need to add it first
                    // Create new Food object with all properties from selected food
                    // IMPORTANT: Set default values for nullable fields to avoid NOT NULL constraint errors
                    foodToUse = new Food
                    {
                        FdcId = _selectedFood.FdcId,
                        FoodName = _selectedFood.FoodName ?? "Unknown Food",
                        DataType = !string.IsNullOrWhiteSpace(_selectedFood.DataType)
                            ? _selectedFood.DataType
                            : "Unknown",
                        Category = !string.IsNullOrWhiteSpace(_selectedFood.Category)
                            ? _selectedFood.Category
                            : "Uncategorised",
                        CaloriesPer100g = _selectedFood.CaloriesPer100g,
                        ProteinPer100g = _selectedFood.ProteinPer100g,
                        CarbsPer100g = _selectedFood.CarbsPer100g,
                        FatsPer100g = _selectedFood.FatsPer100g,
                        ServingSize = _selectedFood.ServingSize,
                        ServingUnit = !string.IsNullOrWhiteSpace(_selectedFood.ServingUnit)
                            ? _selectedFood.ServingUnit
                            : "g",
                        HouseholdServing = !string.IsNullOrWhiteSpace(_selectedFood.HouseholdServing)
                            ? _selectedFood.HouseholdServing
                            : "1 serving",
                        IsCustom = false,
                        DateAdded = DateTime.Now
                    };

                    _context.Foods.Add(foodToUse);
                    _context.SaveChanges();

                    // Reload food from database to get the generated FoodId
                    foodToUse = _context.Foods.First(f => f.FdcId == _selectedFood.FdcId);
                }
                else
                {
                    // Food already exists, use the existing one
                    foodToUse = existingFood;
                }

                // Create meal if this is first food being added
                if (_currentMeal == null)
                {
                    var mealType = (MealType)Enum.Parse(typeof(MealType), cboMealType.SelectedItem.ToString());
                    _currentMeal = _mealService.CreateMeal(
                        _currentUser.UserId,
                        dtpMealDate.Value,
                        mealType,
                        txtMealName.Text
                    );
                }

                // Add food to meal with calculated nutrition based on portion
                _mealService.AddFoodToMeal(_currentMeal.MealId, foodToUse, portionSize, "g");

                // Refresh current meal display to show updated totals
                LoadCurrentMeal();

                // Clear input fields for next entry
                txtPortionSize.Clear();
                lblSelectedFood.Text = "Selected: None";

                MessageBox.Show("Food added to meal!", "Success");
            }
            catch (Exception ex)
            {
                // Show detailed error message including inner exception for troubleshooting
                string errorMessage = $"Error adding food: {ex.Message}";
                if (ex.InnerException != null)
                {
                    errorMessage += $"\n\nInner exception: {ex.InnerException.Message}";
                }
                MessageBox.Show(errorMessage, "Error");
            }
        }

        // Load current meal foods into DataGridView
        private void LoadCurrentMeal()
        {
            if (_currentMeal == null) return;

            // Detach current meal entity to avoid tracking conflicts when reloading
            _context.Entry(_currentMeal).State = Microsoft.EntityFrameworkCore.EntityState.Detached;

            // Reload meal from database to get updated nutrition totals
            _currentMeal = _context.Meals.Find(_currentMeal.MealId);

            if (_currentMeal == null) return;

            // Setup DataGridView columns if not already created
            dgvCurrentMeal.Rows.Clear();
            if (dgvCurrentMeal.Columns.Count == 0)
            {
                dgvCurrentMeal.Columns.Add("FoodName", "Food");
                dgvCurrentMeal.Columns.Add("Portion", "Portion");
                dgvCurrentMeal.Columns.Add("Calories", "Calories");
                dgvCurrentMeal.Columns.Add("Protein", "Protein");
                dgvCurrentMeal.Columns.Add("Carbs", "Carbs");
                dgvCurrentMeal.Columns.Add("Fats", "Fats");
            }

            // Get all foods in current meal from database
            var mealFoods = _mealService.GetMealFoods(_currentMeal.MealId);
            foreach (var mf in mealFoods)
            {
                // Use AsNoTracking for read-only food lookup to avoid tracking issues
                var food = _context.Foods
                    .AsNoTracking()
                    .FirstOrDefault(f => f.FoodId == mf.FoodId);

                if (food != null)
                {
                    dgvCurrentMeal.Rows.Add(
                        food.FoodName,
                        $"{mf.PortionSize:F0}g",
                        $"{mf.Calories:F0}",
                        $"{mf.Protein:F1}g",
                        $"{mf.Carbs:F1}g",
                        $"{mf.Fats:F1}g"
                    );
                }
            }

            // Display meal nutrition summary
            lblMealSummary.Text = $"Total: {_currentMeal.TotalCalories:F0} kcal | " +
                                 $"Protein: {_currentMeal.Protein:F1}g | " +
                                 $"Carbs: {_currentMeal.Carbs:F1}g | " +
                                 $"Fats: {_currentMeal.Fats:F1}g";
        }

        // Save and finalise current meal
        private void btnSaveMeal_Click(object sender, EventArgs e)
        {
            if (_currentMeal == null)
            {
                MessageBox.Show("No meal to save. Add foods first.", "Validation Error");
                return;
            }

            MessageBox.Show("Meal saved successfully!", "Success");

            // Reset form for new meal entry
            _currentMeal = null;
            dgvCurrentMeal.Rows.Clear();
            lblMealSummary.Text = "Total: 0 kcal";
            txtMealName.Text = $"{cboMealType.SelectedItem} - {DateTime.Now:dd/MM/yyyy}";

            // Refresh today's summary and history
            LoadTodaysMeals();
            LoadMealHistory();
        }

        // Load today's meals summary
        private void LoadTodaysMeals()
        {
            var todayMeals = _mealService.GetMealsByDate(_currentUser.UserId, DateTime.Today);
            double totalCalories = todayMeals.Sum(m => m.TotalCalories);

            lblTodaySummary.Text = $"Today's Total: {totalCalories:F0} kcal from {todayMeals.Count} meals";
        }

        // Load meal history into DataGridView
        private void LoadMealHistory()
        {
            dgvMealHistory.Rows.Clear();
            if (dgvMealHistory.Columns.Count == 0)
            {
                dgvMealHistory.Columns.Add("Date", "Date");
                dgvMealHistory.Columns.Add("Type", "Type");
                dgvMealHistory.Columns.Add("Name", "Name");
                dgvMealHistory.Columns.Add("Calories", "Calories");
            }

            // Get last 20 meals for display
            var meals = _mealService.GetUserMeals(_currentUser.UserId).Take(20).ToList();
            foreach (var meal in meals)
            {
                dgvMealHistory.Rows.Add(
                    meal.MealDate.ToString("dd/MM/yyyy HH:mm"),
                    meal.MealType.ToString(),
                    meal.MealName,
                    $"{meal.TotalCalories:F0}"
                );
            }
        }

        // Delete selected meal from history
        private void btnDeleteMeal_Click(object sender, EventArgs e)
        {
            if (dgvMealHistory.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a meal to delete", "Validation Error");
                return;
            }

            var result = MessageBox.Show("Delete this meal?", "Confirm Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    int selectedIndex = dgvMealHistory.SelectedRows[0].Index;
                    var meals = _mealService.GetUserMeals(_currentUser.UserId).Take(20).ToList();

                    if (selectedIndex < meals.Count)
                    {
                        var mealToDelete = meals[selectedIndex];

                        // Clear entity tracker before delete to prevent conflicts
                        _context.ChangeTracker.Clear();
                        _mealService.DeleteMeal(mealToDelete.MealId);

                        // Refresh displays after deletion
                        LoadMealHistory();
                        LoadTodaysMeals();

                        MessageBox.Show("Meal deleted", "Success");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting meal: {ex.Message}\n\nInner exception: {ex.InnerException?.Message}", "Error");
                }
            }
        }

        // View meal details when selected in history
        private void dgvMealHistory_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvMealHistory.SelectedRows.Count > 0)
            {
                int selectedIndex = dgvMealHistory.SelectedRows[0].Index;
                var meals = _mealService.GetUserMeals(_currentUser.UserId).Take(20).ToList();

                if (selectedIndex < meals.Count)
                {
                    var selectedMeal = meals[selectedIndex];
                    var mealFoods = _mealService.GetMealFoods(selectedMeal.MealId);
                    lblMealDetails.Text = $"Foods in meal: {mealFoods.Count}";
                }
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