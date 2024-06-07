using FileProcessingSystem;

//if (args.Length < 2)
//{
//    Console.WriteLine("Usage: FileProcessingSystem <inputDir> <outputDir>");
//    return;
//}

string inputDir = "E:\\Proga\\C#\\polar1s\\FileProcessingSystem\\FileProcessingSystem\\inputFiles";
string outputDir = "E:\\Proga\\C#\\polar1s\\FileProcessingSystem\\FileProcessingSystem\\outputFiles";

var fileHandler = new FileHandler(inputDir, outputDir);
fileHandler.ProcessAllFilesAsync();