using System.Numerics;
using AdventOfCode.Moons;
using AdventOfCode.Puzzles;
using NUnit.Framework;

namespace AdventTest
{
    [TestFixture]
    public class MoonSystemTests
    {
        [Test]
        public void CreationTest()
        {
            string[] data = 
            {
                "<x=-1, y=0, z=2>",
                "<x=2, y=-10, z=-7>",
                "<x=4, y=-8, z=8>",
                "<x=3, y=5, z=-1>"
            };
            var system = new MoonSystem(data);
            Assert.AreEqual(system.Moons[0].Position,new Vector3(-1, 0, 2));
            Assert.AreEqual(system.Moons[0].Velocity, Vector3.Zero);
            Assert.AreEqual(system.Moons[1].Position,new Vector3(2, -10, -7));
            Assert.AreEqual(system.Moons[1].Velocity, Vector3.Zero);
            Assert.AreEqual(system.Moons[2].Position,new Vector3(4, -8, 8));
            Assert.AreEqual(system.Moons[2].Velocity, Vector3.Zero);
            Assert.AreEqual(system.Moons[3].Position,new Vector3(3, 5, -1));
            Assert.AreEqual(system.Moons[3].Velocity, Vector3.Zero);
        }

        [Test]
        public void SimulationTest()
        {
            string[] data = 
            {
                "<x=-1, y=0, z=2>",
                "<x=2, y=-10, z=-7>",
                "<x=4, y=-8, z=8>",
                "<x=3, y=5, z=-1>"
            };
            var system = new MoonSystem(data);
            system.Simulate(10);
            Assert.AreEqual(system.Moons[0].Position,new Vector3(2, 1, -3));
            Assert.AreEqual(system.Moons[0].Velocity, new Vector3(-3,-2,1));
            Assert.AreEqual(system.Moons[1].Position,new Vector3(1, -8, 0));
            Assert.AreEqual(system.Moons[1].Velocity, new Vector3(-1, 1, 3));
            Assert.AreEqual(system.Moons[2].Position,new Vector3(3, -6, 1));
            Assert.AreEqual(system.Moons[2].Velocity, new Vector3(3,2,-3));
            Assert.AreEqual(system.Moons[3].Position,new Vector3(2, 0, 4));
            Assert.AreEqual(system.Moons[3].Velocity, new Vector3(1,-1,-1));
            Assert.AreEqual(179, system.TotalEnergy);
        }

        [Test]
        public void LongerSimulation()
        {
            string[] data = 
            {
                "<x=-8, y=-10, z=0>",
                "<x=5, y=5, z=10>",
                "<x=2, y=-7, z=3>",
                "<x=9, y=-8, z=-3>"
            };
            var system = new MoonSystem(data);
            system.Simulate(100);
            Assert.AreEqual(system.Moons[0].Position,new Vector3(8,-12,-9));
            Assert.AreEqual(system.Moons[0].Velocity, new Vector3(-7,3,0));
            Assert.AreEqual(system.Moons[1].Position,new Vector3(13,16,-3));
            Assert.AreEqual(system.Moons[1].Velocity, new Vector3(3,-11,-5));
            Assert.AreEqual(system.Moons[2].Position,new Vector3(-29,-11,-1));
            Assert.AreEqual(system.Moons[2].Velocity, new Vector3(-3,7,4));
            Assert.AreEqual(system.Moons[3].Position,new Vector3(16, -13,23));
            Assert.AreEqual(system.Moons[3].Velocity, new Vector3(7,1,1));
            Assert.AreEqual(1940, system.TotalEnergy);
        }

        [Test]
        public void TimeLoopTest()
        {
            var puzzle = new TimeLoop();
            puzzle.PuzzleInput = new []
            {
                "<x=-1, y=0, z=2>",
                "<x=2, y=-10, z=-7>",
                "<x=4, y=-8, z=8>",
                "<x=3, y=5, z=-1>"
            };
            puzzle.Solve();
            Assert.AreEqual(2772, puzzle.NumberOfIterations);
        }

        [Test]
        public void LongerLoopTest()
        {
            var puzzle = new TimeLoop();
            puzzle.PuzzleInput = new []
            {
                "<x=-8, y=-10, z=0>",
                "<x=5, y=5, z=10>",
                "<x=2, y=-7, z=3>",
                "<x=9, y=-8, z=-3>"
            };
            puzzle.Solve();
            Assert.AreEqual(4686774924, puzzle.NumberOfIterations);
        }
        
    }
}