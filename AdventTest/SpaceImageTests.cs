using AdventOfCode.Puzzles;
using NUnit.Framework;

namespace AdventTest
{
    [TestFixture]
    public class SpaceImageTests
    {
        [Test]
        public void ImageGeneration()
        {
            string input = "123456789012";
            var image = new SpaceImage(3, 2, input);
            int[][,] result = image.ImageLayers;
            var expected = new int[2][,];
            expected[0] = new [,] {{1, 2, 3},{4, 5, 6}};
            expected[1] = new [,] {{7, 8, 9}, {0, 1, 2}};
            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void CorruptionTest()
        {
            string input = "123456789012";
            var image = new SpaceImage(3, 2, input);
            var result = image.GetLeastCorruptedLayer();
            var expected = new [,] {{1, 2, 3},{4, 5, 6}};
            CollectionAssert.AreEqual(expected, result);
        }
    }
}