namespace HealthTrackerApp.BusinessLogic.HealthMetrics
{
    // Derived class for blood pressure measurements
    // Inherits from HealthMetric and implements blood pressure-specific behaviour
    public class BloodPressureMetric : HealthMetric
    {
        // Systolic pressure (top number) in mmHg
        public int Systolic { get; set; }

        // Diastolic pressure (bottom number) in mmHg
        public int Diastolic { get; set; }

        // Implements abstract method - returns formatted blood pressure display
        public override string Display()
        {
            return $"Blood Pressure: {Systolic}/{Diastolic} mmHg";
        }

        // Implements abstract method - validates blood pressure is within safe range
        // Normal range: Systolic 90-180, Diastolic 60-120
        public override bool Validate()
        {
            // Check if systolic is within acceptable range
            if (Systolic < 90 || Systolic > 180)
            {
                return false;
            }

            // Check if diastolic is within acceptable range
            if (Diastolic < 60 || Diastolic > 120)
            {
                return false;
            }

            // Systolic must be greater than diastolic
            if (Systolic <= Diastolic)
            {
                return false;
            }

            return true;
        }

        // Implements abstract method - returns metric type identifier
        public override string GetMetricType()
        {
            return "BloodPressure";
        }

        // Overrides virtual method to provide blood pressure-specific summary
        public override string GetSummary()
        {
            return $"BP: {Systolic}/{Diastolic} mmHg on {RecordedDate:dd/MM/yyyy}";
        }

        // Additional method specific to blood pressure metrics
        // Returns blood pressure category based on standard guidelines
        public string GetBloodPressureCategory()
        {
            // Based on standard medical guidelines
            if (Systolic < 120 && Diastolic < 80)
                return "Normal";
            else if (Systolic < 130 && Diastolic < 80)
                return "Elevated";
            else if (Systolic < 140 || Diastolic < 90)
                return "High Blood Pressure Stage 1";
            else if (Systolic < 180 || Diastolic < 120)
                return "High Blood Pressure Stage 2";
            else
                return "Hypertensive Crisis - Seek Medical Attention";
        }

        // Calculates mean arterial pressure (MAP)
        // MAP is used to assess overall blood flow and organ perfusion
        public double CalculateMeanArterialPressure()
        {
            // MAP formula: Diastolic + (1/3 × (Systolic - Diastolic))
            return Diastolic + ((Systolic - Diastolic) / 3.0);
        }
    }
}