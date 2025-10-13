using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HealthTrackerApp.Models.Enums;

namespace HealthTrackerApp.Models
{
    // Represents a single meal entry (breakfast, lunch, dinner, or snack)
    // Contains calculated nutritional totals from all foods in the meal
    public class Meal
    {
        [Key]
        public int MealId { get; set; }

        [Required]
        // Foreign key to User table
        public int UserId { get; set; }

        [Required]
        public DateTime MealDate { get; set; }

        [Required]
        // Type of meal: Breakfast, Lunch, Dinner, or Snack
        public MealType MealType { get; set; }

        [Required]
        [MaxLength(100)]
        // User-defined name for the meal (e.g., "Protein Breakfast", "Post-workout Snack")
        public string MealName { get; set; }

        // Calculated nutritional totals for this meal
        // Sum of all foods' portions in MealFoods table
        [Required]
        public double TotalCalories { get; set; }

        [Required]
        public double Protein { get; set; }

        [Required]
        public double Carbs { get; set; }

        [Required]
        public double Fats { get; set; }

        [MaxLength(500)]
        // Optional notes about the meal (e.g., "Felt full", "Cheat meal")
        public string Notes { get; set; }

        // Navigation properties - virtual for lazy loading
        // Reference to the user who logged this meal
        public virtual User User { get; set; }

        // Collection of food items in this meal (many-to-many through MealFood)
        public virtual ICollection<MealFood> MealFoods { get; set; }

        public Meal()
        {
            // Initialise collection to avoid null reference errors
            MealFoods = new List<MealFood>();
            MealDate = DateTime.Now;
        }
    }
}