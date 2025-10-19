namespace HealthTrackerApp.BusinessLogic.Interfaces
{
    // Interface for classes that require input validation
    // Ensures consistent validation pattern across the application
    public interface IValidatable
    {
        // Checks if the object's current state is valid
        // Returns true if valid, false otherwise
        bool Validate();

        // Gets a descriptive error message when validation fails
        // Returns empty string if validation passes
        string GetValidationError();
    }
}