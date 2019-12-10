using System.Collections.Generic;

namespace AdventOfCode.IntCodes
{
    public static class OpCodeTable
    {
        public delegate void OpCodeAction(Memory memory, params int[] parameters);

        //Opcode 1 adds together numbers read from two positions and stores the result in a third position.
        //The three integers immediately after the opcode tell you these three positions - the first two indicate
        //the positions from which you should read the input values, and the third indicates the position at which
        //the output should be stored.
        private static void Op1(Memory memory, params int[] parameters)
        {
            memory[parameters[2]] = memory[parameters[0]] + memory[parameters[1]];
            memory.Pointer += 4;
        }

        //Opcode 2 works exactly like opcode 1, except it multiplies the two inputs instead of adding them.
        //Again, the three integers after the opcode indicate where the inputs and outputs are, not their values.
        private static void Op2(Memory memory, params int[] parameters)
        {
            memory[parameters[2]] = memory[parameters[0]] * memory[parameters[1]];
            memory.Pointer += 4;
        }

        //Opcode 3 takes a single integer as input and saves it to the position given by its only parameter.
        private static void Op3(Memory memory, params int[] parameters)
        {
            memory[parameters[0]] = memory.Input;
            //that's why we must take pointers, not raw values, as input
            memory.Pointer += 2;
        }

        //Opcode 4 outputs the value of its only parameter.
        private static void Op4(Memory memory, params int[] parameters)
        {
            memory.Output = memory[parameters[0]];
            memory.Pointer += 2;
        }

        //Opcode 5 is jump-if-true: if the first parameter is non-zero, it sets the instruction pointer to the value
        //from the second parameter. Otherwise, it does nothing.
        private static void Op5(Memory memory, params int[] parameters)
        {
            if (memory[parameters[0]] != 0)
                memory.Pointer = memory[parameters[1]];
            else
                memory.Pointer += 3;
        }

        //Opcode 6 is jump-if-false: if the first parameter is zero, it sets the instruction pointer to the value from
        //the second parameter. Otherwise, it does nothing.
        private static void Op6(Memory memory, params int[] parameters)
        {
            if (memory[parameters[0]] == 0)
                memory.Pointer = memory[parameters[1]];
            else
                memory.Pointer += 3;
        }

        //Opcode 7 is less than: if the first parameter is less than the second parameter,
        //it stores 1 in the position given by the third parameter. Otherwise, it stores 0.
        private static void Op7(Memory memory, params int[] parameters)
        {
            memory[parameters[2]] = memory[parameters[0]] < memory[parameters[1]] ? 1 : 0;
            memory.Pointer += 4;
        }

        //Opcode 8 is equals: if the first parameter is equal to the second parameter,
        //it stores 1 in the position given by the third parameter. Otherwise, it stores 0.
        private static void Op8(Memory memory, params int[] parameters)
        {
            memory[parameters[2]] = memory[parameters[0]] == memory[parameters[1]] ? 1 : 0;
            memory.Pointer += 4;
        }

        private static void Op99(Memory memory, params int[] parameters)
        {
            memory.Halt();
        }

        public static readonly IDictionary<string, OpCode> OpCodes = new Dictionary<string, OpCode>
        {
            ["01"] = new OpCode(OpCodeID.Op1, 3, Op1),
            ["02"] = new OpCode(OpCodeID.Op2, 3, Op2),
            ["03"] = new OpCode(OpCodeID.Op3, 1, Op3),
            ["04"] = new OpCode(OpCodeID.Op4, 1, Op4),
            ["05"] = new OpCode(OpCodeID.Op5, 2, Op5),
            ["06"] = new OpCode(OpCodeID.Op6, 2, Op6),
            ["07"] = new OpCode(OpCodeID.Op7, 3, Op7),
            ["08"] = new OpCode(OpCodeID.Op8, 3, Op8),
            ["99"] = new OpCode(OpCodeID.Op99, 0, Op99),
        };

        public static OpCode GetOpCode(string code)
        {
            if(OpCodes.TryGetValue(code, out OpCode opCode))
                return opCode;
            else
                //TODO - throw exception
                return new OpCode(OpCodeID.Invalid, 0, Op99);
        }
        //returns the reference to the instance in the dictionary
        
        public enum OpCodeID
        {
            Op1, Op2, Op3, Op4, Op5, Op6, Op7, Op8, Op99, Invalid
        };
        
    }
}