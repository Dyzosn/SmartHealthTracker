using System;
using System.Collections.Generic;
using System.Linq;
using HealthTrackerApp.Data;
using HealthTrackerApp.Models;
using HealthTrackerApp.Models.Enums;
using HealthTrackerApp.BusinessLogic.Calculators;

namespace HealthTrackerApp.Services
{
    // Service for exercise CRUD operations
    // Handles exercise logging with automatic calorie burn calculations
    public class ExerciseService
    {
        private readonly HealthTrackerContext _context;

        public ExerciseService(HealthTrackerContext context)
        {
            _context = context;
        }

        // Creates new exercise record with automatic calorie calculation
        // Uses CalorieBurnCalculator to determine calories burned based on MET values
        public Exercise CreateExercise(int userId, DateTime date, string exerciseName,
            ExerciseCategory category, int durationMinutes, IntensityLevel intensity,
            double userWeightKg, string notes = "")
        {
            // Calculate calories burned using calculator from Phase 2
            var calculator = new CalorieBurnCalculator
            {
                DurationMinutes = durationMinutes,
                WeightKg = userWeightKg,
                Intensity = intensity
            };

            double caloriesBurned = calculator.Calculate();

            // Create exercise record with calculated calories
            var exercise = new Exercise
            {
                UserId = userId,
                ExerciseDate = date,
                ExerciseName = exerciseName,
                Category = category,
                DurationMinutes = durationMinutes,
                Intensity = intensity,
                CaloriesBurned = caloriesBurned,
                Notes = notes
            };

            _context.Exercises.Add(exercise);
            _context.SaveChanges();

            return exercise;
        }

        // Retrieves all exercises for a specific user, ordered by date descending
        public List<Exercise> GetUserExercises(int userId)
        {
            return _context.Exercises
                .Where(e => e.UserId == userId)
                .OrderByDescending(e => e.ExerciseDate)
                .ToList();
        }

        // Retrieves exercises for the current day only
        public List<Exercise> GetTodaysExercises(int userId)
        {
            DateTime today = DateTime.Today;
            return _context.Exercises
                .Where(e => e.UserId == userId && e.ExerciseDate.Date == today)
                .OrderBy(e => e.ExerciseDate)
                .ToList();
        }

        // Retrieves exercises within a specific date range
        public List<Exercise> GetExercisesByDateRange(int userId, DateTime startDate, DateTime endDate)
        {
            return _context.Exercises
                .Where(e => e.UserId == userId &&
                           e.ExerciseDate >= startDate &&
                           e.ExerciseDate <= endDate)
                .OrderBy(e => e.ExerciseDate)
                .ToList();
        }

        // Retrieves single exercise by ID
        public Exercise GetExerciseById(int exerciseId)
        {
            return _context.Exercises.Find(exerciseId);
        }

        // Updates an existing exercise record
        public void UpdateExercise(Exercise exercise)
        {
            // Get user's current weight from health metrics
            double userWeight = GetUserWeight(exercise.UserId);

            // Recalculate calories if duration, intensity, or weight changed
            var calculator = new CalorieBurnCalculator
            {
                DurationMinutes = exercise.DurationMinutes,
                WeightKg = userWeight,
                Intensity = exercise.Intensity
            };

            exercise.CaloriesBurned = calculator.Calculate();

            _context.Exercises.Update(exercise);
            _context.SaveChanges();
        }

        // Gets user's current weight from latest health metric record
        // Returns default 70kg if no weight records found
        private double GetUserWeight(int userId)
        {
            var latestWeight = _context.HealthMetrics
                .Where(hm => hm.UserId == userId && hm.WeightKg.HasValue)
                .OrderByDescending(hm => hm.RecordedDate)
                .FirstOrDefault();

            return latestWeight?.WeightKg ?? 70.0; // Default 70kg if no weight recorded
        }

        // Deletes an exercise record
        public void DeleteExercise(int exerciseId)
        {
            var exercise = _context.Exercises.Find(exerciseId);
            if (exercise == null)
                throw new Exception("Exercise not found");

            _context.Exercises.Remove(exercise);
            _context.SaveChanges();
        }

        // Calculates total calories burned today for a user
        public double GetTodaysCaloriesBurned(int userId)
        {
            var todaysExercises = GetTodaysExercises(userId);
            return todaysExercises.Sum(e => e.CaloriesBurned);
        }

        // Retrieves exercise statistics for a specified number of days
        // Returns tuple with count, total calories, and total minutes
        public (int totalExercises, double totalCalories, int totalMinutes) GetExerciseStats(
            int userId, int days)
        {
            DateTime startDate = DateTime.Now.AddDays(-days);
            var exercises = GetExercisesByDateRange(userId, startDate, DateTime.Now);

            return (
                exercises.Count,
                exercises.Sum(e => e.CaloriesBurned),
                exercises.Sum(e => e.DurationMinutes)
            );
        }

        // Retrieves exercises grouped by category with statistics
        public List<(ExerciseCategory category, int count, double totalCalories)> GetExercisesByCategory(int userId)
        {
            return _context.Exercises
                .Where(e => e.UserId == userId)
                .GroupBy(e => e.Category)
                .Select(g => new ValueTuple<ExerciseCategory, int, double>(
                    g.Key,
                    g.Count(),
                    g.Sum(e => e.CaloriesBurned)
                ))
                .ToList();
        }
    }
}