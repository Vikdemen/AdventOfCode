using AdventOfCode.Computers;
using AdventOfCode.Painting;
using NUnit.Framework;

namespace AdventTest
{
    [TestFixture]
    public class PaintingRobotTest
    {
        [Test]
        public void RobotTest()
        {
            var robot = new PaintingRobot(new long[] {0});
            var brain = new TestComputer(1, 0, 0, 0, 1, 0, 1, 0, 0, 1, 1, 0, 1, 0);
            robot.Brain = brain;
            robot.Start(); 
            int count = robot.PaintedCount;
            Assert.AreEqual(6, count);
        }
    }
}