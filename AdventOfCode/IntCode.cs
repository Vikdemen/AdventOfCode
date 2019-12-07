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
        public int[] Memory { get; private set; }
        //a copy of instruction set, modified by OPCodes
        private int pointer;
        //the instruction pointer
        private int input;
        public int Output { get; private set; } = -1;
        private bool halted;


        //I overloaded the Run method so it can accept both parsed and unparced instructions
        public void Run(string unparsedInstructions, int newInput = 0)
        {
            int[] instructions = ParseInstructions(unparsedInstructions);
            Run(instructions, newInput);
        }
        
        public void Run(int[] instructions, int newInput = 0)
        {
            
            input = newInput;
            Memory = new int [instructions.Length];
            //we copy the instructions, so memory modifications wouldn't change instructions
            instructions.CopyTo(Memory, 0);
            //initialisation
            pointer = 0;
            halted = false;
            Output = -1;
            int forward;
            while (!halted)
            { 
                //different instructions have different number of parameters
                forward = Process();
                //we start to process different instructions and parameters
                pointer += forward;
            }
        }

        //turns a string into instruction and parameter array
        private int[] ParseInstructions(string instructions)
        {
            string[] commands = instructions.Split(',');
            return Array.ConvertAll(commands, int.Parse);
        }

        //executes a command and returns the amount of steps for instruction
        private int Process()
        {
            var instr = new IntCodeInstruction(Memory[pointer]);
            int par1 = 0;
            int par2 = 0;
            int par3 = 0;
            if (instr.Step > 1)
                par1 = GetParameter(1, instr.Par1Immediate);
            if (instr.Step > 2)
                par2 = GetParameter(2, instr.Par2Immediate);
            if (instr.Step > 3)
                par3 = GetParameter(3, instr.Par3Immediate);

            //returns the position of the value
            int GetParameter(int parameterNumber, bool immediate)
            {
                return immediate ? pointer + parameterNumber : Memory[pointer + parameterNumber];
                //in immediate mode the value in the parameter itself
            }

            int forward;

            switch (instr.OpCode)
            {
                case 1:
                    forward = Op1(par1, par2, par3);
                    break;
                case 2:
                    forward = Op2(par1, par2, par3);
                    break;
                case 3:
                    forward = Op3(par1, input);
                    break;
                case 4:
                    forward = Op4(par1);
                    break;
                case 5:
                    forward = Op5(par1, par2);
                    break;
                case 6:
                    forward = Op6(par1, par2);
                    break;
                case 7:
                    forward = Op7(par1, par2, par3);
                    break;
                case 8:
                    forward = Op8(par1, par2, par3);
                    break;
                case 99:
                    forward = Halt();
                    break;
                default:
                    forward = Halt();
                    Console.WriteLine("invalid instruction");
                    break;
            }
            
            return forward;
        }

        //Opcode 1 adds together numbers read from two positions and stores the result in a third position.
        //The three integers immediately after the opcode tell you these three positions - the first two indicate
        //the positions from which you should read the input values, and the third indicates the position at which
        //the output should be stored.
        private int Op1(int par1, int par2, int par3)
        {
            Memory[par3] = Memory[par1] + Memory[par2];
            return 4;
        }
        
        //Opcode 2 works exactly like opcode 1, except it multiplies the two inputs instead of adding them.
        //Again, the three integers after the opcode indicate where the inputs and outputs are, not their values.
        private int Op2(int par1, int par2, int par3)
        {
            Memory[par3] = Memory[par1] * Memory[par2];
            return 4;
        }

        //Opcode 3 takes a single integer as input and saves it to the position given by its only parameter.
        private int Op3(int par1, int input)
        {
            Memory[par1] = input;
            return 2;
        }

        //Opcode 4 outputs the value of its only parameter.
        private int Op4(int par1)
        {
            Output = Memory[par1];
            return 2;
        }

        //Opcode 5 is jump-if-true: if the first parameter is non-zero, it sets the instruction pointer to the value
        //from the second parameter. Otherwise, it does nothing.
        private int Op5(int par1, int par2)
        {
            if (Memory[par1] != 0)
            {
                pointer = Memory[par2];
                return 0;
            }
            else
            {
                return 3;
            }
        }
        
        //Opcode 6 is jump-if-false: if the first parameter is zero, it sets the instruction pointer to the value from
        //the second parameter. Otherwise, it does nothing.
        private int Op6(int par1, int par2)
        {
            if (Memory[par1] == 0)
            {
                pointer = Memory[par2];
                return 0;
            }
            else
                return 3;
        }

        //Opcode 7 is less than: if the first parameter is less than the second parameter,
        //it stores 1 in the position given by the third parameter. Otherwise, it stores 0.
        private int Op7 (int par1, int par2, int par3)
        {
            Memory[par3] = Memory[par1] < Memory[par2] ? 1 : 0;
            return 4;
        }

        //Opcode 8 is equals: if the first parameter is equal to the second parameter,
        //it stores 1 in the position given by the third parameter. Otherwise, it stores 0.
        private int Op8(int par1, int par2, int par3)
        {
            Memory[par3] = Memory[par1] == Memory[par2] ? 1 : 0;
            return 4;
        }

        private int Halt()
        {
            Console.WriteLine("Program stopped");
            halted = true;
            return 0;
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
                    int output = Memory[0];
                    if (output == target)
                    {
                        return noun * 100 + verb;
                    }
                }
            }
            return -1;
        }
    }

    //operation of intcode
    
    //Parameter modes are stored in the same value as the instruction's opcode. The opcode is a two-digit number
    //based only on the ones and tens digit of the value, that is, the opcode is the rightmost two digits of the
    //first value in an instruction. Parameter modes are single digits, one per parameter, read right-to-left from
    //the opcode: the first parameter's mode is in the hundreds digit, the second parameter's mode is in the
    //thousands digit, the third parameter's mode is in the ten-thousands digit, and so on. Any missing modes are 0.
    public struct IntCodeInstruction
    {
        public int OpCode { get; private set; }
        public bool Par1Immediate { get; private set; }
        public bool Par2Immediate { get; private set; }
        public bool Par3Immediate { get; private set; }
        
        public int Step { get; private set; }
        
        public IntCodeInstruction(int instructionNum)
        {
            string instruction = instructionNum.ToString();
            //adds missing zeroes
            instruction = instruction.PadLeft(5, '0');
            //takes last 2 digits as opcode 
            this.OpCode = int.Parse(instruction.Substring(3));
            //remaining digits as parameter modes
            //immediate for 1, position for 0
            Par1Immediate = instruction[2] == '1';
            Par2Immediate = instruction[1] == '1';
            Par3Immediate = instruction[0] == '1';

            Step = OpCode switch
            {
                01 => 4,
                02 => 4,
                03 => 2,
                04 => 2,
                05 => 3,
                06 => 3,
                07 => 4,
                08 => 4,
                99 => 0,
                _ => 0
            };
        }
    }
}