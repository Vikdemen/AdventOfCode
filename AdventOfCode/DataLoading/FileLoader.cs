namespace AdventOfCode.DataLoading
{
    public class FileLoader : IDataLoader
    {
        private const string InputFolder = @"AdventData\";
        
        public string[] GetData(string fileName)
            => System.IO.File.ReadAllLines
                (string.Concat(InputFolder, fileName));
    }
}