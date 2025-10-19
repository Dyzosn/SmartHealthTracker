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
    // Meal tracking form for logging food intake and nutrition
    // Integrates with USDA API for food search and nutrition data
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
            // Populate meal type combo box
            cboMealType.Items.AddRange(new string[] { "Breakfast", "Lunch", "Dinner", "Snack" });
            cboMealType.SelectedIndex = 0;

            // Set default meal name
            txtMealName.Text = $"{cboMealType.SelectedItem} - {DateTime.Now:dd/MM/yyyy}";

            LoadTodaysMeals();
            LoadMealHistory();

            // Wire up event handlers
            btnSearch.Click += btnSearch_Click;
            btnAddFood.Click += btnAddFood_Click;
            btnSaveMeal.Click += btnSaveMeal_Click;
            btnDeleteMeal.Click += btnDeleteMeal_Click;
            dgvSearchResults.SelectionChanged += dgvSearchResults_SelectionChanged;
            dgvMealHistory.SelectionChanged += dgvMealHistory_SelectionChanged;
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

                lblSearchStatus.Text = "Searching...";
                btnSearch.Enabled = false;
                dgvSearchResults.Rows.Clear();

                // Call USDA API service
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
                // Save food to database if not already saved
                var existingFood = _context.Foods.FirstOrDefault(f => f.FdcId == _selectedFood.FdcId);
                if (existingFood == null)
                {
                    _context.Foods.Add(_selectedFood);
                    _context.SaveChanges();
                }
                else
                {
                    _selectedFood = existingFood;
                }

                // Create meal if first food being added
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

                // Add food to meal
                _mealService.AddFoodToMeal(_currentMeal.MealId, _selectedFood, portionSize, "g");

                // Refresh current meal display
                LoadCurrentMeal();

                // Clear selections
                txtPortionSize.Clear();
                lblSelectedFood.Text = "Selected: None";

                MessageBox.Show("Food added to meal!", "Success");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding food: {ex.Message}", "Error");
            }
        }

        // Load current meal foods into DataGridView
        private void LoadCurrentMeal()
        {
            if (_currentMeal == null) return;

            // Refresh meal from database to get updated totals
            _currentMeal = _context.Meals.Find(_currentMeal.MealId);

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

            var mealFoods = _mealService.GetMealFoods(_currentMeal.MealId);
            foreach (var mf in mealFoods)
            {
                var food = _context.Foods.Find(mf.FoodId);
                dgvCurrentMeal.Rows.Add(
                    food.FoodName,
                    $"{mf.PortionSize:F0}g",
                    $"{mf.Calories:F0}",
                    $"{mf.Protein:F1}g",
                    $"{mf.Carbs:F1}g",
                    $"{mf.Fats:F1}g"
                );
            }

            // Update meal summary
            lblMealSummary.Text = $"Total: {_currentMeal.TotalCalories:F0} kcal | " +
                                 $"Protein: {_currentMeal.Protein:F1}g | " +
                                 $"Carbs: {_currentMeal.Carbs:F1}g | " +
                                 $"Fats: {_currentMeal.Fats:F1}g";
        }

        // Save and finalize current meal
        private void btnSaveMeal_Click(object sender, EventArgs e)
        {
            if (_currentMeal == null)
            {
                MessageBox.Show("No meal to save. Add foods first.", "Validation Error");
                return;
            }

            MessageBox.Show("Meal saved successfully!", "Success");

            // Reset for new meal
            _currentMeal = null;
            dgvCurrentMeal.Rows.Clear();
            lblMealSummary.Text = "Total: 0 kcal";
            txtMealName.Text = $"{cboMealType.SelectedItem} - {DateTime.Now:dd/MM/yyyy}";

            LoadTodaysMeals();
            LoadMealHistory();
        }

        // Load today's meals
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
                    var mealToDelete = meals[selectedIndex];

                    _mealService.DeleteMeal(mealToDelete.MealId);
                    LoadMealHistory();
                    LoadTodaysMeals();

                    MessageBox.Show("Meal deleted", "Success");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting meal: {ex.Message}", "Error");
                }
            }
        }

        // View meal details when selected
        private void dgvMealHistory_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvMealHistory.SelectedRows.Count > 0)
            {
                int selectedIndex = dgvMealHistory.SelectedRows[0].Index;
                var meals = _mealService.GetUserMeals(_currentUser.UserId).Take(20).ToList();
                var selectedMeal = meals[selectedIndex];

                var mealFoods = _mealService.GetMealFoods(selectedMeal.MealId);
                lblMealDetails.Text = $"Foods in meal: {mealFoods.Count}";
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            _context?.Dispose();
        }
    }
}