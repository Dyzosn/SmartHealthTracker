using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using HealthTrackerApp.Models;
using HealthTrackerApp.Utilities;

namespace HealthTrackerApp.Services
{
    // Service for USDA FoodData Central API integration
    // Handles food search and nutrition data retrieval from external API
    public class USDAFoodDataService
    {
        private readonly HttpClient _httpClient;
        private const string BASE_URL = "https://api.nal.usda.gov/fdc/v1";

        public USDAFoodDataService()
        {
            _httpClient = new HttpClient();
            _httpClient.Timeout = TimeSpan.FromSeconds(30);
        }

        // Search for foods matching the query string
        // Returns list of Food objects with nutrition per 100g
        public async Task<List<Food>> SearchFoodsAsync(string query, int pageSize = 10)
        {
            try
            {
                // Build search URL with query parameters
                string url = $"{BASE_URL}/foods/search?api_key={Constants.USDA_API_KEY}&query={Uri.EscapeDataString(query)}&pageSize={pageSize}";

                // Make HTTP GET request to API
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                // Parse JSON response
                string jsonResponse = await response.Content.ReadAsStringAsync();
                JObject data = JObject.Parse(jsonResponse);

                // Extract foods array from response
                JArray foodsArray = (JArray)data["foods"];
                List<Food> foods = new List<Food>();

                // Process each food item in results
                foreach (JToken foodToken in foodsArray)
                {
                    Food food = ParseFoodFromSearchResult(foodToken);
                    if (food != null)
                    {
                        foods.Add(food);
                    }
                }

                return foods;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Failed to search foods from USDA API: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error processing USDA API response: {ex.Message}");
            }
        }

        // Parse food object from search result JSON
        // Converts API serving-based nutrition to per 100g standardisation
        private Food ParseFoodFromSearchResult(JToken foodToken)
        {
            try
            {
                // Extract basic food information
                int fdcId = foodToken["fdcId"]?.Value<int>() ?? 0;
                string foodName = foodToken["description"]?.Value<string>() ?? "Unknown Food";
                string dataType = foodToken["dataType"]?.Value<string>() ?? "";
                string category = foodToken["foodCategory"]?.Value<string>() ?? "";

                // Get serving size information if available
                double? servingSize = null;
                string servingUnit = "g";
                string householdServing = null;

                // Try to get serving size from servingSize field
                var servingSizeToken = foodToken["servingSize"];
                if (servingSizeToken != null)
                {
                    servingSize = servingSizeToken.Value<double?>();
                }

                // Try to get serving unit
                var servingUnitToken = foodToken["servingSizeUnit"];
                if (servingUnitToken != null)
                {
                    servingUnit = servingUnitToken.Value<string>();
                }

                // Try to get household serving description
                var householdToken = foodToken["householdServingFullText"];
                if (householdToken != null)
                {
                    householdServing = householdToken.Value<string>();
                }

                // Extract nutrients array
                JArray nutrients = (JArray)foodToken["foodNutrients"];
                if (nutrients == null || nutrients.Count == 0)
                {
                    return null; // Skip foods without nutrition data
                }

                // Find key nutrients (Energy, Protein, Carbs, Fats)
                double? calories = FindNutrient(nutrients, new[] { 1008, 2047 }); // Energy (kcal)
                double? protein = FindNutrient(nutrients, new[] { 1003, 2045 }); // Protein
                double? carbs = FindNutrient(nutrients, new[] { 1005, 2046 }); // Carbohydrate
                double? fats = FindNutrient(nutrients, new[] { 1004, 2048 }); // Total lipid (fat)

                // Skip if essential nutrients are missing
                if (!calories.HasValue || !protein.HasValue || !carbs.HasValue || !fats.HasValue)
                {
                    return null;
                }

                // Convert nutrition values to per 100g
                // API typically returns values per serving size, we need to standardise to per 100g
                double caloriesPer100g, proteinPer100g, carbsPer100g, fatsPer100g;

                if (servingSize.HasValue && servingSize.Value > 0)
                {
                    // Convert from per serving to per 100g
                    // Formula: (value per serving / serving size) * 100
                    caloriesPer100g = (calories.Value / servingSize.Value) * 100;
                    proteinPer100g = (protein.Value / servingSize.Value) * 100;
                    carbsPer100g = (carbs.Value / servingSize.Value) * 100;
                    fatsPer100g = (fats.Value / servingSize.Value) * 100;
                }
                else
                {
                    // If no serving size, assume values are already per 100g
                    caloriesPer100g = calories.Value;
                    proteinPer100g = protein.Value;
                    carbsPer100g = carbs.Value;
                    fatsPer100g = fats.Value;
                }

                // Create Food object with standardised per 100g nutrition
                return new Food
                {
                    FdcId = fdcId,
                    FoodName = foodName,
                    DataType = dataType,
                    Category = category,
                    CaloriesPer100g = Math.Round(caloriesPer100g, 2),
                    ProteinPer100g = Math.Round(proteinPer100g, 2),
                    CarbsPer100g = Math.Round(carbsPer100g, 2),
                    FatsPer100g = Math.Round(fatsPer100g, 2),
                    ServingSize = servingSize,
                    ServingUnit = servingUnit,
                    HouseholdServing = householdServing,
                    IsCustom = false,
                    DateAdded = DateTime.Now
                };
            }
            catch
            {
                return null; // Skip foods that can't be parsed
            }
        }

