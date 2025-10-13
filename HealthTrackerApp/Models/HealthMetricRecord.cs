using System;
using System.ComponentModel.DataAnnotations;

namespace HealthTrackerApp.Models
{
    // Represents a health measurement record (weight, blood pressure, heart rate, blood sugar)
    // Each record stores one type of metric with specific fields populated accordingly
    public class HealthMetricRecord
    {
        [Key]
        public int HealthMetricId { get; set; }

        [Required]
        // Foreign key to User table
        public int UserId { get; set; }

        [Required]
        public DateTime RecordedDate { get; set; }

        [Required]
        [MaxLength(50)]
        // Type of metric: "Weight", "BloodPressure", "HeartRate", "BloodSugar"
        public string MetricType { get; set; }

        // Weight measurement in kilogrammes
        // Populated only when MetricType is "Weight"
        public double? WeightKg { get; set; }

        // Blood pressure measurements in mmHg
        // Populated only when MetricType is "BloodPressure"
        // Systolic is the top number (e.g., 120 in "120/80")
        public int? Systolic { get; set; }

        // Diastolic is the bottom number (e.g., 80 in "120/80")
        public int? Diastolic { get; set; }

        // Heart rate measurement in beats per minute
        // Populated only when MetricType is "HeartRate"
        public int? HeartRateBpm { get; set; }

        // Blood sugar measurement in mmol/L (millimoles per litre)
        // Populated only when MetricType is "BloodSugar"
        public double? BloodSugarMmol { get; set; }

        [MaxLength(500)]
        // Optional notes about the measurement (e.g., "After exercise", "Fasting")
        public string Notes { get; set; }

        // Navigation property - virtual for lazy loading
        // Reference to the user who recorded this metric
        public virtual User User { get; set; }

        public HealthMetricRecord()
        {
            RecordedDate = DateTime.Now;
        }
    }
}