using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;

namespace AdventOfCode.IntCodes
{
    public class Memory
    {
        public List<long> MemoryRegister { get; }

        public long this[int index]
        {
            get
            {
                CheckIndex(index);
                return MemoryRegister[index];
            }
            set
            {
                CheckIndex(index);
                MemoryRegister[index] = value;
            }
        }

        //memory should be infinite
        private void CheckIndex(int index)
        {
            if (index >= MemoryRegister.Count)
            {
                int extraLength = index - MemoryRegister.Count + 1;
                MemoryRegister.AddRange(new long[extraLength]);
            }
        }

        //ugly hack, rewrite later

        private int pointer;
        public int Pointer
        {
            get => pointer;
            set => pointer = Clamp(value);
        }
        
        public bool Halted { get; private set; }
        public bool Paused { get; private set; }

        private readonly Queue<int> inputQueue = new Queue<int>();
        public int Input
        {
            get => inputQueue.Dequeue();
            set => inputQueue.Enqueue(value);
        }
        //important - each input value may be used only once
        public bool HasInput()
        {
            return inputQueue.Any();
        }

        public long Output { get; set; } = -1;
        
        public int RelativeBase { get; set; } = 0;


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
        
        //stock clamp function is accessible only in .net core
        private int Clamp(int value)
        {
            if (value < 0)
                return 0;
            if (value > MemoryRegister.Count)
                return MemoryRegister.Count;
            return value;
        }
    }
}