# Smart Health Tracker & Wellness Manager

This is a Windows Forms desktop app we built for tracking your daily nutrition, workouts, health measurements, and fitness goals all in one place. Think of it like a personal health diary but with actual data analysis and statistics.

## Project Information

- **Course**: 31927/32998 - Applications Development with .NET
- **Due Date**: Friday, 17 October 2025
- **Framework**: .NET 9.0 with C# Windows Forms
- **Database**: SQLite with Entity Framework Core 9.0
- **External API**: USDA FoodData Central API
- **Testing**: NUnit

## Team Members

**Muhammad Naufal Farhan Mudofi** - 25164613  
Role: Team Leader & Database Architect  
Responsibilities: Database design, Entity Framework setup, polymorphism implementation, API integration, and overall project coordination.

**Stanley Ng** - 14639482  
Role: UI/UX Developer  
Responsibilities: Designing and building all the forms, implementing data visualisations and statistical displays, making sure the interface is actually usable.

**Felixson Wongso** - 14578850  
Role: Business Logic Specialist  
Responsibilities: Writing calculator classes, implementing interfaces, delegates and events, plus all the NUnit testing.

## Features

### Core Features (6/6 Required)
1. **User Authentication** - Login and registration system
2. **Meal Tracker** - Log meals with nutrition data from USDA FoodData Central API
3. **Exercise Logger** - Track workouts with calorie burn calculations
4. **Health Metrics Monitoring** - Record weight, blood pressure, heart rate, blood sugar with statistical analysis
5. **Goal Management** - Set and track fitness/health goals with progress monitoring
6. **Reports & Analytics** - Statistical summaries and data tables showing trends

### Bonus Features (3/5 for Bonus Marks)
- Entity Framework Core 9.0 with Code-First approach and SQLite
- External API Integration with USDA FoodData Central
- Advanced Data Analysis with statistical displays

## Tech Stack

**Framework & Language**
- .NET 9.0
- C# 13
- Windows Forms

**Database**
- SQLite
- Entity Framework Core 9.0
- Code-First approach with migrations

**External APIs**
- USDA FoodData Central API (free, public domain)

**Testing**
- NUnit framework
- 5+ unit tests covering calculators and validation

**Data Visualisation**
- DataGridView for historical data
- Labels for statistical displays
- ProgressBar for visual indicators

**Additional Libraries**
- Newtonsoft.Json for JSON parsing
- HttpClient for API requests

## Installation

### Prerequisites
- Windows 10 or Windows 11
- Visual Studio 2022 or later
- .NET 9.0 SDK (included with Visual Studio 2022)
- Internet connection for NuGet packages and API access

### Setup Instructions

1. **Clone or extract the project**
```bash
   git clone https://github.com/Dyzosn/SmartHealthTracker.git
   # Or extract from ZIP file
```

2. **Open solution in Visual Studio 2022**
   - Open `HealthTrackerApp.sln`

3. **Restore NuGet packages** (automatic on first build)
   - Build → Rebuild Solution (Ctrl+Shift+B)

4. **Run the application** (F5)
   - **Database is included** - Pre-configured with schema and 10 seeded foods
   - **Test user ready** - Username: `testuser`, Password: `test123`
   - Database location: `bin/Debug/net9.0-windows/HealthTracker.db` (copied from repository on build)

5. **Start using the app**
   - Login with test user
   - Try adding health metrics, weight tracking, or exploring the dashboard

### Important Notes

- **Database file IS included in repository** - For tutor convenience, the database is pre-configured with seed data and test user
- **No migration setup needed** - Database schema already created, just build and run
- **USDA API Key included** - Already configured in `Constants.cs`, no additional setup needed
- **Australian locale** - Application uses en-AU culture (decimal separator: `.`, date format: `dd/MM/yyyy`)

### NuGet Packages

The following packages will be restored automatically:

```
Microsoft.EntityFrameworkCore.Sqlite (9.0.0)
Microsoft.EntityFrameworkCore.Tools (9.0.0)
Microsoft.EntityFrameworkCore.Design (9.0.0)
Newtonsoft.Json (13.0.3)
NUnit (4.0.1)
NUnit3TestAdapter (4.5.0)
Microsoft.NET.Test.Sdk (17.8.0)
```

## API Configuration

This project uses the USDA FoodData Central API for nutritional data. The API is free and provides access to over 300,000 foods with comprehensive nutrition information.

**API Details:**
- Provider: U.S. Department of Agriculture
- Cost: Free (public domain government data)
- Rate Limit: 1,000 requests per hour per IP address
- Documentation: https://fdc.nal.usda.gov/api-guide.html

**API Key:**

The API key is stored in `Utilities/Constants.cs`:

