using FinanceTracker.Enums;
using FinanceTracker.Models;
using System.Text.Json;

namespace FinanceTracker.Utils
{
    public class JsonFileManager
    {
        public static void WriteToJson(Account account, string filePath)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(account.GetOperations(), options);
            File.WriteAllText(filePath, json);
        }

        public static List<Operation> ReadFromJson(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return new List<Operation>();
            }

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            string jsonDb = File.ReadAllText(filePath);

            return JsonSerializer.Deserialize<List<Operation>>(jsonDb, options) ?? new List<Operation>();
        }

        public static List<Category> ReadCategoriesFromJson(string filepath)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            string jsonCategories = File.ReadAllText(filepath);

            return JsonSerializer.Deserialize<List<Category>>(jsonCategories, options) ?? new List<Category>();
        }
    }
}
