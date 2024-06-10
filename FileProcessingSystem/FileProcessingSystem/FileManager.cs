namespace FileProcessingSystem
{
    public class FileManager
    {
        public static async Task<string> ReadFileAsync(string path)
        {
            using (var reader = new StreamReader(path))
            {
                return await reader.ReadToEndAsync();
            }
        }

        public static async Task WriteFileAsync(string path, string data)
        {
            using(var writer = new StreamWriter(path))
            {
                writer.AutoFlush = true;
                await writer.WriteAsync(data);
                
            }
        }
    }
}
