using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HealthTrackerApp.Models
{
    // Represents a food item with nutritional information
    // Can be sourced from USDA API or added as custom food by users
    // Nutrition values are standardised per 100g for consistent calculations
    public class Food
    {
        [Key]
        public int FoodId { get; set; }

        // USDA FoodData Central ID (0 for custom foods)
        public int FdcId { get; set; }

        [Required]
        [MaxLength(200)]
        public string FoodName { get; set; }

        [MaxLength(50)]
        // Data type from USDA API: "Branded", "SR Legacy", "Foundation", etc.
        public string DataType { get; set; }

        [MaxLength(100)]
        // Food category from USDA API or user-defined
        public string Category { get; set; }

        // Nutrition values standardised per 100g
        // This allows consistent calculations regardless of serving size
        [Required]
        public double CaloriesPer100g { get; set; }

        [Required]
        public double ProteinPer100g { get; set; }

        [Required]
        public double CarbsPer100g { get; set; }

        [Required]
        public double FatsPer100g { get; set; }

        // Original serving information from USDA API
        // Nullable because custom foods might not have this data
        // Serving size in grams (e.g., 284.0 for "284g")
        public double? ServingSize { get; set; }

        [MaxLength(20)]
        // Unit for serving size: "g", "ml", "oz", etc.
        public string ServingUnit { get; set; }

        [MaxLength(100)]
        // Household serving description from API (e.g., "1 CHICKEN BREAST", "1 CUP")
        public string HouseholdServing { get; set; }

        [Required]
        // Flag to distinguish between API-sourced and user-added foods
        public bool IsCustom { get; set; }

        [Required]
        public DateTime DateAdded { get; set; }

        // Navigation property - virtual for lazy loading
        // Foods can be used in many meals through MealFood junction table
        public virtual ICollection<MealFood> MealFoods { get; set; }

        public Food()
        {
            // Initialise collection to avoid null reference errors
            MealFoods = new List<MealFood>();
            DateAdded = DateTime.Now;
            IsCustom = false;
        }
    }
}