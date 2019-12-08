using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Net.Configuration;

namespace AdventOfCode
{
    public class Memory
    {
        public int[] MemoryRegister { get; }
        
        public int this[int index]
        {
            get => MemoryRegister[index];
            set => MemoryRegister[index] = value;
        }

        private int pointer;
        public bool Halted { get; private set; } = false;

        private readonly Queue<int> inputQueue = new Queue<int>();
        public int Input
        {
            get => inputQueue.Dequeue();
            set => inputQueue.Enqueue(value);
        }
        //important - each input value may be used only once

        public int Output { get; set; } = -1;

        public int Pointer
        {
            get => pointer;
            set => pointer = value; //Clamp(value);
        }
        

        public Memory(int[] instructions)
        {
            MemoryRegister = new int[instructions.Length];
            instructions.CopyTo(MemoryRegister, 0);
            //we copy the instructions, so memory modifications wouldn't change instructions
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
            if (value > MemoryRegister.Length)
                return MemoryRegister.Length;
            return value;
        }
        
    }
}