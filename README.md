# Smart Health Tracker & Wellness Manager

This is a Windows Forms desktop app we built for tracking your daily nutrition, workouts, health measurements, and fitness goals all in one place. Think of it like a personal health diary but with actual data analysis and charts.

## Project Information

- **Course**: 31927/32998 - Applications Development with .NET
- **Due Date**: Friday, 17 October 2025
- **Framework**: .NET 7.0+ with C# Windows Forms
- **Database**: SQLite with Entity Framework Core
- **External API**: Edamam Nutrition Analysis API
- **Testing**: NUnit

## Team Members

**Muhammad Naufal Farhan Mudofi** - 25164613  
Role: Team Leader & Database Architect  
Responsibilities: Database design, Entity Framework setup, polymorphism implementation, API integration, and overall project coordination.

**Stanley Ng** - 14639482  
Role: UI/UX Developer  
Responsibilities: Designing and building all the forms, implementing charts and visualisations, making sure the interface is actually usable.

**Felixson Wongso** - 14578850  
Role: Business Logic Specialist  
Responsibilities: Writing calculator classes, implementing interfaces, delegates and events, plus all the NUnit testing.

## Features

### Core Features (6/6 Required)
1. **User Authentication** - Login and registration system
2. **Meal Tracker** - Log meals with nutrition data from Edamam API
3. **Exercise Logger** - Track workouts with calorie burn calculations
4. **Health Metrics Monitoring** - Record weight, blood pressure, heart rate, blood sugar
5. **Goal Management** - Set and track fitness/health goals with progress monitoring
6. **Reports & Analytics** - Visual charts and progress reports

### Bonus Features (3/5 Implemented)
- **Entity Framework + SQLite** - Database persistence
- **External API Integration** - Edamam Nutrition Analysis API
- **Advanced UI** - Charts with System.Windows.Forms.DataVisualization

## Project Structure

```
SmartHealthTracker/
├── HealthTrackerApp/           # Main Windows Forms project
│   ├── Models/                 # Entity classes
│   ├── Data/                   # DbContext and database
│   ├── BusinessLogic/          # Core logic (calculators, interfaces)
│   ├── Services/               # API and data services
│   ├── Forms/                  # UI forms
│   ├── Utilities/              # Helper classes
│   └── Resources/              # Images and icons
└── HealthTrackerApp.Tests/     # NUnit test project
```

## Technologies Used

- **C# .NET 7.0+**
- **Windows Forms** for UI
- **Entity Framework Core 7.0+** for database ORM
- **SQLite** for local database storage
- **Edamam API** for nutrition data
- **NUnit** for unit testing
- **System.Windows.Forms.DataVisualization** for charts

## Setup Instructions

### Prerequisites
- Visual Studio 2022 (Community Edition or higher)
- .NET 7.0 SDK or later
- Git for version control

### Installation Steps

1. **Clone the repository**
   ```bash
   git clone https://github.com/Dyzosn/SmartHealthTracker.git
   cd SmartHealthTracker
   ```

2. **Open in Visual Studio**
   - Open `HealthTrackerApp.sln` in Visual Studio 2022

3. **Install NuGet Packages**
   Packages will be restored automatically, but if needed:
   ```
   - Microsoft.EntityFrameworkCore.Sqlite (7.0+)
   - Microsoft.EntityFrameworkCore.Tools (7.0+)
   - Newtonsoft.Json (for API calls)
   - NUnit (3.13+)
   - NUnit3TestAdapter
   ```

4. **Database Setup**
   The database will be created automatically on first run.
   
   Alternatively, use Package Manager Console:
   ```
   Add-Migration InitialCreate
   Update-Database
   ```

5. **API Key Configuration**
   - Register for a free Edamam API key at: https://developer.edamam.com/
   - Update the API key in `Utilities/Constants.cs`:
     ```csharp
     public const string EDAMAM_APP_ID = "your_app_id";
     public const string EDAMAM_APP_KEY = "your_app_key";
     ```

6. **Run the Application**
   - Press F5 or click "Start" in Visual Studio
   - Default login credentials will be seeded (check DbInitializer.cs)

## How to Use

1. **Login/Register**: Create a new account or login with existing credentials
2. **Dashboard**: View your daily summary and quick stats
3. **Meal Tracker**: Search foods using Edamam API and log your meals
4. **Exercise Logger**: Record your workouts and view calories burned
5. **Health Metrics**: Track vital measurements (weight, BP, heart rate)
6. **Goals**: Set fitness goals and monitor your progress
7. **Reports**: View charts and analytics of your health data

## Testing

Run NUnit tests from Visual Studio Test Explorer:
- Right-click on `HealthTrackerApp.Tests` project
- Select "Run Tests"

## Key Technical Implementations

- **Polymorphism**: HealthMetric abstract class with derived classes (WeightMetric, BloodPressureMetric, etc.)
- **Interfaces**: ITrackable, ICalculatable, IValidatable
- **Delegates & Events**: GoalManager with event handling for goal achievements
- **LINQ Queries**: Complex data queries for reports and analytics
- **Generics**: Generic collections for data management
- **Entity Framework**: Code-first approach with migrations

## Development Notes

- Database file: `HealthTracker.db` (SQLite) - created in application directory
- All dates stored in UTC, displayed in local time
- API calls are rate-limited (check Edamam free tier limits)
- Charts auto-refresh when data changes

## Contributing (Team Workflow)

### Branch Strategy
- `main` - Production-ready code (final submission)
- `development` - Integration branch for merging features
- `naufal-database-setup` - Database and core models (Naufal)
- `stanley-ui-forms` - UI forms and design (Stanley)
- `felix-business-logic` - Calculators and business logic (Felix)

### Workflow
1. Create feature branch from `development`
2. Commit changes with meaningful messages
3. Push to your branch
4. Create Pull Request to `development`
5. After testing, merge `development` to `main`

**Last Updated**: October 2025  
**Version**: 1.0.0