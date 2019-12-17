using AdventOfCode;
using AdventOfCode.Orbits;
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

        [Test]
        public void OrbitalTransfer()
        {
            string[] testMapData = {
                "COM)B",
                "B)C",
                "C)D",
                "D)E",
                "E)F",
                "B)G",
                "G)H",
                "D)I",
                "E)J",
                "J)K",
                "K)L",
                "K)YOU",
                "I)SAN"
            };
            var mapper = new OrbitMapper();
            mapper.GenerateMap(testMapData);
            int result = mapper.CountTransfers("YOU", "SAN");
            //we count transfers between bodies you and santa orbit
            result -= 2;
            Assert.AreEqual(4, result);
        }
    }
}