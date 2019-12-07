using System;
using System.Collections.Generic;
using System.Threading;

namespace AdventOfCode
{
    public class IntCode
    {
        //An Intcode program is a list of integers separated by commas (like 1,0,0,3,99). To run one, start by looking
        //at the first integer (called position 0). Here, you will find an opcode - either 1, 2, or 99. The opcode
        //indicates what to do; for example, 99 means that the program is finished and should immediately halt.
        //Encountering an unknown opcode means something went wrong.
        public Memory MemoryBlock { get; private set; }
        //a copy of instruction set, modified by OPCodes
        public int[] MemoryRegister => MemoryBlock.MemoryRegister;
        public int Output => MemoryBlock.Output;


        //I overloaded the Run method so it can accept both parsed and unparced instructions
        public void Run(string unparsedInstructions, int input = 0)
        {
            int[] instructions = ParseInstructions(unparsedInstructions);
            Run(instructions, input);
        }
        
        public void Run(int[] instructions, int input = 0)
        {
            MemoryBlock = new Memory(instructions) {Input = input};
            while (!MemoryBlock.Halted)
            { 
                Process();
            }
        }
        
        private void Process()
        {
            var instruction = IntCodeInstruction.InterpretInstruction(MemoryBlock);
            instruction.Execute();
        }
        
        //turns a string into instruction and parameter array
        private int[] ParseInstructions(string instructions)
        {
            string[] commands = instructions.Split(',');
            return Array.ConvertAll(commands, int.Parse);
        }

        //iterates through combinations of instructions, looking for those which would output the target number,
        //returning the first one as 4-digit number - a combination of 2d and 3rd positions in program
        //returns -1 if no such combinations are found
        public int FindNounVerb(string unparsedInstructions, int target)
        { 
            int[] instructions = ParseInstructions(unparsedInstructions);
            for (int noun = 0; noun < 100; noun++)
            {
                for (int verb = 0; verb < 100; verb++)
                {
                    instructions[1] = noun;
                    instructions[2] = verb;
                    Run(instructions);
                    int output = MemoryBlock[0];
                    if (output == target)
                    {
                        return noun * 100 + verb;
                    }
                }
            }
            return -1;
        }
    }

}