```csharp
public const string USDA_API_KEY = "rScicIaOkCCGhpNbeD1T7eEAxd0G8EV2uQ7W6A5D";
```

**Note:** In production applications, API keys should be stored in environment variables or secure configuration. For this project, the key is included in source code for ease of setup and testing.

**How It Works:**

1. User searches for food (e.g., "chicken breast")
2. App queries USDA API with search term
3. Results display with complete nutrition data
4. Selected foods are cached locally in SQLite database
5. Future searches check local database first to reduce API calls

**Important:** The USDA API returns nutrition values per serving size (e.g., per 284g chicken breast). Our application automatically converts all values to per 100g for consistency and comparability across different foods.

## Project Structure

```
HealthTrackerApp/
├── Models/                          # Entity classes
│   ├── User.cs
│   ├── Meal.cs
│   ├── Food.cs
│   ├── MealFood.cs
│   ├── Exercise.cs
│   ├── HealthMetricRecord.cs
│   ├── Goal.cs
│   └── Enums/
│       ├── MealType.cs
│       ├── ExerciseCategory.cs
│       ├── IntensityLevel.cs
│       └── GoalType.cs
│
├── Data/                            # Database context
│   ├── HealthTrackerContext.cs
│   └── DbInitializer.cs
│
├── BusinessLogic/                   # Core logic
│   ├── Calculators/
│   │   ├── BMICalculator.cs
│   │   ├── CalorieCalculator.cs
│   │   ├── CalorieBurnCalculator.cs
│   │   └── MacroDistributionCalculator.cs
│   ├── HealthMetrics/               # Polymorphism implementation
│   │   ├── HealthMetric.cs          # Abstract base class
│   │   ├── WeightMetric.cs
│   │   ├── BloodPressureMetric.cs
│   │   ├── HeartRateMetric.cs
│   │   └── BloodSugarMetric.cs
│   ├── Managers/
│   │   └── GoalManager.cs           # Delegates and Events
│   └── Interfaces/
│       ├── ITrackable.cs
│       ├── ICalculatable.cs
│       └── IValidatable.cs
│
├── Services/                        # API and data services
│   ├── USDAFoodDataService.cs
│   ├── MealService.cs
│   ├── ExerciseService.cs
│   ├── HealthMetricService.cs
│   └── GoalService.cs
│
├── Forms/                           # User interface
│   ├── LoginForm.cs
│   ├── RegistrationForm.cs
│   ├── DashboardForm.cs
│   ├── MealTrackerForm.cs
│   ├── ExerciseLoggerForm.cs
│   ├── HealthMetricsForm.cs
│   ├── GoalsProgressForm.cs
│   └── ReportsAnalyticsForm.cs
│
├── Utilities/
│   └── Constants.cs
│
└── HealthTrackerApp.Tests/         # Unit tests
    ├── CalculatorTests.cs
    ├── ValidationTests.cs
    └── GoalManagerTests.cs
```

## Database Schema

**Entities:**
- Users
- Meals
- Foods
- MealFoods (junction table)
- Exercises
- HealthMetricRecords
- Goals

**Relationships:**
- User has many Meals (1:N)
- User has many Exercises (1:N)
- User has many HealthMetricRecords (1:N)
- User has many Goals (1:N)
- Meals and Foods have Many-to-Many relationship through MealFoods

**Database File:** `HealthTracker.db` (SQLite, created automatically)

## Key Technical Features

### Object-Oriented Programming

**Polymorphism:**
- Abstract base class `HealthMetric`
- Derived classes: `WeightMetric`, `BloodPressureMetric`, `HeartRateMetric`, `BloodSugarMetric`
- Each override `Display()` and `Validate()` methods

**Interfaces:**
- `ICalculatable` - For classes with calculation logic
- `ITrackable` - For trackable entities
- `IValidatable` - For input validation

**Delegates and Events:**
- `GoalManager` class uses delegates and events
- Fires `OnGoalAchieved` event when user completes a goal
- Forms can subscribe to these events for notifications

**Generic Collections:**
- `List<T>` for storing collections of entities
- `Dictionary<TKey, TValue>` for lookup operations
- Used throughout the application

**LINQ with Lambda Expressions:**
- All database queries use LINQ instead of raw SQL
- Example: `meals.Where(m => m.Date == today).Sum(m => m.Calories)`
- Complex queries with grouping, ordering, and projection

### Entity Framework Core

**Approach:** Code-First with migrations

**Key Features:**
- DbContext: `HealthTrackerContext`
- 7 DbSet properties for entities
- Fluent API for relationship configuration
- Automatic database creation
- Seed data for initial foods

