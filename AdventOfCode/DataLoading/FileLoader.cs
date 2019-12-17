namespace AdventOfCode.DataLoading
{
    public class FileLoader : IDataLoader
    {
        //loads data from text file, plain and simple
        //may download it from the site by itself later
        
        //a folder where the data files lie
        public string InputFolder { get; set; } = @"AdventData\";

        public string[] GetData(string fileName) =>
            System.IO.File.ReadAllLines(string.Concat(InputFolder, fileName));
    }
}