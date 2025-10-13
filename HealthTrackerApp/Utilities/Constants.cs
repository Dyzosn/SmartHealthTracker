namespace HealthTrackerApp.Utilities
{
    // Application-wide constants and configuration values
    // Used for database connection, API keys, and default settings
    public static class Constants
    {
        // Database configuration
        // SQLite database file location (relative to application directory)
        public const string DATABASE_NAME = "HealthTracker.db";
        public const string CONNECTION_STRING = $"Data Source={DATABASE_NAME}";

        // USDA FoodData Central API configuration
        // API Key for accessing USDA nutrition database (FREE tier, 1000 requests/hour)
        // Registered at: https://fdc.nal.usda.gov/api-key-signup.html
        public const string USDA_API_KEY = "rScicIaOkCCGhpNbeD1T7eEAxd0G8EV2uQ7W6A5D";

        // Default values for calculations
        public const double DEFAULT_CALORIE_TARGET = 2000.0; // Daily calorie target in kcal
        public const double DEFAULT_PROTEIN_TARGET = 50.0; // Daily protein target in grammes
        public const double DEFAULT_CARBS_TARGET = 275.0; // Daily carbs target in grammes
        public const double DEFAULT_FATS_TARGET = 70.0; // Daily fats target in grammes

        // BMI category thresholds (WHO standards)
        public const double BMI_UNDERWEIGHT = 18.5;
        public const double BMI_NORMAL = 25.0;
        public const double BMI_OVERWEIGHT = 30.0;
        // Above 30.0 is considered Obese

        // Exercise intensity multipliers for calorie calculation (MET values)
        // MET = Metabolic Equivalent of Task
        public const double MET_LOW_INTENSITY = 3.0;
        public const double MET_MODERATE_INTENSITY = 5.0;
        public const double MET_HIGH_INTENSITY = 7.0;
        public const double MET_VERY_HIGH_INTENSITY = 10.0;

        // Application settings
        public const int MAX_SEARCH_RESULTS = 20; // Maximum food search results to display
        public const int DEFAULT_CHART_DAYS = 30; // Default number of days for chart display
        public const int SESSION_TIMEOUT_MINUTES = 30; // User session timeout in minutes

        // Validation ranges
        public const double MIN_WEIGHT_KG = 20.0;
        public const double MAX_WEIGHT_KG = 300.0;
        public const double MIN_HEIGHT_M = 0.5;
        public const double MAX_HEIGHT_M = 2.5;
        public const int MIN_AGE = 10;
        public const int MAX_AGE = 120;

        // Date format for display
        public const string DATE_FORMAT = "dd/MM/yyyy";
        public const string DATETIME_FORMAT = "dd/MM/yyyy HH:mm";

        // Note: In production applications, sensitive data like API keys should be stored securely using environment variables or secure configuration management systems.
        // But for this assignment, we storing the API key in Constants.cs is acceptable as it's a free public API and the key is for educational purposes only.
    }
}