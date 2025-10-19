using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using HealthTrackerApp.Data;
using HealthTrackerApp.Models;
using HealthTrackerApp.Models.Enums;
using HealthTrackerApp.Services;

namespace HealthTrackerApp.Services
{
    // Service for goal CRUD operations
    // Handles goal creation, progress updates, and completion tracking
    public class GoalService
    {
        private readonly HealthTrackerContext _context;

        public GoalService(HealthTrackerContext context)
        {
            _context = context;
        }

        // Creates a new goal for a user
        public Goal CreateGoal(int userId, GoalType goalType, string description,
            double targetValue, double currentValue, DateTime targetDate)
        {
            var goal = new Goal
            {
                UserId = userId,
                GoalType = goalType,
                Description = description,
                TargetValue = targetValue,
                CurrentValue = currentValue,
                StartDate = DateTime.Now,
                TargetDate = targetDate,
                IsCompleted = false
            };

            _context.Goals.Add(goal);
            _context.SaveChanges();

            return goal;
        }

        // Retrieves all goals for a specific user, ordered by start date descending
        public List<Goal> GetUserGoals(int userId)
        {
            return _context.Goals
                .Where(g => g.UserId == userId)
                .OrderByDescending(g => g.StartDate)
                .ToList();
        }

        // Retrieves only active (incomplete) goals, ordered by target date
        public List<Goal> GetActiveGoals(int userId)
        {
            return _context.Goals
                .Where(g => g.UserId == userId && !g.IsCompleted)
                .OrderBy(g => g.TargetDate)
                .ToList();
        }

        // Retrieves completed goals, ordered by completion date descending
        public List<Goal> GetCompletedGoals(int userId)
        {
            return _context.Goals
                .Where(g => g.UserId == userId && g.IsCompleted)
                .OrderByDescending(g => g.CompletedDate)
                .ToList();
        }

        // Retrieves single goal by ID
        public Goal GetGoalById(int goalId)
        {
            return _context.Goals.Find(goalId);
        }

        // Updates goal progress value only
        // Does not mark as completed - use GoalManager for completion logic
        public void UpdateGoalProgress(int goalId, double newValue)
        {
            var goal = _context.Goals.Find(goalId);
            if (goal == null)
                throw new Exception("Goal not found");

            goal.CurrentValue = newValue;
            _context.SaveChanges();
        }

        // Updates complete goal record
        public void UpdateGoal(Goal goal)
        {
            _context.Goals.Update(goal);
            _context.SaveChanges();
        }

        // Marks goal as completed with completion date
        public void CompleteGoal(int goalId)
        {
            var goal = _context.Goals.Find(goalId);
            if (goal == null)
                throw new Exception("Goal not found");

            goal.IsCompleted = true;
            goal.CompletedDate = DateTime.Now;
            _context.SaveChanges();
        }

        // Deletes a goal from database
        public void DeleteGoal(int goalId)
        {
            var goal = _context.Goals.Find(goalId);
            if (goal == null)
                throw new Exception("Goal not found");

            _context.Goals.Remove(goal);
            _context.SaveChanges();
        }

        // Retrieves goals of a specific type for a user
        public List<Goal> GetGoalsByType(int userId, GoalType goalType)
        {
            return _context.Goals
                .Where(g => g.UserId == userId && g.GoalType == goalType)
                .ToList();
        }

        // Retrieves overdue goals (not completed and past target date)
        public List<Goal> GetOverdueGoals(int userId)
        {
            DateTime now = DateTime.Now;
            return _context.Goals
                .Where(g => g.UserId == userId &&
                           !g.IsCompleted &&
                           g.TargetDate < now)
                .OrderBy(g => g.TargetDate)
                .ToList();
        }

        // Retrieves goals due within specified number of days
        public List<Goal> GetUpcomingGoals(int userId, int daysAhead)
        {
            DateTime now = DateTime.Now;
            DateTime futureDate = now.AddDays(daysAhead);

            return _context.Goals
                .Where(g => g.UserId == userId &&
                           !g.IsCompleted &&
                           g.TargetDate >= now &&
                           g.TargetDate <= futureDate)
                .OrderBy(g => g.TargetDate)
                .ToList();
        }

        // Counts active goals by type for dashboard statistics
        public Dictionary<GoalType, int> GetGoalCountsByType(int userId)
        {
            return _context.Goals
                .Where(g => g.UserId == userId && !g.IsCompleted)
                .GroupBy(g => g.GoalType)
                .ToDictionary(g => g.Key, g => g.Count());
        }
    }
}