using System;
using AdventOfCode.IntCodes;

namespace AdventOfCode.Puzzles
{
    public class SensorBoost: Puzzle, IPuzzle
    {
        //--- Day 9: Sensor Boost ---
        //While BOOST (your puzzle input) is capable of boosting your sensors, for tenuous safety reasons, it refuses to
        //do so until the computer it runs on passes some checks to demonstrate it is a complete Intcode computer.
        //The computer's available memory should be much larger than the initial program. Memory beyond the initial
        //program starts with the value 0 and can be read or written like any other memory. (It is invalid to try to
        //access memory at a negative address, though.)
        //The computer should have support for large numbers. Some instructions near the beginning of the BOOST program
        //will verify this capability.
        //The BOOST program will ask for a single input; run it in test mode by providing it the value 1. It will
        //perform a series of checks on each opcode, output any opcodes (and the associated parameter modes) that seem
        //to be functioning incorrectly, and finally output a BOOST keycode.
        //Once your Intcode computer is fully functional, the BOOST program should report no malfunctioning opcodes when
        //run in test mode; it should only output a single value, the BOOST keycode. What BOOST keycode does it produce?
        
        //--- Part Two ---
        //The program runs in sensor boost mode by providing the input instruction the value 2. Once run, it will boost
        //the sensors automatically, but it might take a few seconds to complete the operation on slower hardware. In
        //sensor boost mode, the program will output a single value: the coordinates of the distress signal.
        //Run the BOOST program in sensor boost mode. What are the coordinates of the distress signal?
        
        protected override string InputFile => "day9.txt";
        public override string ResultText => $"BOOST keycode is {Result.ToString()}";
        private int initialInput;

        public SensorBoost(int initialInput)
        {
            this.initialInput = initialInput;
        }

        public override void Solve()
        {
            long[] program = InstructionParser.Parse(PuzzleInput[0]);
            var memory = new Memory(program);
            memory.Input = initialInput;
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