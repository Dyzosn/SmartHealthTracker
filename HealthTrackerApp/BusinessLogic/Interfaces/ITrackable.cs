using System;
using System.Runtime.Intrinsics.Arm;
using HealthTrackerApp.BusinessLogic.HealthMetrics;

namespace HealthTrackerApp.BusinessLogic.Interfaces
{
    // Interface for trackable health data entries
    // Implemented by entities that are logged over time (meals, exercises, metrics)
    public interface ITrackable
    {
        // Unique identifier for this trackable entry
        int Id { get; set; }

        // The user who owns this entry
        int UserId { get; set; }

        // When this entry was recorded
        DateTime RecordedDate { get; set; }

        // Optional notes about this entry
        string Notes { get; set; }

        // Returns a formatted display string for this entry
        // Used in UI lists and summaries
        string Display();
    }
}