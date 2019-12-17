namespace AdventOfCode.DataLoading
{
    public class TestDataLoader : IDataLoader
    {
        //class for testing purposes, keeps the data wrapped

        private string[] testData;

        public TestDataLoader(string[] data)
        {
            testData = data;
        }

        public TestDataLoader(string data)
        {
            testData = new[] {data};
        }

        public string[] GetData(string fileName) =>
            testData;
    }
}