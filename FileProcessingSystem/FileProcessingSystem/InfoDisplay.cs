using System.Collections.Concurrent;

namespace FileProcessingSystem
{
    public class InfoDisplay
    {
        public static void ShowProgress(string path)
        {
            Console.WriteLine($"Processing of '{path} is done!'");
        }

        public static void ShowCharCount(ConcurrentDictionary<string, int> charCounts)
        {
            foreach (var fileType in charCounts.Keys)
            {
                Console.WriteLine($"Character count for '{fileType}' files: {charCounts[fileType]}");
            }
        }
    }
}
