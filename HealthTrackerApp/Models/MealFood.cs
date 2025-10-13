using System.ComponentModel.DataAnnotations;

namespace HealthTrackerApp.Models
{
    // Junction table for many-to-many relationship between Meals and Foods
    // Represents a specific food item within a meal with its portion size and calculated nutrition
    public class MealFood
    {
        [Key]
        public int MealFoodId { get; set; }

        [Required]
        // Foreign key to Meal table
        public int MealId { get; set; }

        [Required]
        // Foreign key to Food table
        public int FoodId { get; set; }

        [Required]
        // User's portion size (e.g., 150 for "150g" or 1 for "1 serving")
        public double PortionSize { get; set; }

        [Required]
        [MaxLength(20)]
        // Unit for portion: "g" (grammes), "ml" (millilitres), "serving", etc.
        public string PortionUnit { get; set; }

        // Calculated nutritional values for this specific portion
        // Formula: (Food.CaloriesPer100g * PortionSize) / 100
        // These are pre-calculated and stored to improve query performance
        [Required]
        public double Calories { get; set; }

        [Required]
        public double Protein { get; set; }

        [Required]
        public double Carbs { get; set; }

        [Required]
        public double Fats { get; set; }

        // Navigation properties - virtual for lazy loading
        // Reference to the meal this food belongs to
        public virtual Meal Meal { get; set; }

        // Reference to the food item
        public virtual Food Food { get; set; }
    }
}