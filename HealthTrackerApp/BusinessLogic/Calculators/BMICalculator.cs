using System;
using HealthTrackerApp.BusinessLogic.Interfaces;
using HealthTrackerApp.Utilities;

namespace HealthTrackerApp.BusinessLogic.Calculators
{
    // Calculator for Body Mass Index
    // Formula: weight (kg) divided by height squared (metres)
    public class BMICalculator : ICalculatable, IValidatable
    {
        // Input properties
        public double WeightKg { get; set; }
        public double HeightM { get; set; }

        // Output property
        public double BMI { get; private set; }

        // Implements ICalculatable interface
        public double Calculate()
        {
            if (!Validate())
            {
                throw new InvalidOperationException(GetValidationError());
            }

            // BMI formula: weight / height squared
            BMI = WeightKg / (HeightM * HeightM);
            return BMI;
        }

        // Implements IValidatable interface
        public bool Validate()
        {
            // Weight must be within realistic human range
            if (WeightKg < Constants.MIN_WEIGHT_KG || WeightKg > Constants.MAX_WEIGHT_KG)
                return false;

            // Height must be within realistic human range
            if (HeightM < Constants.MIN_HEIGHT_M || HeightM > Constants.MAX_HEIGHT_M)
                return false;

            return true;
        }

        public string GetValidationError()
        {
            if (WeightKg < Constants.MIN_WEIGHT_KG || WeightKg > Constants.MAX_WEIGHT_KG)
                return $"Weight must be between {Constants.MIN_WEIGHT_KG} and {Constants.MAX_WEIGHT_KG} kg";

            if (HeightM < Constants.MIN_HEIGHT_M || HeightM > Constants.MAX_HEIGHT_M)
                return $"Height must be between {Constants.MIN_HEIGHT_M} and {Constants.MAX_HEIGHT_M} metres";

            return string.Empty;
        }

        // Returns BMI category based on WHO standards
        public string GetCategory()
        {
            if (BMI == 0) Calculate(); // Ensure BMI is calculated

            if (BMI < Constants.BMI_UNDERWEIGHT)
                return "Underweight";
            else if (BMI < Constants.BMI_NORMAL)
                return "Normal";
            else if (BMI < Constants.BMI_OVERWEIGHT)
                return "Overweight";
            else
                return "Obese";
        }

        // Returns colour for UI display based on category
        public string GetCategoryColour()
        {
            string category = GetCategory();

            switch (category)
            {
                case "Underweight":
                    return "Blue";
                case "Normal":
                    return "Green";
                case "Overweight":
                    return "Orange";
                case "Obese":
                    return "Red";
                default:
                    return "Black";
            }
        }
    }
}