using System.Text;
using System.Text.Json;
using System.Buffers.Text;

namespace Warranty.Service.Common.Pagination;

public static class CursorEncoder
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    public static string Encode<T>(T cursorData) where T : notnull
    {
        var json = JsonSerializer.Serialize(cursorData, JsonOptions);
        var bytes = Encoding.UTF8.GetBytes(json);
        return Base64Url.EncodeToString(bytes);
    }

    public static T? Decode<T>(string cursor) where T : class
    {
        if (string.IsNullOrEmpty(cursor))
            return default;

        try
        {
            var bytes = Base64Url.DecodeFromChars(cursor);
            var json = Encoding.UTF8.GetString(bytes);
            return JsonSerializer.Deserialize<T>(json, JsonOptions);
        }
        catch
        {
            return default;
        }
    }
}
