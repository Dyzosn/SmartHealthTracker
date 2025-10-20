using NUnit.Framework;
using HealthTrackerApp.BusinessLogic.Calculators;
using HealthTrackerApp.Models.Enums;
using System;

namespace HealthTrackerApp.Tests
{
    // Tests for all calculator classes
    // Covers BMI, Calorie, CalorieBurn, and MacroDistribution calculators
    [TestFixture]
    public class CalculatorTests
    {
        // BMI Calculator Tests

        [Test]
        public void BMICalculator_NormalWeight_ReturnsCorrectValue()
        {
            // Arrange - person with 70kg weight and 1.75m height
            var calculator = new BMICalculator
            {
                WeightKg = 70.0,
                HeightM = 1.75
            };

            // Act
            double result = calculator.Calculate();

            // Assert - BMI should be 22.86 (70 / 1.75^2)
            Assert.That(result, Is.EqualTo(22.86).Within(0.01));
            Assert.That(calculator.GetCategory(), Is.EqualTo("Normal"));
        }

        [Test]
        public void BMICalculator_Underweight_ReturnsCorrectCategory()
        {
            // Arrange - underweight person
            var calculator = new BMICalculator
            {
                WeightKg = 50.0,
                HeightM = 1.75
            };

            // Act
            double result = calculator.Calculate();

            // Assert - BMI should be under 18.5
            Assert.That(result, Is.LessThan(18.5));
            Assert.That(calculator.GetCategory(), Is.EqualTo("Underweight"));
        }

        [Test]
        public void BMICalculator_Obese_ReturnsCorrectCategory()
        {
            // Arrange - obese person
            var calculator = new BMICalculator
            {
                WeightKg = 100.0,
                HeightM = 1.65
            };

            // Act
            calculator.Calculate();

            // Assert - BMI should be over 30
            Assert.That(calculator.GetCategory(), Is.EqualTo("Obese"));
        }

        [Test]
        public void BMICalculator_InvalidWeight_ThrowsException()
        {
            // Arrange - unrealistic weight
            var calculator = new BMICalculator
            {
                WeightKg = 500.0,
                HeightM = 1.75
            };

            // Act & Assert - should throw exception for invalid input
            Assert.That(() => calculator.Calculate(), Throws.TypeOf<InvalidOperationException>());
        }

        // Calorie Calculator Tests

        [Test]
        public void CalorieCalculator_BalancedMacros_ReturnsCorrectTotal()
        {
            // Arrange - typical meal with balanced macros
            var calculator = new CalorieCalculator
            {
                Protein = 25.0,  // 25g protein = 100 kcal
                Carbs = 50.0,    // 50g carbs = 200 kcal
                Fats = 15.0      // 15g fats = 135 kcal
            };

            // Act
            double result = calculator.Calculate();

            // Assert - total should be 435 kcal
            Assert.That(result, Is.EqualTo(435.0).Within(0.01));
        }

        [Test]
        public void CalorieCalculator_NegativeValues_ThrowsException()
        {
            // Arrange - invalid negative protein
            var calculator = new CalorieCalculator
            {
                Protein = -10.0,
                Carbs = 50.0,
                Fats = 10.0
            };

            // Act & Assert - should reject negative macros
            Assert.That(() => calculator.Calculate(), Throws.TypeOf<InvalidOperationException>());
        }

        // Calorie Burn Calculator Tests

        [Test]
        public void CalorieBurnCalculator_HighIntensity_ReturnsCorrectCalories()
        {
            // Arrange - 30 min high intensity workout for 70kg person
            var calculator = new CalorieBurnCalculator
            {
                DurationMinutes = 30,
                WeightKg = 70.0,
                Intensity = IntensityLevel.High
            };

            // Act
            double result = calculator.Calculate();

            // Assert - MET 7.0 × 70kg × 0.5hr = 245 kcal
            Assert.That(result, Is.EqualTo(245.0).Within(0.1));
        }

        [Test]
        public void CalorieBurnCalculator_ModerateIntensity_ReturnsCorrectCalories()
        {
            // Arrange - 60 min moderate intensity for 80kg person
            var calculator = new CalorieBurnCalculator
            {
                DurationMinutes = 60,
                WeightKg = 80.0,
                Intensity = IntensityLevel.Moderate
            };

            // Act
            double result = calculator.Calculate();

            // Assert - MET 5.0 × 80kg × 1hr = 400 kcal
            Assert.That(result, Is.EqualTo(400.0).Within(0.1));
        }

