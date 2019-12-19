using System.Collections.Generic;
using System.Numerics;
using AdventOfCode.Asteroids;
using NUnit.Framework;

namespace AdventTest
{
    [TestFixture]
    public class AsteroidTest
    {
        [Test]
        public void MapCreationTest()
        {
            string[] data = {
                ".#..#",
                ".....",
                "#####",
                "....#",
                "...##"
            };
            var map = new AsteroidMap(data);
            var asteroids = map.AsteroidLocations;
            var test = new HashSet<Vector2>()
            {
                new Vector2(1,0),
                new Vector2(4,0),
                new Vector2(0,2),
                new Vector2(1,2),
                new Vector2(2,2),
                new Vector2(3,2),
                new Vector2(4,2),
                new Vector2(4,3),
                new Vector2(3, 4),
                new Vector2(4,4)
            };
            CollectionAssert.AreEquivalent(test, asteroids);
        }

        [Test]
        public void BestLocationTest()
        {
            string[] data = {
                ".#..#",
                ".....",
                "#####",
                "....#",
                "...##"
            };
            var map = new AsteroidMap(data);
            Assert.AreEqual((3,4), map.BestLocation);
        }
    }
}