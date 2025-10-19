namespace HealthTrackerApp.BusinessLogic.Interfaces
{
    // Interface for classes that perform calculations
    // Used by BMI, Calorie, CalorieBurn, and MacroDistribution calculators
    public interface ICalculatable
    {
        // Performs the calculation and returns the result
        // Throws InvalidOperationException if inputs are invalid
        double Calculate();
    }
}