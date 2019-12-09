using System.Collections.Generic;
using static AdventOfCode.OpCodeTable;

namespace AdventOfCode
{
    //Parameter modes are stored in the same value as the instruction's opcode. The opcode is a two-digit number
    //based only on the ones and tens digit of the value, that is, the opcode is the rightmost two digits of the
    //first value in an instruction. Parameter modes are single digits, one per parameter, read right-to-left from
    //the opcode: the first parameter's mode is in the hundreds digit, the second parameter's mode is in the
    //thousands digit, the third parameter's mode is in the ten-thousands digit, and so on. Any missing modes are 0.
    public class IntCodeInstruction
    {
        private Memory memory;
        public int OpCode { get; private set; }
        private int[] parameters;
        private readonly ParameterMode[] parameterModes = new ParameterMode[3];

        private OpCodeAction opCodeAction;

        public static IntCodeInstruction InterpretInstruction(Memory memory)
        {
            return new IntCodeInstruction(memory);
        }

        private IntCodeInstruction(Memory memory)
        {
            this.memory = memory;
            //reads data from current position
            Initialise(memory[memory.Pointer]);
        }

        private void Initialise(int unparsedInstruction)
        {
            ParseInstruction(unparsedInstruction, 3);
            int numberOfParameters = ParametersForOpCode[OpCode];
            parameters = new int[numberOfParameters];
            for (int i = 0; i < numberOfParameters; i++)
            {
                ParameterMode parameterMode = parameterModes[i];
                switch (parameterMode)
                {
                    case ParameterMode.Immediate:
                        parameters[i] = memory.Pointer + i + 1;
                        break;
                    case ParameterMode.Position:
                        parameters[i] = memory[memory.Pointer + i + 1];
                        break;
                    default:
                        break;
                        //TODO throw exception
                }
            }
            
            //right now we have maximum 3 parameters
            void ParseInstruction(int input, int maxParameters)
            {
                string instruction = input.ToString();
                //adds missing zeroes
                instruction = instruction.PadLeft(maxParameters + 2, '0');
                //takes last 2 digits as opcode 
                OpCode = int.Parse(instruction.Substring(maxParameters));
                string parameterString = instruction.Remove(maxParameters);
                parameterModes[0] = ParseParameter(parameterString[2]);
                parameterModes[1] = ParseParameter(parameterString[1]);
                parameterModes[2] = ParseParameter(parameterString[0]);
                
                ParameterMode ParseParameter(char chr)
                {
                    switch (chr)
                    {
                        case('0'):
                            return ParameterMode.Position;
                        case ('1'):
                            return ParameterMode.Immediate;
                        default:
                            return ParameterMode.Invalid;
                    }
                }
            }

            //in position mode, returns the pointer to value, in immediate returns the pointer to parameter itself

            switch (OpCode)
            {
                case 1:
                    opCodeAction = Op1;
                    break;
                case 2:
                    opCodeAction = Op2;
                    break;
                case 3:
                    opCodeAction = Op3;
                    break;
                case 4:
                    opCodeAction = Op4;
                    break;
                case 5:
                    opCodeAction = Op5;
                    break;
                case 6:
                    opCodeAction = Op6;
                    break;
                case 7:
                    opCodeAction = Op7;
                    break;
                case 8:
                    opCodeAction = Op8;
                    break;
                case 99:
                    opCodeAction = Op99;
                    break;
                default:
                    opCodeAction = Op99;
                    break;
            }
        }

        public void Execute()
        {
            opCodeAction(memory, parameters);
        }

        private static readonly Dictionary<int, int> ParametersForOpCode = new Dictionary<int, int>()
        {
            [1] = 3,
            [2] = 3,
            [3] = 1,
            [4] = 1,
            [5] = 2,
            [6] = 2,
            [7] = 3,
            [8] = 3,
            [99] = 0
        };

        private enum ParameterMode
        {
            Position,
            Immediate,
            Invalid
        };
    }
}