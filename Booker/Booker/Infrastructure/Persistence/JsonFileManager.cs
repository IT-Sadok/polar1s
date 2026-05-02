using Booker.Domain.Entity;
using System.Text.Json;

namespace Booker.Infrastructure.Persistence
{
    public class JsonFileManager
    {
        private static readonly JsonSerializerOptions _options = new()
        {
            WriteIndented = true
        };

        public static void WriteToJson(List<Book> books, string filePath)
        {
            var json = JsonSerializer.Serialize(books, _options);
            File.WriteAllText(filePath, json);
        }

        public static List<Book> ReadFromJson(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return [];
            }
            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Book>>(json, _options) ?? [];
        }
    }
}
