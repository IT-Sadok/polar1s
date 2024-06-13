namespace FileProcessingSystem
{
    public class DataEditor
    {
        public static async Task<string> EditDataAsync(string data)
        {
            return await Task.FromResult(data.ToUpper());
        }
    }
}
