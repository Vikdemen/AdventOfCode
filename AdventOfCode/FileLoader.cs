namespace AdventOfCode
{
    public static class FileLoader
    {
        private const string inputFolder = @"AdventData\";
        
        public static string[] GetData(string fileName, string folder = inputFolder)
            => System.IO.File.ReadAllLines
                (string.Concat(inputFolder, fileName));
    }
}