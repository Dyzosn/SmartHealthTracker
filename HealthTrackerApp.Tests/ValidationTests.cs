using NUnit.Framework;
using HealthTrackerApp.BusinessLogic.Calculators;
using HealthTrackerApp.Models.Enums;

namespace HealthTrackerApp.Tests
{
    // Tests for IValidatable interface implementations
    // Ensures all calculators properly validate their inputs
    [TestFixture]
    public class ValidationTests
    {
        // BMI Calculator Validation Tests

        [Test]
        public void BMICalculator_ValidInputs_PassesValidation()
        {
            // Arrange - normal valid person
            var calculator = new BMICalculator
            {
                WeightKg = 70.0,
                HeightM = 1.75
            };

            // Act
            bool isValid = calculator.Validate();

            // Assert
            Assert.That(isValid, Is.True);
            Assert.That(calculator.GetValidationError(), Is.Empty);
        }

        [Test]
        public void BMICalculator_InvalidWeight_FailsValidation()
        {
            // Arrange - weight exceeds maximum
            var calculator = new BMICalculator
            {
                WeightKg = 500.0,
                HeightM = 1.75
            };

            // Act
            bool isValid = calculator.Validate();

            // Assert
            Assert.That(isValid, Is.False);
            Assert.That(calculator.GetValidationError(), Is.Not.Empty);
            Assert.That(calculator.GetValidationError(), Does.Contain("Weight must be between"));
        }

        [Test]
        public void BMICalculator_InvalidHeight_FailsValidation()
        {
            // Arrange - height below minimum
            var calculator = new BMICalculator
            {
                WeightKg = 70.0,
                HeightM = 0.3
            };

            // Act
            bool isValid = calculator.Validate();

            // Assert
            Assert.That(isValid, Is.False);
            Assert.That(calculator.GetValidationError(), Is.Not.Empty);
            Assert.That(calculator.GetValidationError(), Does.Contain("Height must be between"));
        }

        // Calorie Calculator Validation Tests

        [Test]
        public void CalorieCalculator_NegativeMacros_FailsValidation()
        {
            // Arrange - negative protein value
            var calculator = new CalorieCalculator
            {
                Protein = -10.0,
                Carbs = 50.0,
                Fats = 10.0
            };

            // Act
            bool isValid = calculator.Validate();

            // Assert
            Assert.That(isValid, Is.False);
            Assert.That(calculator.GetValidationError(), Is.Not.Empty);
            Assert.That(calculator.GetValidationError(), Does.Contain("cannot be negative"));
        }

        [Test]
        public void CalorieCalculator_UnrealisticValues_FailsValidation()
        {
            // Arrange - protein exceeds 100g per 100g food (impossible)
            var calculator = new CalorieCalculator
            {
                Protein = 150.0,
                Carbs = 50.0,
                Fats = 10.0
            };

            // Act
            bool isValid = calculator.Validate();

            // Assert
            Assert.That(isValid, Is.False);
            Assert.That(calculator.GetValidationError(), Is.Not.Empty);
            Assert.That(calculator.GetValidationError(), Does.Contain("unrealistic"));
        }

        // Calorie Burn Calculator Validation Tests

        [Test]
        public void CalorieBurnCalculator_ExcessiveDuration_FailsValidation()
        {
            // Arrange - duration over 8 hours (unrealistic for single workout)
            var calculator = new CalorieBurnCalculator
            {
                DurationMinutes = 500,
                WeightKg = 70.0,
                Intensity = IntensityLevel.Moderate
            };

            // Act
            bool isValid = calculator.Validate();

            // Assert
            Assert.That(isValid, Is.False);
            Assert.That(calculator.GetValidationError(), Is.Not.Empty);
        }

        [Test]
        public void CalorieBurnCalculator_InvalidWeight_FailsValidation()
        {
            // Arrange - weight below realistic minimum
            var calculator = new CalorieBurnCalculator
            {
                DurationMinutes = 30,
                WeightKg = 10.0,
                Intensity = IntensityLevel.Moderate
            };

            // Act
            bool isValid = calculator.Validate();

            // Assert
            Assert.That(isValid, Is.False);
            Assert.That(calculator.GetValidationError(), Does.Contain("Weight must be between"));
        }

        // Macro Distribution Calculator Validation Tests

        [Test]
        public void MacroDistributionCalculator_ValidPercentages_PassesValidation()
        {
            // Arrange - percentages sum to 100
            var calculator = new MacroDistributionCalculator
            {
                TotalCalories = 2000.0,
                ProteinPercent = 30.0,
                CarbsPercent = 40.0,
                FatsPercent = 30.0
            };

            // Act
            bool isValid = calculator.Validate();

            // Assert
            Assert.That(isValid, Is.True);
            Assert.That(calculator.GetValidationError(), Is.Empty);
        }

        [Test]
        public void MacroDistributionCalculator_PercentageSumNot100_FailsValidation()
        {
            // Arrange - percentages sum to 90
            var calculator = new MacroDistributionCalculator
            {
                TotalCalories = 2000.0,
                ProteinPercent = 30.0,
                CarbsPercent = 30.0,
                FatsPercent = 30.0
            };

            // Act
            bool isValid = calculator.Validate();

            // Assert
            Assert.That(isValid, Is.False);
            Assert.That(calculator.GetValidationError(), Does.Contain("must sum to 100"));
        }

        [Test]
        public void MacroDistributionCalculator_NegativePercentage_FailsValidation()
        {
            // Arrange - negative protein percentage
            var calculator = new MacroDistributionCalculator
            {
                TotalCalories = 2000.0,
                ProteinPercent = -10.0,
                CarbsPercent = 60.0,
                FatsPercent = 50.0
            };

            // Act
            bool isValid = calculator.Validate();

            // Assert
            Assert.That(isValid, Is.False);
            Assert.That(calculator.GetValidationError(), Does.Contain("between 0 and 100"));
        }

        // Edge Case Validation Tests

        [Test]
        public void BMICalculator_BoundaryWeightMinimum_PassesValidation()
        {
            // Arrange - weight at minimum boundary (20kg)
            var calculator = new BMICalculator
            {
                WeightKg = 20.0,
                HeightM = 1.5
            };

            // Act
            bool isValid = calculator.Validate();

            // Assert - should accept boundary value
            Assert.That(isValid, Is.True);
        }

        [Test]
        public void BMICalculator_BoundaryWeightMaximum_PassesValidation()
        {
            // Arrange - weight at maximum boundary (300kg)
            var calculator = new BMICalculator
            {
                WeightKg = 300.0,
                HeightM = 2.0
            };

            // Act
            bool isValid = calculator.Validate();

            // Assert - should accept boundary value
            Assert.That(isValid, Is.True);
        }

        [Test]
        public void CalorieBurnCalculator_OneDurationMinute_PassesValidation()
        {
            // Arrange - minimum valid duration
            var calculator = new CalorieBurnCalculator
            {
                DurationMinutes = 1,
                WeightKg = 70.0,
                Intensity = IntensityLevel.Low
            };

            // Act
            bool isValid = calculator.Validate();

            // Assert - should accept minimum duration
            Assert.That(isValid, Is.True);
        }
    }
}