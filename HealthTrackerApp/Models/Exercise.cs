using System;
using System.ComponentModel.DataAnnotations;
using HealthTrackerApp.Models.Enums;

namespace HealthTrackerApp.Models
{
    // Represents a single exercise or workout session
    // Tracks physical activity with duration, intensity, and calories burned
    public class Exercise
    {
        [Key]
        public int ExerciseId { get; set; }

        [Required]
        // Foreign key to User table
        public int UserId { get; set; }

        [Required]
        public DateTime ExerciseDate { get; set; }

        [Required]
        [MaxLength(100)]
        // Name of the exercise (e.g., "Running", "Bench Press", "Yoga")
        public string ExerciseName { get; set; }

        [Required]
        // Category of exercise: Cardio, Strength, Flexibility, Sports, or Other
        public ExerciseCategory Category { get; set; }

        [Required]
        // Duration of exercise in minutes
        public int DurationMinutes { get; set; }

        [Required]
        // Intensity level: Low, Moderate, High, or VeryHigh
        public IntensityLevel Intensity { get; set; }

        [Required]
        // Calories burned during this exercise
        // Can be calculated using MET values or user-provided
        public double CaloriesBurned { get; set; }

        [MaxLength(500)]
        // Optional notes about the workout (e.g., "Personal best!", "Felt tired")
        public string Notes { get; set; }

        // Navigation property - virtual for lazy loading
        // Reference to the user who logged this exercise
        public virtual User User { get; set; }

        public Exercise()
        {
            ExerciseDate = DateTime.Now;
        }
    }
}