using AdventOfCode.Fuel;
using NUnit.Framework;

namespace AdventTest
{
    [TestFixture]
    public class OreProcessingTests
    {
        [Test]
        public void ReactionGeneration()
        {
            string data = "7 A, 1 E => 1 FUEL";
            var reaction = new Reaction(data);
            var expectedResult = ("FUEL", 1);
            var expectedReagents = new[] {("A", 7), ("E", 1)};
            Assert.AreEqual(expectedResult, reaction.Result);
            CollectionAssert.AreEqual(expectedReagents, reaction.Reagents);
        }

        [Test]
        public void OreProcessing()
        {
            string[] reactionList = new[]
            {
                "10 ORE => 10 A",
                "1 ORE => 1 B",
                "7 A, 1 B => 1 C",
                "7 A, 1 C => 1 D",
                "7 A, 1 D => 1 E",
                "7 A, 1 E => 1 FUEL"
            };
            
            var processor = new OreProcessor(reactionList);
            int oreToFuel = processor.OrePerFuel();
            Assert.AreEqual(31, oreToFuel);
        }
    }
}