        [Test]
        public void CalorieBurnCalculator_ZeroDuration_ThrowsException()
        {
            // Arrange - invalid zero duration
            var calculator = new CalorieBurnCalculator
            {
                DurationMinutes = 0,
                WeightKg = 70.0,
                Intensity = IntensityLevel.Moderate
            };

            // Act & Assert - should reject zero duration
            Assert.That(() => calculator.Calculate(), Throws.TypeOf<InvalidOperationException>());
        }

        // Macro Distribution Calculator Tests

        [Test]
        public void MacroDistributionCalculator_BalancedSplit_ReturnsCorrectGrams()
        {
            // Arrange - 2000 kcal split as 30/40/30 protein/carbs/fats
            var calculator = new MacroDistributionCalculator
            {
                TotalCalories = 2000.0,
                ProteinPercent = 30.0,
                CarbsPercent = 40.0,
                FatsPercent = 30.0
            };

            // Act
            calculator.Calculate();

            // Assert
            // Protein: 2000 × 0.3 / 4 = 150g
            // Carbs: 2000 × 0.4 / 4 = 200g
            // Fats: 2000 × 0.3 / 9 = 66.67g
            Assert.That(calculator.ProteinGrams, Is.EqualTo(150.0).Within(0.1));
            Assert.That(calculator.CarbsGrams, Is.EqualTo(200.0).Within(0.1));
            Assert.That(calculator.FatsGrams, Is.EqualTo(66.67).Within(0.1));
        }

        [Test]
        public void MacroDistributionCalculator_PercentagesNotSumTo100_ThrowsException()
        {
            // Arrange - percentages sum to 95, not 100
            var calculator = new MacroDistributionCalculator
            {
                TotalCalories = 2000.0,
                ProteinPercent = 30.0,
                CarbsPercent = 35.0,
                FatsPercent = 30.0
            };

            // Act & Assert - should reject invalid percentage sum
            Assert.That(() => calculator.Calculate(), Throws.TypeOf<InvalidOperationException>());
        }

        [Test]
        public void MacroDistributionCalculator_NegativeCalories_ThrowsException()
        {
            // Arrange - negative total calories
            var calculator = new MacroDistributionCalculator
            {
                TotalCalories = -1000.0,
                ProteinPercent = 30.0,
                CarbsPercent = 40.0,
                FatsPercent = 30.0
            };

            // Act & Assert - should reject negative calories
            Assert.That(() => calculator.Calculate(), Throws.TypeOf<InvalidOperationException>());
        }

        // Edge Case Tests

        [Test]
        public void BMICalculator_ExtremelyTallPerson_HandlesCorrectly()
        {
            // Arrange - very tall person at upper limit
            var calculator = new BMICalculator
            {
                WeightKg = 100.0,
                HeightM = 2.4
            };

            // Act
            double result = calculator.Calculate();

            // Assert - should calculate without error
            Assert.That(result, Is.EqualTo(17.36).Within(0.01));
            Assert.That(calculator.GetCategory(), Is.EqualTo("Underweight"));
        }

        [Test]
        public void CalorieBurnCalculator_VeryHighIntensity_ReturnsHighCalories()
        {
            // Arrange - very high intensity workout
            var calculator = new CalorieBurnCalculator
            {
                DurationMinutes = 45,
                WeightKg = 85.0,
                Intensity = IntensityLevel.VeryHigh
            };

            // Act
            double result = calculator.Calculate();

            // Assert - MET 10.0 × 85kg × 0.75hr = 637.5 kcal
            Assert.That(result, Is.EqualTo(637.5).Within(0.1));
        }

        [Test]
        public void MacroDistributionCalculator_LowCarbSplit_ReturnsCorrectGrams()
        {
            // Arrange - keto-style split 30/20/50
            var calculator = new MacroDistributionCalculator
            {
                TotalCalories = 1800.0,
                ProteinPercent = 30.0,
                CarbsPercent = 20.0,
                FatsPercent = 50.0
            };

            // Act
            calculator.Calculate();

            // Assert
            // Protein: 1800 × 0.3 / 4 = 135g
            // Carbs: 1800 × 0.2 / 4 = 90g
            // Fats: 1800 × 0.5 / 9 = 100g
            Assert.That(calculator.ProteinGrams, Is.EqualTo(135.0).Within(0.1));
            Assert.That(calculator.CarbsGrams, Is.EqualTo(90.0).Within(0.1));
            Assert.That(calculator.FatsGrams, Is.EqualTo(100.0).Within(0.1));
        }
    }
}