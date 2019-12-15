using System;
using AdventOfCode.IntCodes;

namespace AdventOfCode.Puzzles
{
    public class SensorBoost: Puzzle, IPuzzle
    {
        protected override string InputFile => "day9.txt";
        public override string ResultText => $"BOOST keycode is {Result.ToString()}";
        public override void Solve()
        {
            long[] program = InstructionParser.Parse(PuzzleInput[0]);
            var memory = new Memory(program);
            memory.Input = 1;
            memory.Start();
            long[] outputs = memory.OutputQueue.ToArray();
            foreach (var item in outputs)
            {
                Console.WriteLine(item.ToString());
            }
            Result = outputs[0];
        }
    }
}