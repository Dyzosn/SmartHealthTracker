using System;
using System.Collections.Generic;
using System.Linq;
using HealthTrackerApp.Data;
using HealthTrackerApp.Models;
using HealthTrackerApp.BusinessLogic.HealthMetrics;

namespace HealthTrackerApp.Services
{
    // Service for health metric CRUD operations
    // Provides methods to create, read, update, and delete health metric records
    public class HealthMetricService
    {
        private readonly HealthTrackerContext _context;

        public HealthMetricService(HealthTrackerContext context)
        {
            _context = context;
        }

        // Add a new health metric record to database
        public void AddHealthMetric(HealthMetricRecord metric)
        {
            if (metric == null)
            {
                throw new ArgumentNullException(nameof(metric));
            }

            _context.HealthMetrics.Add(metric);
            _context.SaveChanges();
        }

        // Get all health metrics for a specific user
        public List<HealthMetricRecord> GetHealthMetricsByUserId(int userId)
        {
            return _context.HealthMetrics
                .Where(h => h.UserId == userId)
                .OrderByDescending(h => h.RecordedDate)
                .ToList();
        }

        // Get health metrics by user and metric type (e.g., all weight records)
        public List<HealthMetricRecord> GetHealthMetricsByType(int userId, string metricType)
        {
            return _context.HealthMetrics
                .Where(h => h.UserId == userId && h.MetricType == metricType)
                .OrderByDescending(h => h.RecordedDate)
                .ToList();
        }

        // Get health metrics within a date range
        public List<HealthMetricRecord> GetHealthMetricsByDateRange(int userId, DateTime startDate, DateTime endDate)
        {
            return _context.HealthMetrics
                .Where(h => h.UserId == userId && h.RecordedDate >= startDate && h.RecordedDate <= endDate)
                .OrderByDescending(h => h.RecordedDate)
                .ToList();
        }

        // Get latest health metric of specific type for a user
        public HealthMetricRecord GetLatestHealthMetric(int userId, string metricType)
        {
            return _context.HealthMetrics
                .Where(h => h.UserId == userId && h.MetricType == metricType)
                .OrderByDescending(h => h.RecordedDate)
                .FirstOrDefault();
        }

        // Update an existing health metric record
        public void UpdateHealthMetric(HealthMetricRecord metric)
        {
            if (metric == null)
            {
                throw new ArgumentNullException(nameof(metric));
            }

            var existingMetric = _context.HealthMetrics.Find(metric.HealthMetricId);
            if (existingMetric == null)
            {
                throw new Exception("Health metric not found");
            }

            // Update properties
            existingMetric.RecordedDate = metric.RecordedDate;
            existingMetric.MetricType = metric.MetricType;
            existingMetric.WeightKg = metric.WeightKg;
            existingMetric.Systolic = metric.Systolic;
            existingMetric.Diastolic = metric.Diastolic;
            existingMetric.HeartRateBpm = metric.HeartRateBpm;
            existingMetric.BloodSugarMmol = metric.BloodSugarMmol;
            existingMetric.Notes = metric.Notes;

            _context.SaveChanges();
        }

        // Delete a health metric record
        public void DeleteHealthMetric(int healthMetricId)
        {
            var metric = _context.HealthMetrics.Find(healthMetricId);
            if (metric == null)
            {
                throw new Exception("Health metric not found");
            }

            _context.HealthMetrics.Remove(metric);
            _context.SaveChanges();
        }

        // Get weight history for trend analysis
        public List<HealthMetricRecord> GetWeightHistory(int userId, int days)
        {
            DateTime startDate = DateTime.Now.AddDays(-days);
            return _context.HealthMetrics
                .Where(h => h.UserId == userId && h.MetricType == "Weight" && h.RecordedDate >= startDate)
                .OrderBy(h => h.RecordedDate)
                .ToList();
        }

        // Calculate average weight over a period
        public double? GetAverageWeight(int userId, int days)
        {
            DateTime startDate = DateTime.Now.AddDays(-days);
            var weights = _context.HealthMetrics
                .Where(h => h.UserId == userId && h.MetricType == "Weight" && h.RecordedDate >= startDate)
                .Select(h => h.WeightKg)
                .ToList();

            if (weights.Count == 0)
            {
                return null;
            }

            // Calculate average, filtering out null values
            var validWeights = weights.Where(w => w.HasValue).Select(w => w.Value);
            return validWeights.Any() ? validWeights.Average() : (double?)null;
        }

        // Get blood pressure statistics
        public (int? avgSystolic, int? avgDiastolic) GetAverageBloodPressure(int userId, int days)
        {
            DateTime startDate = DateTime.Now.AddDays(-days);
            var bpRecords = _context.HealthMetrics
                .Where(h => h.UserId == userId && h.MetricType == "BloodPressure" && h.RecordedDate >= startDate)
                .ToList();

            if (bpRecords.Count == 0)
            {
                return (null, null);
            }

            // Calculate averages
            var systolicValues = bpRecords.Where(b => b.Systolic.HasValue).Select(b => b.Systolic.Value);
            var diastolicValues = bpRecords.Where(b => b.Diastolic.HasValue).Select(b => b.Diastolic.Value);

            int? avgSystolic = systolicValues.Any() ? (int?)systolicValues.Average() : null;
            int? avgDiastolic = diastolicValues.Any() ? (int?)diastolicValues.Average() : null;

            return (avgSystolic, avgDiastolic);
        }

        // Convert HealthMetricRecord to appropriate polymorphic HealthMetric object
        public HealthMetric ConvertToPolymorphicMetric(HealthMetricRecord record)
        {
            if (record == null)
            {
                return null;
            }

            switch (record.MetricType)
            {
                case "Weight":
                    return new WeightMetric
                    {
                        MetricId = record.HealthMetricId,
                        UserId = record.UserId,
                        RecordedDate = record.RecordedDate,
                        Notes = record.Notes,
                        WeightKg = record.WeightKg ?? 0
                    };

                case "BloodPressure":
                    return new BloodPressureMetric
                    {
                        MetricId = record.HealthMetricId,
                        UserId = record.UserId,
                        RecordedDate = record.RecordedDate,
                        Notes = record.Notes,
                        Systolic = record.Systolic ?? 0,
                        Diastolic = record.Diastolic ?? 0
                    };

                case "HeartRate":
                    return new HeartRateMetric
                    {
                        MetricId = record.HealthMetricId,
                        UserId = record.UserId,
                        RecordedDate = record.RecordedDate,
                        Notes = record.Notes,
                        HeartRateBpm = record.HeartRateBpm ?? 0
                    };

                case "BloodSugar":
                    return new BloodSugarMetric
                    {
                        MetricId = record.HealthMetricId,
                        UserId = record.UserId,
                        RecordedDate = record.RecordedDate,
                        Notes = record.Notes,
                        BloodSugarMmol = record.BloodSugarMmol ?? 0
                    };

                default:
                    return null;
            }
        }

        // Get all metrics as polymorphic objects for polymorphism demonstration
        public List<HealthMetric> GetPolymorphicMetrics(int userId)
        {
            var records = GetHealthMetricsByUserId(userId);
            List<HealthMetric> polymorphicMetrics = new List<HealthMetric>();

            foreach (var record in records)
            {
                var metric = ConvertToPolymorphicMetric(record);
                if (metric != null)
                {
                    polymorphicMetrics.Add(metric);
                }
            }

            return polymorphicMetrics;
        }
    }
}