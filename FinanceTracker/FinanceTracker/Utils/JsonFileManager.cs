using FinanceTracker.Models;
using System.Text.Json;

namespace FinanceTracker.Utils
{
    public class JsonFileManager
    {
        public static void WriteToJson(List<Operation> operations, string filePath)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(operations, options);
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

            string json = File.ReadAllText(filePath);

            return JsonSerializer.Deserialize<List<Operation>>(json, options) ?? new List<Operation>();
        }
    }
}
