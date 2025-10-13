namespace HealthTrackerApp.BusinessLogic.HealthMetrics
{
    // Derived class for blood sugar measurements
    // Inherits from HealthMetric and implements blood sugar-specific behaviour
    public class BloodSugarMetric : HealthMetric
    {
        // Blood sugar level in mmol/L (millimoles per litre)
        public double BloodSugarMmol { get; set; }

        // Indicates if measurement was taken while fasting
        public bool IsFasting { get; set; }

        // Implements abstract method - returns formatted blood sugar display
        public override string Display()
        {
            string fastingStatus = IsFasting ? "(Fasting)" : "(Non-fasting)";
            return $"Blood Sugar: {BloodSugarMmol:F1} mmol/L {fastingStatus}";
        }

        // Implements abstract method - validates blood sugar is within measurable range
        // Acceptable range: 2.0-30.0 mmol/L (covers hypoglycemia to severe hyperglycemia)
        public override bool Validate()
        {
            // Blood sugar must be within realistic range
            if (BloodSugarMmol < 2.0 || BloodSugarMmol > 30.0)
            {
                return false;
            }
            return true;
        }

        // Implements abstract method - returns metric type identifier
        public override string GetMetricType()
        {
            return "BloodSugar";
        }

        // Overrides virtual method to provide blood sugar-specific summary
        public override string GetSummary()
        {
            return $"Blood Sugar: {BloodSugarMmol:F1} mmol/L on {RecordedDate:dd/MM/yyyy}";
        }

        // Additional method specific to blood sugar metrics
        // Returns blood sugar category based on fasting or non-fasting status
        public string GetBloodSugarCategory()
        {
            if (IsFasting)
            {
                // Fasting blood sugar categories (8+ hours fasting)
                if (BloodSugarMmol < 4.0)
                    return "Low (Hypoglycemia)";
                else if (BloodSugarMmol < 5.6)
                    return "Normal";
                else if (BloodSugarMmol < 7.0)
                    return "Prediabetes";
                else
                    return "Diabetes Range";
            }
            else
            {
                // Non-fasting blood sugar categories (2 hours after meal)
                if (BloodSugarMmol < 4.0)
                    return "Low (Hypoglycemia)";
                else if (BloodSugarMmol < 7.8)
                    return "Normal";
                else if (BloodSugarMmol < 11.1)
                    return "Prediabetes";
                else
                    return "Diabetes Range";
            }
        }

        // Checks if blood sugar indicates diabetes risk
        public bool IsDiabetesRisk()
        {
            // Diabetes risk if fasting >= 7.0 or non-fasting >= 11.1
            if (IsFasting && BloodSugarMmol >= 7.0)
                return true;
            if (!IsFasting && BloodSugarMmol >= 11.1)
                return true;
            return false;
        }

        // Converts blood sugar from mmol/L to mg/dL (US standard)
        public double ConvertToMgDl()
        {
            // Conversion factor: 1 mmol/L = 18.018 mg/dL
            return BloodSugarMmol * 18.018;
        }
    }
}