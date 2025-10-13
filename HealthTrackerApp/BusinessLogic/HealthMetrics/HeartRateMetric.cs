using System;

namespace HealthTrackerApp.BusinessLogic.HealthMetrics
{
    // Derived class for heart rate measurements
    // Inherits from HealthMetric and implements heart rate-specific behaviour
    public class HeartRateMetric : HealthMetric
    {
        // Heart rate in beats per minute (bpm)
        public int HeartRateBpm { get; set; }

        // Implements abstract method - returns formatted heart rate display
        public override string Display()
        {
            return $"Heart Rate: {HeartRateBpm} bpm";
        }

        // Implements abstract method - validates heart rate is within safe range
        // Acceptable range: 40-200 bpm (includes athletes and during exercise)
        public override bool Validate()
        {
            // Heart rate must be within realistic human range
            if (HeartRateBpm < 40 || HeartRateBpm > 200)
            {
                return false;
            }
            return true;
        }

        // Implements abstract method - returns metric type identifier
        public override string GetMetricType()
        {
            return "HeartRate";
        }

        // Overrides virtual method to provide heart rate-specific summary
        public override string GetSummary()
        {
            return $"Heart Rate: {HeartRateBpm} bpm on {RecordedDate:dd/MM/yyyy}";
        }

        // Additional method specific to heart rate metrics
        // Returns heart rate category for adults at rest
        public string GetRestingHeartRateCategory()
        {
            // Based on resting heart rate standards for adults
            if (HeartRateBpm < 60)
                return "Athletic/Excellent"; // Well-trained athletes
            else if (HeartRateBpm < 70)
                return "Good";
            else if (HeartRateBpm < 80)
                return "Average";
            else if (HeartRateBpm < 100)
                return "Above Average";
            else
                return "High - Consider Medical Consultation";
        }

        // Calculates maximum heart rate based on age
        // Uses simple formula: 220 - age
        public int CalculateMaxHeartRate(int age)
        {
            if (age < 1 || age > 120)
            {
                throw new ArgumentException("Age must be between 1 and 120");
            }
            return 220 - age;
        }

        // Calculates target heart rate zones for exercise
        // Returns tuple of (lower bound, upper bound) for moderate intensity (50-70% max HR)
        public (int lowerBound, int upperBound) CalculateTargetHeartRateZone(int age)
        {
            int maxHR = CalculateMaxHeartRate(age);
            // Moderate intensity zone: 50-70% of max heart rate
            int lower = (int)(maxHR * 0.5);
            int upper = (int)(maxHR * 0.7);
            return (lower, upper);
        }
    }
}