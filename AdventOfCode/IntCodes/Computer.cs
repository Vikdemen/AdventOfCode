using System;

namespace AdventOfCode.IntCodes
{
    public class Computer : ICircuit
    {
        //An Intcode program is a list of integers separated by commas (like 1,0,0,3,99). To run one, start by looking
        //at the first integer (called position 0). Here, you will find an opcode - either 1, 2, or 99. The opcode
        //indicates what to do; for example, 99 means that the program is finished and should immediately halt.
        //Encountering an unknown opcode means something went wrong.
        private readonly Memory memoryBlock;
        public long[] MemoryRegister => memoryBlock.MemoryRegister.ToArray();
        //provides a copy, so you can't manipulate memory directly
        public int Output => (int)memoryBlock.Output;

        public Computer(long[] instructions)
        {
            memoryBlock = new Memory(instructions);
        }

        public int Run(params int[] input)
        {
            foreach (int value in input)
                memoryBlock.Input = value;
            memoryBlock.Start();
            return Output;
        }
        
        //probably bugged, need to remove/fix later;
    }

}