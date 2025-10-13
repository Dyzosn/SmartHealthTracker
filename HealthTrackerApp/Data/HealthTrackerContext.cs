using Microsoft.EntityFrameworkCore;
using HealthTrackerApp.Models;

namespace HealthTrackerApp.Data
{
    // Database context for Health Tracker application
    // Manages all entity sets and database configuration using Entity Framework Core
    public class HealthTrackerContext : DbContext
    {
        // Entity sets representing database tables
        public DbSet<User> Users { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<MealFood> MealFoods { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<HealthMetricRecord> HealthMetrics { get; set; }
        public DbSet<Goal> Goals { get; set; }

        // Configure database connection
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Use SQLite with connection string from Constants
            optionsBuilder.UseSqlite(HealthTrackerApp.Utilities.Constants.CONNECTION_STRING);
        }

        // Configure entity relationships and constraints
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Many-to-Many relationship between Meals and Foods through MealFoods
            // A meal can have many foods, and a food can be in many meals
            modelBuilder.Entity<MealFood>()
                .HasOne(mf => mf.Meal)
                .WithMany(m => m.MealFoods)
                .HasForeignKey(mf => mf.MealId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MealFood>()
                .HasOne(mf => mf.Food)
                .WithMany(f => f.MealFoods)
                .HasForeignKey(mf => mf.FoodId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure One-to-Many relationship: User -> Meals
            modelBuilder.Entity<Meal>()
                .HasOne(m => m.User)
                .WithMany(u => u.Meals)
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure One-to-Many relationship: User -> Exercises
            modelBuilder.Entity<Exercise>()
                .HasOne(e => e.User)
                .WithMany(u => u.Exercises)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure One-to-Many relationship: User -> HealthMetrics
            modelBuilder.Entity<HealthMetricRecord>()
                .HasOne(h => h.User)
                .WithMany(u => u.HealthMetrics)
                .HasForeignKey(h => h.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure One-to-Many relationship: User -> Goals
            modelBuilder.Entity<Goal>()
                .HasOne(g => g.User)
                .WithMany(u => u.Goals)
                .HasForeignKey(g => g.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure enum storage as strings for better readability in database
            modelBuilder.Entity<Meal>()
                .Property(m => m.MealType)
                .HasConversion<string>();

            modelBuilder.Entity<Exercise>()
                .Property(e => e.Category)
                .HasConversion<string>();

            modelBuilder.Entity<Exercise>()
                .Property(e => e.Intensity)
                .HasConversion<string>();

            modelBuilder.Entity<Goal>()
                .Property(g => g.GoalType)
                .HasConversion<string>();
        }
    }
}