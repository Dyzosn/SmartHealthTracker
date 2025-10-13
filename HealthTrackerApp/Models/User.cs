using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HealthTrackerApp.Models
{
    // Represents a user account in the system
    // Contains profile information and navigation properties to all user-related data
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MaxLength(100)]
        // Stored as plain text for simplicity in this assignment
        // In production, passwords should be hashed using bcrypt or similar
        public string Password { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        // Height in metres (e.g., 1.75 for 175cm)
        public double Height { get; set; }

        [Required]
        [MaxLength(10)]
        public string Gender { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        // Navigation properties - virtual for lazy loading
        // One user can have many meals
        public virtual ICollection<Meal> Meals { get; set; }

        // One user can have many exercises
        public virtual ICollection<Exercise> Exercises { get; set; }

        // One user can have many health metric records
        public virtual ICollection<HealthMetricRecord> HealthMetrics { get; set; }

        // One user can have many goals
        public virtual ICollection<Goal> Goals { get; set; }

        public User()
        {
            // Initialise collections to avoid null reference errors
            Meals = new List<Meal>();
            Exercises = new List<Exercise>();
            HealthMetrics = new List<HealthMetricRecord>();
            Goals = new List<Goal>();
            CreatedDate = DateTime.Now;
        }
    }
}