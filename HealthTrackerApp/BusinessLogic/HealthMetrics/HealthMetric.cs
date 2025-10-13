using System;

namespace HealthTrackerApp.BusinessLogic.HealthMetrics
{
    // Abstract base class for all health metric types
    // Demonstrates polymorphism - derived classes implement specific metric behaviour
    public abstract class HealthMetric
    {
        // Common properties shared by all health metrics
        public int MetricId { get; set; }
        public int UserId { get; set; }
        public DateTime RecordedDate { get; set; }
        public string Notes { get; set; }

        // Abstract method - must be implemented by derived classes
        // Returns formatted string for display (e.g., "Weight: 70.5 kg")
        public abstract string Display();

        // Abstract method - validates the metric value is within healthy/acceptable range
        // Returns true if value is valid, false otherwise
        public abstract bool Validate();

        // Abstract method - returns the type of metric as string
        // Used for identification and filtering (e.g., "Weight", "BloodPressure")
        public abstract string GetMetricType();

        // Virtual method - can be overridden but has default implementation
        // Returns basic summary of the metric
        public virtual string GetSummary()
        {
            return $"{GetMetricType()} recorded on {RecordedDate:dd/MM/yyyy}";
        }

        // Constructor
        protected HealthMetric()
        {
            RecordedDate = DateTime.Now;
        }
    }
}