﻿using AdventOfCode.IntCode;

namespace AdventOfCode.Puzzles
{
    public class ProgramAlarm: Puzzle, IPuzzle
    {
        //--- Day 2: 1202 Program Alarm ---
        //Once you have a working computer, the first step is to restore the gravity assist program (your puzzle input)
        //to the "1202 program alarm" state it had just before the last computer caught fire. To do this, before running
        //the program, replace position 1 with the value 12 and replace position 2 with the value 2. What value is left
        //at position 0 after the program halts?
        protected override string InputFile => "day2.txt";
        public override string ResultText => $"Program finished, {Result.ToString()}" ;

        public override void Process()
        {
            string commands = PuzzleInput[0];
            Computer computer = new Computer();
            computer.Run(commands);
            Result = computer.MemoryRegister[0];
        }
    }

    public class GravityAssist: Puzzle, IPuzzle
    {
        //--- Part Two ---
        //To complete the gravity assist, you need to determine what pair of inputs produces the output 19690720.
        //The inputs should still be provided to the program by replacing the values at addresses 1 and 2, just like
        //before. In this program, the value placed in address 1 is called the noun, and the value placed in address 2
        //is called the verb. Each of the two input values will be between 0 and 99, inclusive.
        //Once the program has halted, its output is available at address 0, also just like before. Each time you try a
        //pair of inputs, make sure you first reset the computer's memory to the values in the program (your puzzle
        //input) - in other words, don't reuse memory from a previous attempt.
        //Find the input noun and verb that cause the program to produce the output 19690720. What is 100 * noun + verb?
        protected override string InputFile => "day2.txt";
        public override string ResultText => $"You need to input {Result.ToString()}" ;
        private int target = 19690720;

        public override void Process()
        {
            string commands = PuzzleInput[0];
            Computer computer = new Computer();
            Result = computer.FindNounVerb(commands, target);
        }
    }

}