using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.IntCodes
{
    public class Memory
    {
        public List<long> MemoryRegister { get; }

        public long this[long index]
        {
            get
            {
                CheckIndex(index);
                return MemoryRegister[(int)index];
            }
            set
            {
                CheckIndex(index);
                MemoryRegister[(int)index] = value;
            }
        }

        //memory should be infinite
        //adds extra space when needed
        private void CheckIndex(long index)
        {
            if (index >= MemoryRegister.Count)
            {
                long extraLength = index - MemoryRegister.Count + 1;
                MemoryRegister.AddRange(new long[extraLength]);
            }
        }

        public long Pointer { get; set; }

        public bool Halted { get; private set; }
        public bool Paused { get; private set; }

        private readonly Queue<long> inputQueue = new Queue<long>();
        public int Input
        {
            get => (int)inputQueue.Dequeue();
            set => inputQueue.Enqueue(value);
        }
        //important - each input value may be used only once
        public bool HasInput()
        {
            return inputQueue.Count != 0;
        }

        public long Output
        {
            get
            {
                if (OutputQueue.Any())
                    return OutputQueue.Dequeue();
                return -1;
            }
            set => OutputQueue.Enqueue(value);
        }

        public Queue<long> OutputQueue { get; } = new Queue<long>();

        public long RelativeBase { get; set; }


        public Memory(long[] instructions)
        {
            MemoryRegister = new List<long>(instructions);
        }

        public void Start()
        {
            if (Paused)
                Paused = false;
            while (!Halted && !Paused)
            { 
                var instruction = new IntCodeInstruction((int)this[Pointer]);
                instruction.Execute(this, Pointer);
            }
        }

        public void Halt()
        {
            Halted = true;
        }

        public void Pause()
        {
            Paused = true;
        }
    }
}