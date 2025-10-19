using System;
using HealthTrackerApp.BusinessLogic.Interfaces;
using HealthTrackerApp.Models.Enums;
using HealthTrackerApp.Utilities;

namespace HealthTrackerApp.BusinessLogic.Calculators
{
    // Calculator for calories burned during exercise
    // Uses MET (Metabolic Equivalent of Task) values based on intensity
    public class CalorieBurnCalculator : ICalculatable, IValidatable
    {
        // Input properties
        public int DurationMinutes { get; set; }
        public double WeightKg { get; set; }
        public IntensityLevel Intensity { get; set; }

        // Output property
        public double CaloriesBurned { get; private set; }

        // Implements ICalculatable interface
        public double Calculate()
        {
            if (!Validate())
            {
                throw new InvalidOperationException(GetValidationError());
            }

            // Get MET value based on intensity level
            double metValue = GetMETValue();

            // Convert duration to hours
            double durationHours = DurationMinutes / 60.0;

            // MET formula: MET × weight (kg) × duration (hours)
            CaloriesBurned = metValue * WeightKg * durationHours;

            return CaloriesBurned;
        }

        // Gets MET value from Constants based on intensity level
        private double GetMETValue()
        {
            switch (Intensity)
            {
                case IntensityLevel.Low:
                    return Constants.MET_LOW_INTENSITY;
                case IntensityLevel.Moderate:
                    return Constants.MET_MODERATE_INTENSITY;
                case IntensityLevel.High:
                    return Constants.MET_HIGH_INTENSITY;
                case IntensityLevel.VeryHigh:
                    return Constants.MET_VERY_HIGH_INTENSITY;
                default:
                    return Constants.MET_MODERATE_INTENSITY;
            }
        }

        // Implements IValidatable interface
        public bool Validate()
        {
            // Duration must be positive and reasonable (max 8 hours)
            if (DurationMinutes <= 0 || DurationMinutes > 480)
                return false;

            // Weight must be within realistic range
            if (WeightKg < Constants.MIN_WEIGHT_KG || WeightKg > Constants.MAX_WEIGHT_KG)
                return false;

            return true;
        }

        public string GetValidationError()
        {
            if (DurationMinutes <= 0 || DurationMinutes > 480)
                return "Duration must be between 1 and 480 minutes";

            if (WeightKg < Constants.MIN_WEIGHT_KG || WeightKg > Constants.MAX_WEIGHT_KG)
                return $"Weight must be between {Constants.MIN_WEIGHT_KG} and {Constants.MAX_WEIGHT_KG} kg";

            return string.Empty;
        }

        // Returns calories burned per minute
        public double GetCaloriesPerMinute()
        {
            if (CaloriesBurned == 0) Calculate();
            return CaloriesBurned / DurationMinutes;
        }
    }
}