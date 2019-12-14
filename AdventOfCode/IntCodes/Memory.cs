using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;

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

        public int Output { get; set; } = -1;
        
        public int RelativeBase { get; set; } = 0;


        public Memory(int[] instructions)
        {
            MemoryRegister = new List<int>(instructions);
        }

        public void Start()
        {
            if (Paused)
                Paused = false;
            while (!Halted && !Paused)
            { 
                var instruction = new IntCodeInstruction(this[Pointer]);
                instruction.Execute(this, Pointer);
            }
        }

        public void Resume()
        {
            Paused = false;
            Start();
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