namespace FileProcessingSystem
{
    public class DataEditor
    {
        // just for test using only ToUpper
        public static async Task<string> EditDataAsync(string data)
        {
            return await Task.FromResult(data.ToUpper());
        }
    }
}
