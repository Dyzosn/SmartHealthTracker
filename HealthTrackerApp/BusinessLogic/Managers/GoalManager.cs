using System;
using HealthTrackerApp.BusinessLogic.Calculators;
using HealthTrackerApp.BusinessLogic.Interfaces;
using HealthTrackerApp.BusinessLogic.Managers;
using HealthTrackerApp.Models;
using HealthTrackerApp.Models.Enums;

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
                    // For calorie target, goal is to STAY UNDER target
                    // Achievement means staying at or below target consistently
                    return goal.CurrentValue <= goal.TargetValue;

                case GoalType.ExerciseFrequency:
                    // For frequency targets, current should meet or exceed target
                    return goal.CurrentValue >= goal.TargetValue;

                default:
                    return false;
            }
        }

        // Calculates progress as a percentage from 0 to 100
        // Uses InitialValue as starting point for TRUE LINEAR PROGRESS
        public double CalculateProgress(Goal goal)
        {
            if (goal == null) return 0;

            double progress;

            switch (goal.GoalType)
            {
                case GoalType.WeightLoss:
                    // For weight loss: Progress from Initial (higher) to Target (lower)
                    // Example: Initial=80, Target=75, Current=77
                    // Progress = (80-77) / (80-75) * 100 = 3/5 * 100 = 60%

                    if (goal.CurrentValue <= goal.TargetValue)
                        return 100.0; // Achieved!

                    if (goal.CurrentValue >= goal.InitialValue)
                        return 0.0; // No progress yet (or gained weight)

                    double totalWeightToLose = goal.InitialValue - goal.TargetValue;
                    double weightLostSoFar = goal.InitialValue - goal.CurrentValue;

                    progress = totalWeightToLose > 0 ? (weightLostSoFar / totalWeightToLose) * 100.0 : 0;
                    break;

                case GoalType.WeightGain:
                case GoalType.MuscleGain:
                    // For weight gain: Progress from Initial (lower) to Target (higher)
                    // Example: Initial=80, Target=85, Current=82
                    // Progress = (82-80) / (85-80) * 100 = 2/5 * 100 = 40%

                    if (goal.CurrentValue >= goal.TargetValue)
                        return 100.0; // Achieved!

                    if (goal.CurrentValue <= goal.InitialValue)
                        return 0.0; // No progress yet (or lost weight)

                    double totalWeightToGain = goal.TargetValue - goal.InitialValue;
                    double weightGainedSoFar = goal.CurrentValue - goal.InitialValue;

                    progress = totalWeightToGain > 0 ? (weightGainedSoFar / totalWeightToGain) * 100.0 : 0;
                    break;

                case GoalType.CalorieTarget:
                    // For calorie target: Goal is to STAY UNDER target
                    // Progress from Initial (over target) to Target (at or under)
                    // Example: Initial=2200, Target=2000, Current=2100
                    // Progress = (2200-2100) / (2200-2000) * 100 = 100/200 * 100 = 50%

                    if (goal.CurrentValue <= goal.TargetValue)
                        return 100.0; // Successfully staying under!

                    if (goal.CurrentValue >= goal.InitialValue)
                        return 0.0; // No progress (still at or above initial)

                    double totalCaloriesToReduce = goal.InitialValue - goal.TargetValue;
                    double caloriesReducedSoFar = goal.InitialValue - goal.CurrentValue;

                    progress = totalCaloriesToReduce > 0 ? (caloriesReducedSoFar / totalCaloriesToReduce) * 100.0 : 0;
                    break;

                case GoalType.ExerciseFrequency:
                    // For frequency: Simple ratio from 0 to target
                    // Example: Initial=0, Target=20, Current=8
                    // Progress = 8/20 * 100 = 40%

                    if (goal.CurrentValue >= goal.TargetValue)
                        return 100.0; // Target met!

                    progress = goal.TargetValue > 0 ? (goal.CurrentValue / goal.TargetValue) * 100.0 : 0;
                    break;

                default:
                    progress = 0;
                    break;
            }

            // Cap between 0-100%
            return Math.Max(0, Math.Min(progress, 100));
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