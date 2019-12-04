using NUnit.Framework;
using AdventOfCode;

namespace AdventTest
{
    [TestFixture]
    public class IntCodeTests
    {
        [TestCase(new int[] {1,0,0,0,99}, new int[] {2,0,0,0,99})]
        [TestCase(new int[] {2,3,0,3,99}, new int[] {2,3,0,6,99})]
        [TestCase(new int[] {2,4,4,5,99,0}, new int[] {2,4,4,5,99,9801})]
        [TestCase(new int[] {1, 1, 1, 4, 99, 5, 6, 0, 99}, new int[] {30, 1, 1, 4, 2, 5, 6, 0, 99})]
        public void IntCodeTest(int[] input, int[] output)
        {
            IntCode computer = new IntCode(input);
            CollectionAssert.AreEqual(output, computer.OpList);
        }
    }
}