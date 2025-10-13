using System;
using System.Linq;
using HealthTrackerApp.Models;

namespace HealthTrackerApp.Data
{
    // Database initialiser to seed initial data
    // Populates database with common foods if database is empty
    public static class DbInitializer
    {
        // Initialise database with seed data
        public static void Initialise(HealthTrackerContext context)
        {
            // Ensure database is created
            context.Database.EnsureCreated();

            // Check if database already has data (skip seeding if not empty)
            if (context.Foods.Any())
            {
                return; // Database already seeded
            }

            // Seed common food items with nutritional values per 100g
            // These values are based on USDA FoodData Central database
            var foods = new Food[]
            {
                new Food
                {
                    FdcId = 171477,
                    FoodName = "Chicken Breast, Raw",
                    DataType = "SR Legacy",
                    Category = "Poultry Products",
                    CaloriesPer100g = 120,
                    ProteinPer100g = 22.5,
                    CarbsPer100g = 0,
                    FatsPer100g = 2.62,
                    ServingSize = 284,
                    ServingUnit = "g",
                    HouseholdServing = "1 BREAST",
                    IsCustom = false,
                    DateAdded = DateTime.Now
                },
                new Food
                {
                    FdcId = 168874,
                    FoodName = "Brown Rice, Cooked",
                    DataType = "SR Legacy",
                    Category = "Cereal Grains and Pasta",
                    CaloriesPer100g = 112,
                    ProteinPer100g = 2.6,
                    CarbsPer100g = 23.5,
                    FatsPer100g = 0.9,
                    ServingSize = 195,
                    ServingUnit = "g",
                    HouseholdServing = "1 CUP",
                    IsCustom = false,
                    DateAdded = DateTime.Now
                },
                new Food
                {
                    FdcId = 170379,
                    FoodName = "Broccoli, Raw",
                    DataType = "SR Legacy",
                    Category = "Vegetables and Vegetable Products",
                    CaloriesPer100g = 34,
                    ProteinPer100g = 2.82,
                    CarbsPer100g = 6.64,
                    FatsPer100g = 0.37,
                    ServingSize = 91,
                    ServingUnit = "g",
                    HouseholdServing = "1 CUP CHOPPED",
                    IsCustom = false,
                    DateAdded = DateTime.Now
                },
                new Food
                {
                    FdcId = 173424,
                    FoodName = "Banana, Raw",
                    DataType = "SR Legacy",
                    Category = "Fruits and Fruit Juices",
                    CaloriesPer100g = 89,
                    ProteinPer100g = 1.09,
                    CarbsPer100g = 22.84,
                    FatsPer100g = 0.33,
                    ServingSize = 118,
                    ServingUnit = "g",
                    HouseholdServing = "1 MEDIUM",
                    IsCustom = false,
                    DateAdded = DateTime.Now
                },
                new Food
                {
                    FdcId = 173410,
                    FoodName = "Egg, Whole, Raw",
                    DataType = "SR Legacy",
                    Category = "Dairy and Egg Products",
                    CaloriesPer100g = 143,
                    ProteinPer100g = 12.56,
                    CarbsPer100g = 0.72,
                    FatsPer100g = 9.51,
                    ServingSize = 50,
                    ServingUnit = "g",
                    HouseholdServing = "1 LARGE",
                    IsCustom = false,
                    DateAdded = DateTime.Now
                },
                new Food
                {
                    FdcId = 175167,
                    FoodName = "Salmon, Atlantic, Raw",
                    DataType = "SR Legacy",
                    Category = "Finfish and Shellfish Products",
                    CaloriesPer100g = 208,
                    ProteinPer100g = 20.42,
                    CarbsPer100g = 0,
                    FatsPer100g = 13.42,
                    ServingSize = 170,
                    ServingUnit = "g",
                    HouseholdServing = "HALF FILLET",
                    IsCustom = false,
                    DateAdded = DateTime.Now
                },
                new Food
                {
                    FdcId = 170457,
                    FoodName = "Sweet Potato, Raw",
                    DataType = "SR Legacy",
                    Category = "Vegetables and Vegetable Products",
                    CaloriesPer100g = 86,
                    ProteinPer100g = 1.57,
                    CarbsPer100g = 20.12,
                    FatsPer100g = 0.05,
                    ServingSize = 130,
                    ServingUnit = "g",
                    HouseholdServing = "1 MEDIUM",
                    IsCustom = false,
                    DateAdded = DateTime.Now
                },
                new Food
                {
                    FdcId = 170890,
                    FoodName = "Oats, Uncooked",
                    DataType = "SR Legacy",
                    Category = "Cereal Grains and Pasta",
                    CaloriesPer100g = 389,
                    ProteinPer100g = 16.89,
                    CarbsPer100g = 66.27,
                    FatsPer100g = 6.9,
                    ServingSize = 40,
                    ServingUnit = "g",
                    HouseholdServing = "HALF CUP",
                    IsCustom = false,
                    DateAdded = DateTime.Now
                },
                new Food
                {
                    FdcId = 174292,
                    FoodName = "Almonds",
                    DataType = "SR Legacy",
                    Category = "Nut and Seed Products",
                    CaloriesPer100g = 579,
                    ProteinPer100g = 21.15,
                    CarbsPer100g = 21.55,
                    FatsPer100g = 49.93,
                    ServingSize = 28,
                    ServingUnit = "g",
                    HouseholdServing = "1 OUNCE",
                    IsCustom = false,
                    DateAdded = DateTime.Now
                },
                new Food
                {
                    FdcId = 171265,
                    FoodName = "Greek Yogurt, Plain, Nonfat",
                    DataType = "SR Legacy",
                    Category = "Dairy and Egg Products",
                    CaloriesPer100g = 59,
                    ProteinPer100g = 10.19,
                    CarbsPer100g = 3.6,
                    FatsPer100g = 0.39,
                    ServingSize = 170,
                    ServingUnit = "g",
                    HouseholdServing = "1 CONTAINER",
                    IsCustom = false,
                    DateAdded = DateTime.Now
                }
            };

            // Add all foods to database
            context.Foods.AddRange(foods);
            context.SaveChanges();
        }
    }
}