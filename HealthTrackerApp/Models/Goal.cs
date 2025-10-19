using System;
using System.ComponentModel.DataAnnotations;
using HealthTrackerApp.Models.Enums;

namespace HealthTrackerApp.Models
{
    // Represents a user's health or fitness goal with progress tracking
    // Goals can be weight-related, calorie targets, or exercise frequency
    public class Goal
    {
        [Key]
        public int GoalId { get; set; }

        [Required]
        // Foreign key to User table
        public int UserId { get; set; }

        [Required]
        // Type of goal: WeightLoss, WeightGain, MuscleGain, CalorieTarget, ExerciseFrequency
        public GoalType GoalType { get; set; }

        [Required]
        [MaxLength(200)]
        // User-defined description (e.g., "Lose 5kg by summer", "Exercise 4x per week")
        public string Description { get; set; }

        [Required]
        // Target value to achieve (e.g., 70.0 for "70kg target weight")
        public double TargetValue { get; set; }

        [Required]
        // Current progress towards the goal (updated as user logs data)
        public double CurrentValue { get; set; }

        [Required]
        // Stores the starting value when goal was first created
        // This allows accurate progress calculation without arbitrary windows
        public double InitialValue { get; set; }

        [Required]
        // Date when the goal was created
        public DateTime StartDate { get; set; }

        [Required]
        // Target date to achieve the goal
        public DateTime TargetDate { get; set; }

        // Date when the goal was completed (null if still in progress)
        public DateTime? CompletedDate { get; set; }

        [Required]
        // Flag indicating whether the goal has been achieved
        public bool IsCompleted { get; set; }

        // Navigation property - virtual for lazy loading
        // Reference to the user who set this goal
        public virtual User User { get; set; }

        public Goal()
        {
            StartDate = DateTime.Now;
            IsCompleted = false;
            CurrentValue = 0;
            InitialValue = 0; // Will be set to CurrentValue on creation
        }
    }
}