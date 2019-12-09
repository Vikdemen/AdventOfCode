namespace AdventOfCode
{
    public static class FileLoader
    {
        private const string InputFolder = @"AdventData\";
        
        public static string[] GetData(string fileName, string folder = InputFolder)
            => System.IO.File.ReadAllLines
                (string.Concat(folder, fileName));
    }
}