﻿namespace AdventOfCode
{
    public static class OpCodeTable
    {
        public delegate void OpCodeAction (Memory memory, params int[] parameters);
        
        //Opcode 1 adds together numbers read from two positions and stores the result in a third position.
        //The three integers immediately after the opcode tell you these three positions - the first two indicate
        //the positions from which you should read the input values, and the third indicates the position at which
        //the output should be stored.
        public static void Op1 (Memory memory, params int[] parameters)
        {
            memory[parameters[2]] = memory[parameters[0]] + memory[parameters[1]];
            memory.Pointer += 4;
        }
        
        //Opcode 2 works exactly like opcode 1, except it multiplies the two inputs instead of adding them.
        //Again, the three integers after the opcode indicate where the inputs and outputs are, not their values.
        public static void Op2 (Memory memory, params int[] parameters)
        {
            memory[parameters[2]] = memory[parameters[0]] * memory[parameters[1]];
            memory.Pointer += 4;
        }

        //Opcode 3 takes a single integer as input and saves it to the position given by its only parameter.
        public static void Op3(Memory memory, params int[] parameters)
        {
            memory[parameters[0]] = memory.Input;
            //that's why we must take pointers, not raw values, as input
            memory.Pointer += 2;
        }

        //Opcode 4 outputs the value of its only parameter.
        public static void Op4 (Memory memory, params int[] parameters)
        {
            memory.Output = memory[parameters[0]];
            memory.Pointer += 2;
        }

        //Opcode 5 is jump-if-true: if the first parameter is non-zero, it sets the instruction pointer to the value
        //from the second parameter. Otherwise, it does nothing.
        public static void Op5 (Memory memory, params int[] parameters)
        {
            if (memory[parameters[0]] != 0)
                memory.Pointer = memory[parameters[1]];
            else
                memory.Pointer += 3;
        }
        
        //Opcode 6 is jump-if-false: if the first parameter is zero, it sets the instruction pointer to the value from
        //the second parameter. Otherwise, it does nothing.
        public static void Op6(Memory memory, params int[] parameters)
        {
            if (memory[parameters[0]] == 0)
                memory.Pointer = memory[parameters[1]];
            else
                memory.Pointer += 3;
        }

        //Opcode 7 is less than: if the first parameter is less than the second parameter,
        //it stores 1 in the position given by the third parameter. Otherwise, it stores 0.
        public static void Op7 (Memory memory, params int[] parameters)
        {
            memory[parameters[2]] = memory[parameters[0]] < memory[parameters[1]] ? 1 : 0;
            memory.Pointer += 4;
        }

        //Opcode 8 is equals: if the first parameter is equal to the second parameter,
        //it stores 1 in the position given by the third parameter. Otherwise, it stores 0.
        public static void Op8(Memory memory, params int[] parameters)
        {
            memory[parameters[2]] = memory[parameters[0]] == memory[parameters[1]] ? 1 : 0;
            memory.Pointer += 4;
        }

        public static void Op99(Memory memory, params int[] parameters)
        {
            memory.Halt();
        }
    }
}