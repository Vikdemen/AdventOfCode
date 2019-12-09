using AdventOfCode;
using NUnit.Framework;

namespace AdventTest
{
    [TestFixture]
    public class OrbitMapTests
    {
        [TestCase]
        public void TestMap()
        {
            string[] testMapData = new string[]
            {
                "COM)BBB",
                "BBB)CCC",
                "CCC)DDD",
                "DDD)EEE",
                "EEE)FFF",
                "BBB)GGG",
                "GGG)HHH",
                "DDD)III",
                "EEE)JJJ",
                "JJJ)KKK",
                "KKK)LLL"
            };
            var mapper = new OrbitMapper();
            mapper.GenerateMap(testMapData);
            int result = mapper.GetCheckSum();
            Assert.AreEqual(42, result);
        }
    }
}