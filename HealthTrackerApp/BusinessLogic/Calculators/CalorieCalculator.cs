using System;
using HealthTrackerApp.BusinessLogic.Interfaces;

namespace HealthTrackerApp.BusinessLogic.Calculators
{
    // Calculator for total calories from macronutrients
    // Each gramme of protein and carbs provides 4 calories, fats provide 9 calories
    public class CalorieCalculator : ICalculatable, IValidatable
    {
        // Input properties - macronutrients in grammes
        public double Protein { get; set; }
        public double Carbs { get; set; }
        public double Fats { get; set; }

        // Calorie conversion constants
        private const double CALORIES_PER_GRAM_PROTEIN = 4.0;
        private const double CALORIES_PER_GRAM_CARBS = 4.0;
        private const double CALORIES_PER_GRAM_FATS = 9.0;

        // Output property
        public double TotalCalories { get; private set; }

        // Implements ICalculatable interface
        public double Calculate()
        {
            if (!Validate())
            {
                throw new InvalidOperationException(GetValidationError());
            }

            // Calculate calories from each macronutrient
            double proteinCal = Protein * CALORIES_PER_GRAM_PROTEIN;
            double carbsCal = Carbs * CALORIES_PER_GRAM_CARBS;
            double fatsCal = Fats * CALORIES_PER_GRAM_FATS;

            TotalCalories = proteinCal + carbsCal + fatsCal;
            return TotalCalories;
        }

        // Implements IValidatable interface
        public bool Validate()
        {
            // All macros must be non-negative
            if (Protein < 0 || Carbs < 0 || Fats < 0)
                return false;

            // Reasonable upper limits for per-100g food values
            if (Protein > 100 || Carbs > 100 || Fats > 100)
                return false;

            return true;
        }

        public string GetValidationError()
        {
            if (Protein < 0 || Carbs < 0 || Fats < 0)
                return "Macronutrients cannot be negative";

            if (Protein > 100 || Carbs > 100 || Fats > 100)
                return "Macronutrient values seem unrealistic (max 100g per 100g food)";

            return string.Empty;
        }

        // Returns percentage breakdown of calories from each macro
        public (double proteinPercent, double carbsPercent, double fatsPercent) GetMacroPercentages()
        {
            if (TotalCalories == 0) Calculate();

            double proteinCal = Protein * CALORIES_PER_GRAM_PROTEIN;
            double carbsCal = Carbs * CALORIES_PER_GRAM_CARBS;
            double fatsCal = Fats * CALORIES_PER_GRAM_FATS;

            return (
                (proteinCal / TotalCalories) * 100,
                (carbsCal / TotalCalories) * 100,
                (fatsCal / TotalCalories) * 100
            );
        }
    }
}