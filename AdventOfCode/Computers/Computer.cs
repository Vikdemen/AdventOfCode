using AdventOfCode.IntCode;

namespace AdventOfCode.Computers
{
    public class Computer : IComputer, ISimpleComputer
    {
        //computer is a wrapper class that takes input, processes it with its IntCode program and returns it;
        private Memory MemoryBlock { get; }

        public bool Finished => MemoryBlock.Halted;

        public Computer(long[] instructions)
        {
            MemoryBlock = new Memory(instructions);
        }

        public int Run(int input = 0)
        {
            MemoryBlock.InputQueue.Enqueue(input);
            MemoryBlock.Start();
            return (int) (MemoryBlock.OutputQueue.Count != 0 ? MemoryBlock.OutputQueue.Dequeue() : -1);
        }

        public long[] Run(params int[] input)
        {
            foreach (int value in input)
                MemoryBlock.InputQueue.Enqueue(value);
            MemoryBlock.Start();
            long[] output = MemoryBlock.OutputQueue.ToArray();
            MemoryBlock.OutputQueue.Clear();
            return output;
        }
    }

}