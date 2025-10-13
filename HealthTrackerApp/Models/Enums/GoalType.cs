namespace HealthTrackerApp.Models.Enums
{
    // Defines the different types of health and fitness goals users can set
    // Used in Goal entity to categorise goals for progress tracking and achievement notifications
    public enum GoalType
    {
        WeightLoss,
        WeightGain,
        MuscleGain,
        CalorieTarget,
        ExerciseFrequency
    }
}