        // Helper method to find nutrient value by nutrient ID
        // USDA API uses different nutrient IDs for same nutrient in different databases
        private double? FindNutrient(JArray nutrients, int[] nutrientIds)
        {
            foreach (JToken nutrient in nutrients)
            {
                int nutrientId = nutrient["nutrientId"]?.Value<int>() ?? 0;
                if (nutrientIds.Contains(nutrientId))
                {
                    return nutrient["value"]?.Value<double>();
                }
            }
            return null;
        }

        // Get detailed nutrition information for specific food by FdcId
        // This endpoint provides more complete data than search results
        public async Task<Food> GetFoodDetailsAsync(int fdcId)
        {
            try
            {
                // Build details URL
                string url = $"{BASE_URL}/food/{fdcId}?api_key={Constants.USDA_API_KEY}";

                // Make HTTP GET request
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                // Parse JSON response
                string jsonResponse = await response.Content.ReadAsStringAsync();
                JObject data = JObject.Parse(jsonResponse);

                // Parse food from detailed response
                return ParseFoodFromDetails(data);
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Failed to get food details from USDA API: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error processing food details: {ex.Message}");
            }
        }

        // Parse food from detailed API response
        private Food ParseFoodFromDetails(JObject data)
        {
            // Similar parsing logic as search results but with more detailed fields
            int fdcId = data["fdcId"]?.Value<int>() ?? 0;
            string foodName = data["description"]?.Value<string>() ?? "Unknown Food";
            string dataType = data["dataType"]?.Value<string>() ?? "";
            string category = data["foodCategory"]?["description"]?.Value<string>() ?? "";

            // Extract serving information
            double? servingSize = data["servingSize"]?.Value<double?>();
            string servingUnit = data["servingSizeUnit"]?.Value<string>() ?? "g";
            string householdServing = data["householdServingFullText"]?.Value<string>();

            // Extract nutrients
            JArray nutrients = (JArray)data["foodNutrients"];
            double? calories = FindNutrient(nutrients, new[] { 1008, 2047 });
            double? protein = FindNutrient(nutrients, new[] { 1003, 2045 });
            double? carbs = FindNutrient(nutrients, new[] { 1005, 2046 });
            double? fats = FindNutrient(nutrients, new[] { 1004, 2048 });

            // Convert to per 100g
            double caloriesPer100g = calories ?? 0;
            double proteinPer100g = protein ?? 0;
            double carbsPer100g = carbs ?? 0;
            double fatsPer100g = fats ?? 0;

            if (servingSize.HasValue && servingSize.Value > 0)
            {
                caloriesPer100g = (caloriesPer100g / servingSize.Value) * 100;
                proteinPer100g = (proteinPer100g / servingSize.Value) * 100;
                carbsPer100g = (carbsPer100g / servingSize.Value) * 100;
                fatsPer100g = (fatsPer100g / servingSize.Value) * 100;
            }

            return new Food
            {
                FdcId = fdcId,
                FoodName = foodName,
                DataType = dataType,
                Category = category,
                CaloriesPer100g = Math.Round(caloriesPer100g, 2),
                ProteinPer100g = Math.Round(proteinPer100g, 2),
                CarbsPer100g = Math.Round(carbsPer100g, 2),
                FatsPer100g = Math.Round(fatsPer100g, 2),
                ServingSize = servingSize,
                ServingUnit = servingUnit,
                HouseholdServing = householdServing,
                IsCustom = false,
                DateAdded = DateTime.Now
            };
        }
    }
}