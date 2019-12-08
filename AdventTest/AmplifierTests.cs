using NUnit.Framework;
using AdventOfCode;
using AdventOfCode.Puzzles;

namespace AdventTest
{
    [TestFixture]
    public class AmplifierTests
    {
        [TestCase("3,23,3,24,1002,24,10,24,1002,23,-1,23,101,5,23,23,1,24,23,23,4,23,99,0,0",
            new[] {0,1,2,3,4}, 54321)]
        [TestCase("3,31,3,32,1002,32,10,32,1001,31,-2,31,1007,31,0,33,1002,33,7,33,1,33,31,31,1,32,31,31,4,31,99,0,0,0",
            new[] {1,0,4,3,2}, 65210)]
        [TestCase("3,15,3,16,1002,16,10,16,1,16,15,15,4,15,99,0,0", new[]{4,3,2,1,0}, 43210)]
        public void CheckMaxSignal(string instructions, int[] sequence, int signal)
        {
            IntCode amplifier = new IntCode();
            amplifier.ChainRun(instructions, 5, 0, sequence);
            int result = amplifier.Output;
            Assert.AreEqual(signal, result);
        }

        [TestCase(new string[1] {"3,23,3,24,1002,24,10,24,1002,23,-1,23,101,5,23,23,1,24,23,23,4,23,99,0,0"}, 54321)]
        public void CheckPuzzle(string[] data, int signal)
        {
            Puzzle puzzle = new AmplificationCircuit();
            puzzle.PuzzleInput = data;
            puzzle.Process();
            int result = puzzle.Result;
            Assert.AreEqual(signal, result);
        }
    }
}