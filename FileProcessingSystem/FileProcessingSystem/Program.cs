using FileProcessingSystem;

if (args.Length < 2)
{
    Console.WriteLine("Usage: FileProcessingSystem <inputDir> <outputDir>");
    return;
}

string inputDir = args[0];
string outputDir = args[1];

var fileHandler = new FileHandler(inputDir, outputDir);
fileHandler.ProcessAllFilesAsync();