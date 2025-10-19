using System;
using System.Runtime.Intrinsics.Arm;
using HealthTrackerApp.BusinessLogic.Calculators;
using HealthTrackerApp.BusinessLogic.Interfaces;
using HealthTrackerApp.BusinessLogic.Managers;
using HealthTrackerApp.Models;
using HealthTrackerApp.Models.Enums;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HealthTrackerApp.BusinessLogic.Managers
{
    // Delegate declarations for goal events
    // Invoked when a goal is achieved
    public delegate void GoalAchievedHandler(Goal goal, string message);

    // Invoked when goal progress is updated
    public delegate void GoalProgressHandler(Goal goal, double progressPercent);

    // Manager class for tracking goal progress and firing achievement events
    // Demonstrates delegates and events pattern for notification system
    public class GoalManager
    {
        // Event declarations using the delegates above
        public event GoalAchievedHandler OnGoalAchieved;
        public event GoalProgressHandler OnGoalProgress;

        // Updates the goal's current value and checks for achievement
        // Fires events to notify subscribers of progress or completion
        public void UpdateProgress(Goal goal, double newValue)
        {
            if (goal == null)
                throw new ArgumentNullException(nameof(goal));

            // Update current value
            goal.CurrentValue = newValue;

            // Calculate progress percentage
            double progressPercent = CalculateProgress(goal);

            // Fire progress event to notify subscribers
            OnGoalProgress?.Invoke(goal, progressPercent);

            // Check if goal is now achieved
            if (IsGoalAchieved(goal) && !goal.IsCompleted)
            {
                // Mark as completed
                goal.IsCompleted = true;
                goal.CompletedDate = DateTime.Now;

                // Fire achievement event with congratulatory message
                string message = $"Congratulations! You've achieved your goal: {goal.Description}";
                OnGoalAchieved?.Invoke(goal, message);
            }
        }

        // Determines if a goal has been achieved based on goal type
        // Different goal types have different achievement criteria
        public bool IsGoalAchieved(Goal goal)
        {
            switch (goal.GoalType)
            {
                case GoalType.WeightLoss:
                    // For weight loss, current value should be less than or equal to target
                    return goal.CurrentValue <= goal.TargetValue;

                case GoalType.WeightGain:
                case GoalType.MuscleGain:
                    // For gain goals, current value should be greater than or equal to target
                    return goal.CurrentValue >= goal.TargetValue;

                case GoalType.CalorieTarget:
                case GoalType.ExerciseFrequency:
                    // For frequency targets, current should meet or exceed target
                    return goal.CurrentValue >= goal.TargetValue;

                default:
                    return false;
            }
        }

        // Calculates progress as a percentage from 0 to 100
        // Returns capped value to prevent percentages above 100
        public double CalculateProgress(Goal goal)
        {
            if (goal == null) return 0;

            double progress;

            switch (goal.GoalType)
            {
                case GoalType.WeightLoss:
                    // For weight loss, calculate how much has been lost
                    // Assume user started above target
                    double initialWeight = goal.TargetValue + Math.Abs(goal.TargetValue - goal.CurrentValue);
                    double weightLost = Math.Max(0, initialWeight - goal.CurrentValue);
                    double totalToLose = initialWeight - goal.TargetValue;
                    progress = totalToLose > 0 ? (weightLost / totalToLose) * 100 : 0;
                    break;

                case GoalType.WeightGain:
                case GoalType.MuscleGain:
                    // For gain goals, progress is current value relative to target
                    progress = goal.TargetValue > 0 ? (goal.CurrentValue / goal.TargetValue) * 100 : 0;
                    break;

                case GoalType.CalorieTarget:
                case GoalType.ExerciseFrequency:
                    // For target-based goals, calculate completion percentage
                    progress = goal.TargetValue > 0 ? (goal.CurrentValue / goal.TargetValue) * 100 : 0;
                    break;

                default:
                    progress = 0;
                    break;
            }

            // Cap progress at 100%
            return Math.Min(progress, 100);
        }

        // Returns number of days remaining until goal deadline
        public int GetDaysRemaining(Goal goal)
        {
            if (goal == null) return 0;

            TimeSpan remaining = goal.TargetDate - DateTime.Now;
            return Math.Max(0, remaining.Days);
        }

        // Checks if goal deadline has passed without completion
        public bool IsOverdue(Goal goal)
        {
            if (goal == null) return false;

            return DateTime.Now > goal.TargetDate && !goal.IsCompleted;
        }

        // Returns motivational message based on current progress
        public string GetMotivationMessage(Goal goal)
        {
            double progress = CalculateProgress(goal);

            if (goal.IsCompleted)
                return "Goal achieved! Congratulations!";

            if (progress >= 90)
                return "Almost there! Keep pushing!";
            else if (progress >= 75)
                return "Great progress! You're doing amazing!";
            else if (progress >= 50)
                return "Halfway there! Keep it up!";
            else if (progress >= 25)
                return "Good start! Stay consistent!";
            else
                return "Let's get started! You can do this!";
        }

        // Suggests actionable advice based on progress and time remaining
        public string SuggestAction(Goal goal)
        {
            double progress = CalculateProgress(goal);
            int daysRemaining = GetDaysRemaining(goal);

            if (IsOverdue(goal))
                return "Goal is overdue. Consider extending the deadline or creating a new goal.";

            if (daysRemaining < 7 && progress < 50)
                return "Time is running short. Consider adjusting your target or increasing effort.";

            if (progress < 20 && daysRemaining > 30)
                return "Plenty of time left! Stay consistent with your routine.";

            if (progress >= 90)
                return "You're so close! One final push to reach your goal!";

            return "Keep working towards your goal. Consistency is key!";
        }
    }
}