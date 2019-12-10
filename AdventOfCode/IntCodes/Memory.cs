using System.Collections.Generic;

namespace AdventOfCode.IntCodes
{
    public class Memory
    {
        public List<int> MemoryRegister { get; }

        public int this[int index]
        {
            get => MemoryRegister[index];
            set => MemoryRegister[index] = value;
        }
        
        public bool Halted { get; private set; } = false;

        private readonly Queue<int> inputQueue = new Queue<int>();
        public int Input
        {
            get => inputQueue.Dequeue();
            set => inputQueue.Enqueue(value);
        }
        //important - each input value may be used only once

        public int Output { get; set; } = -1;

        private int pointer;
        public int Pointer
        {
            get => pointer;
            set => pointer = Clamp(value);
        }

        public Memory(int[] instructions)
        {
            MemoryRegister = new List<int> (instructions);
        }

        public void Start()
        {
            while (!Halted)
            { 
                var instruction = new IntCodeInstruction(this[Pointer]);
                instruction.Execute(this, Pointer);
            }
        }

        public void Halt()
        {
            Halted = true;
        }
        
        //stock clamp function is accesible only in .net core
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