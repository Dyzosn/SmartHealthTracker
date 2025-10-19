using System;
using HealthTrackerApp.BusinessLogic.Interfaces;

namespace HealthTrackerApp.BusinessLogic.Calculators
{
    // Calculator for distributing total calories into macronutrient grammes
    // Based on percentage ratios (e.g., 30% protein, 40% carbs, 30% fats)
    public class MacroDistributionCalculator : ICalculatable, IValidatable
    {
        // Input properties
        public double TotalCalories { get; set; }
        public double ProteinPercent { get; set; }
        public double CarbsPercent { get; set; }
        public double FatsPercent { get; set; }

        // Output properties - grammes of each macro
        public double ProteinGrams { get; private set; }
        public double CarbsGrams { get; private set; }
        public double FatsGrams { get; private set; }

        // Calorie conversion constants
        private const double CALORIES_PER_GRAM_PROTEIN = 4.0;
        private const double CALORIES_PER_GRAM_CARBS = 4.0;
        private const double CALORIES_PER_GRAM_FATS = 9.0;

        // Implements ICalculatable interface
        public double Calculate()
        {
            if (!Validate())
            {
                throw new InvalidOperationException(GetValidationError());
            }

            // Calculate calories for each macro based on percentages
            double proteinCalories = TotalCalories * (ProteinPercent / 100.0);
            double carbsCalories = TotalCalories * (CarbsPercent / 100.0);
            double fatsCalories = TotalCalories * (FatsPercent / 100.0);

            // Convert calories to grammes using conversion factors
            ProteinGrams = proteinCalories / CALORIES_PER_GRAM_PROTEIN;
            CarbsGrams = carbsCalories / CALORIES_PER_GRAM_CARBS;
            FatsGrams = fatsCalories / CALORIES_PER_GRAM_FATS;

            return TotalCalories;
        }

        // Implements IValidatable interface
        public bool Validate()
        {
            // Total calories must be positive
            if (TotalCalories <= 0)
                return false;

            // Each percentage must be between 0 and 100
            if (ProteinPercent < 0 || ProteinPercent > 100)
                return false;
            if (CarbsPercent < 0 || CarbsPercent > 100)
                return false;
            if (FatsPercent < 0 || FatsPercent > 100)
                return false;

            // Percentages must sum to 100 (allow 0.1% tolerance for rounding)
            double sum = ProteinPercent + CarbsPercent + FatsPercent;
            if (Math.Abs(sum - 100.0) > 0.1)
                return false;

            return true;
        }

        public string GetValidationError()
        {
            if (TotalCalories <= 0)
                return "Total calories must be positive";

            if (ProteinPercent < 0 || ProteinPercent > 100 ||
                CarbsPercent < 0 || CarbsPercent > 100 ||
                FatsPercent < 0 || FatsPercent > 100)
                return "Percentages must be between 0 and 100";

            double sum = ProteinPercent + CarbsPercent + FatsPercent;
            if (Math.Abs(sum - 100.0) > 0.1)
                return $"Percentages must sum to 100 (currently {sum:F1}%)";

            return string.Empty;
        }

        // Provides common macro distribution presets
        public static (double protein, double carbs, double fats) GetBalancedSplit()
        {
            return (30, 40, 30); // 30% protein, 40% carbs, 30% fats
        }

        public static (double protein, double carbs, double fats) GetHighProteinSplit()
        {
            return (40, 30, 30); // 40% protein, 30% carbs, 30% fats
        }

        public static (double protein, double carbs, double fats) GetLowCarbSplit()
        {
            return (30, 20, 50); // 30% protein, 20% carbs, 50% fats (keto-style)
        }
    }
}