**Common Commands:**
```powershell
Add-Migration <MigrationName>
Update-Database
Remove-Migration
```

### External API Integration

**Service:** `USDAFoodDataService.cs`

**Functionality:**
- Search foods by name
- Retrieve detailed nutrition information
- Parse JSON responses
- Map API data to Food entities
- Convert serving-based values to per 100g
- Error handling for network issues

**Conversion Logic:**

The USDA API returns nutrition per serving size. For example, a chicken breast might have 20.42g protein per 284g serving. We convert this to per 100g:

```csharp
double proteinPer100g = (20.42 / 284) * 100; // Result: 7.19g per 100g
```

This ensures all foods are standardised and comparable.

## Running Tests

1. Open Test Explorer (View > Test Explorer)
2. Click "Run All" to execute all tests
3. Tests cover:
   - BMI calculations
   - Calorie calculations from macronutrients
   - Goal completion logic
   - Input validation
   - Calculator accuracy

All tests should pass. If any fail, check the test output for details.

## Usage

### First Time Setup

1. Run the application
2. The application starts with LoginForm
3. To create a new account:
   - Enter desired username and password in LoginForm
   - Click "Register" button
   - System checks if username is available
   - If available, RegistrationForm opens automatically
4. Complete your profile in RegistrationForm:
   - Email address (required)
   - Date of Birth (must be at least 10 years old)
   - Height in metres (e.g., 1.75 for 175cm)
   - Gender (Male, Female, or Other)
5. Click "Complete Registration"
6. Return to LoginForm and login with your new credentials

### Adding a Meal

1. Open Meal Tracker
2. Click "Add Meal"
3. Search for food using USDA database
4. Select food from results
5. Enter portion size in grams
6. Add to meal
7. Nutrition calculated automatically

### Logging Exercise

1. Open Exercise Logger
2. Select exercise type
3. Enter duration and intensity
4. Calories burned calculated automatically
5. Save to database

### Recording Health Metrics

1. Open Health Metrics
2. Select metric type (Weight, Blood Pressure, Heart Rate, Blood Sugar)
3. Enter measurement values
4. View historical data and statistical analysis in DataGridView

### Setting Goals

1. Open Goals & Progress
2. Create new goal
3. Set target value and deadline
4. Track progress automatically
5. Receive notification when goal achieved

### Viewing Reports

1. Open Reports & Analytics
2. Select date range
3. View nutrition breakdown (statistical summary)
4. View exercise summary (data tables)
5. View weight trends (DataGridView with statistics)

## Troubleshooting

**Database Issues:**
- Delete `HealthTracker.db` and restart application to recreate
- Check Package Manager Console for migration errors

**API Issues:**
- Verify internet connection
- Check API key in `Constants.cs`
- Rate limit: 1,000 requests/hour (wait 1 hour if exceeded)
- Search local database if API unavailable

**Build Issues:**
- Clean solution (Build > Clean Solution)
- Restore NuGet packages
- Rebuild solution

**Missing References:**
- Right-click solution > Restore NuGet Packages
- Ensure .NET 9.0 SDK is installed

## Code Conventions

**Naming Conventions:**
- Classes: PascalCase (BMICalculator, HealthMetric)
- Methods: PascalCase (CalculateTotalCalories, ValidateInput)
- Properties: PascalCase (TotalCalories, MealDate)
- Private fields: camelCase with underscore (_httpClient, _context)
- Constants: UPPER_SNAKE_CASE (DEFAULT_CALORIE_TARGET)

## Development Notes

### Food Data Standardisation

All foods in the database store nutrition per 100g, regardless of their original serving size from the API. This allows:
- Easy comparison between foods
- Simple portion calculations
- Consistent user experience

When a user logs a meal with 150g of chicken:
```
Protein per 100g: 7.19g
User portion: 150g
Calculation: 7.19 * (150/100) = 10.79g protein
```

### Branch Structure

- `main` - Stable releases
- `development` - Integration branch
- `naufal-database-setup` - Database and API work
- `stanley-ui-forms` - UI development
- `felix-business-logic` - Business logic and testing

## GitHub Repository

https://github.com/Dyzosn/SmartHealthTracker

## Assignment Requirements

**Core Requirements Met:**
- Polymorphism with abstract class and derived classes
- Interfaces (ICalculatable, ITrackable, IValidatable)
- LINQ queries with lambda expressions
- Generic collections (List, Dictionary)
- Delegates and events
- High cohesion, low coupling architecture
- Unit tests with NUnit

**Bonus Features Implemented:**
- Entity Framework Core 9.0
- External API integration
- Advanced data analysis and statistical displays

**The Total Marks We Targeted:** 35 + 3 bonus = 38/40 (eventually = 35/35)