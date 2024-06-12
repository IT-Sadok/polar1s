using System.Collections.Concurrent;

namespace FileProcessingSystem
{
    public class FileHandler
    {
        const int THREADS = 10; // number of threads that can be created concurrently

        private readonly string _inputDir;
        private readonly string _outputDir;
        private readonly ConcurrentDictionary<string, int> _charCounts;
        private readonly SemaphoreSlim _semaphore;

        public FileHandler(string inputDir, string outputDir)
        {
            _inputDir = inputDir;
            _outputDir = outputDir;
            _charCounts = new ConcurrentDictionary<string, int>();
            _semaphore = new SemaphoreSlim(THREADS);
        }

        public async Task ProcessAllFilesAsync()
        {
            var files = Directory.GetFiles(_inputDir, "*.*");

            if (files.Length == 0)
            {
                Console.WriteLine("No files to process!");
                return;
            }

            var tasks = new List<Task>();

            foreach (var file in files)
            {
                tasks.Add(ProcessFileAsync(file));
            }

            await Task.WhenAll(tasks);
            Console.WriteLine("All files were processed!\n");

            InfoDisplay.ShowCharCount(_charCounts);
        }

        private async Task ProcessFileAsync(string path)
        {
            await _semaphore.WaitAsync();

            try
            {
                var data = await FileManager.ReadFileAsync(path);
                var content = await DataEditor.EditDataAsync(data);
                var outputFilePath = Path.Combine(_outputDir, Path.GetFileName(path));
                await FileManager.WriteFileAsync(outputFilePath, content);

                CountCharacters(Path.GetExtension(path), content);
                InfoDisplay.ShowProgress(path);
            }
            finally
            {
                _semaphore.Release();
            }


        }

        private void CountCharacters(string fileType, string content)
        {
            var charCount = content.Count(c => (!Char.IsControl(c) && !Char.IsWhiteSpace(c)));

            _charCounts.AddOrUpdate(
                fileType,
                charCount,
                (key, value) => value + charCount
                );
        }
    }
}
