using System;
using System.Collections.Generic;
using System.Linq;
using HealthTrackerApp.Data;
using HealthTrackerApp.Models;
using HealthTrackerApp.Models.Enums;

namespace HealthTrackerApp.Services
{
    // Service for meal CRUD operations and nutrition calculations
    // Handles meal creation, food additions, and portion-based nutrition
    public class MealService
    {
        private readonly HealthTrackerContext _context;

        public MealService(HealthTrackerContext context)
        {
            _context = context;
        }

        // Create new meal for user
        public Meal CreateMeal(int userId, DateTime mealDate, MealType mealType, string mealName)
        {
            var meal = new Meal
            {
                UserId = userId,
                MealDate = mealDate,
                MealType = mealType,
                MealName = mealName,
                TotalCalories = 0,
                Protein = 0,
                Carbs = 0,
                Fats = 0,
                Notes = ""
            };

            _context.Meals.Add(meal);
            _context.SaveChanges();

            return meal;
        }

        // Add food to meal with portion calculation
        public void AddFoodToMeal(int mealId, Food food, double portionSize, string portionUnit)
        {
            // Calculate nutrition for user's portion based on per 100g values
            double multiplier = portionSize / 100.0;

            var mealFood = new MealFood
            {
                MealId = mealId,
                FoodId = food.FoodId,
                PortionSize = portionSize,
                PortionUnit = portionUnit,
                Calories = food.CaloriesPer100g * multiplier,
                Protein = food.ProteinPer100g * multiplier,
                Carbs = food.CarbsPer100g * multiplier,
                Fats = food.FatsPer100g * multiplier
            };

            _context.MealFoods.Add(mealFood);
            UpdateMealTotals(mealId);
            _context.SaveChanges();
        }

        // Recalculate meal totals from all foods
        private void UpdateMealTotals(int mealId)
        {
            var meal = _context.Meals.Find(mealId);
            if (meal == null) return;

            // Sum nutrition from all foods in meal using LINQ
            var mealFoods = _context.MealFoods
                .Where(mf => mf.MealId == mealId)
                .ToList();

            meal.TotalCalories = mealFoods.Sum(mf => mf.Calories);
            meal.Protein = mealFoods.Sum(mf => mf.Protein);
            meal.Carbs = mealFoods.Sum(mf => mf.Carbs);
            meal.Fats = mealFoods.Sum(mf => mf.Fats);
        }

        // Get all meals for user
        public List<Meal> GetUserMeals(int userId)
        {
            return _context.Meals
                .Where(m => m.UserId == userId)
                .OrderByDescending(m => m.MealDate)
                .ToList();
        }

        // Get meals for specific date
        public List<Meal> GetMealsByDate(int userId, DateTime date)
        {
            return _context.Meals
                .Where(m => m.UserId == userId && m.MealDate.Date == date.Date)
                .OrderBy(m => m.MealDate)
                .ToList();
        }

        // Get meals within date range
        public List<Meal> GetMealsByDateRange(int userId, DateTime startDate, DateTime endDate)
        {
            return _context.Meals
                .Where(m => m.UserId == userId &&
                           m.MealDate >= startDate &&
                           m.MealDate <= endDate)
                .OrderBy(m => m.MealDate)
                .ToList();
        }

        // Delete meal and associated foods
        public void DeleteMeal(int mealId)
        {
            var meal = _context.Meals.Find(mealId);
            if (meal == null) return;

            // EF will cascade delete related MealFoods automatically
            _context.Meals.Remove(meal);
            _context.SaveChanges();
        }

        // Get foods in specific meal
        public List<MealFood> GetMealFoods(int mealId)
        {
            return _context.MealFoods
                .Where(mf => mf.MealId == mealId)
                .ToList();
        }

        // Get today's total calories for user
        public double GetTodaysTotalCalories(int userId)
        {
            var todayMeals = GetMealsByDate(userId, DateTime.Today);
            return todayMeals.Sum(m => m.TotalCalories);
        }

        // Get weekly average calories
        public double GetWeeklyAverageCalories(int userId)
        {
            var weekStart = DateTime.Today.AddDays(-7);
            var weeklyMeals = GetMealsByDateRange(userId, weekStart, DateTime.Today);

            if (weeklyMeals.Count == 0) return 0;

            return weeklyMeals.GroupBy(m => m.MealDate.Date)
                .Select(g => g.Sum(m => m.TotalCalories))
                .Average();
        }
    }
}