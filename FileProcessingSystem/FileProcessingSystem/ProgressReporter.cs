namespace FileProcessingSystem
{
    public class ProgressReporter
    {
        public static void ShowProgress(string path)
        {
            Console.WriteLine($"Processing of '{path} is done!'");
        }
    }
}
