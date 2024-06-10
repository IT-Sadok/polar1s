namespace FileProcessingSystem
{
    public class FileHandler
    {
        private readonly string _inputDir;
        private readonly string _outputDir;

        public FileHandler(string inputDir, string outputDir)
        {
            _inputDir = inputDir;
            _outputDir = outputDir;
        }

        public async void ProcessAllFilesAsync()
        {
            var files = Directory.GetFiles(_inputDir, "*.txt");
            var tasks = new List<Task>();

            foreach (var file in files)
            {
                tasks.Add(ProcessFileAsync(file));
            }

            await Task.WhenAll(tasks);
            Console.WriteLine("All files were processed!");
        }

        private async Task ProcessFileAsync(string path)
        {
            var data = await FileManager.ReadFileAsync(path);
            var editedData = await DataEditor.EditDataAsync(data);
            var outputFilePath = Path.Combine(_outputDir, Path.GetFileName(path));
            await FileManager.WriteFileAsync(outputFilePath, editedData);

            ProgressReporter.ShowProgress(path);
        }
    }
}
