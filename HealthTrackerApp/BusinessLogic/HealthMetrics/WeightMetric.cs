using System;

namespace HealthTrackerApp.BusinessLogic.HealthMetrics
{
    // Derived class for weight measurements
    // Inherits from HealthMetric and implements weight-specific behaviour
    public class WeightMetric : HealthMetric
    {
        // Weight in kilogrammes
        public double WeightKg { get; set; }

        // Implements abstract method - returns formatted weight display
        public override string Display()
        {
            return $"Weight: {WeightKg:F2} kg";
        }

        // Implements abstract method - validates weight is within reasonable range
        // Acceptable range: 20kg to 300kg
        public override bool Validate()
        {
            // Weight must be positive and within realistic human range
            if (WeightKg < 20 || WeightKg > 300)
            {
                return false;
            }
            return true;
        }

        // Implements abstract method - returns metric type identifier
        public override string GetMetricType()
        {
            return "Weight";
        }

        // Overrides virtual method to provide weight-specific summary
        public override string GetSummary()
        {
            return $"Weight: {WeightKg:F1}kg on {RecordedDate:dd/MM/yyyy}";
        }

        // Additional method specific to weight metrics
        // Calculates BMI if height is provided
        public double CalculateBMI(double heightInMetres)
        {
            if (heightInMetres <= 0)
            {
                throw new ArgumentException("Height must be greater than zero");
            }
            // BMI formula: weight (kg) / height^2 (m)
            return WeightKg / (heightInMetres * heightInMetres);
        }

        // Returns BMI category based on WHO standards
        public string GetBMICategory(double bmi)
        {
            if (bmi < 18.5)
                return "Underweight";
            else if (bmi < 25)
                return "Normal";
            else if (bmi < 30)
                return "Overweight";
            else
                return "Obese";
        }
    }
}