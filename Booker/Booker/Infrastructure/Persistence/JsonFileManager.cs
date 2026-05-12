using Booker.Application.Contracts;
using System.Text.Json;

namespace Booker.Infrastructure.Persistence;

public class JsonFileManager<TEntity> : IFileManager<TEntity>
{
    private static readonly JsonSerializerOptions _options = new()
    {
        WriteIndented = true
    };

    public void WriteToFile(List<TEntity> entities, string filePath)
    {
        var json = JsonSerializer.Serialize(entities, _options);
        File.WriteAllText(filePath, json);
    }

    public List<TEntity> ReadFromFile(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return [];
        }
        var json = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<List<TEntity>>(json, _options) ?? [];
    }
}