using AdventOfCode.Puzzles;
using AdventOfCode.SpaceImages;
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
            int[,,] result = image.Image;
            var expected = new[,,] {{{1, 2, 3}, {4, 5, 6}}, {{7, 8, 9}, {0, 1, 2}}};
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

        [Test]
        public void PixelLayers()
        {
            string input = "0222112222120000";
            var image = new SpaceImage(2, 2, input);
            var result = image.ReadImage();
            var expected = new [,] {{0, 1}, {1, 0}};
            CollectionAssert.AreEqual(expected,result);
        }
    }
}