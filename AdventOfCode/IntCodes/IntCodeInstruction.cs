using System.Linq;
using static AdventOfCode.IntCodes.OpCodeTable;

namespace AdventOfCode.IntCodes
{
    //Parameter modes are stored in the same value as the instruction's opcode. The opcode is a two-digit number
    //based only on the ones and tens digit of the value, that is, the opcode is the rightmost two digits of the
    //first value in an instruction. Parameter modes are single digits, one per parameter, read right-to-left from
    //the opcode: the first parameter's mode is in the hundreds digit, the second parameter's mode is in the
    //thousands digit, the third parameter's mode is in the ten-thousands digit, and so on. Any missing modes are 0.
    public class IntCodeInstruction
    {
        private readonly OpCode opCode;
        private readonly ParameterMode[] parameterModes;

        public IntCodeInstruction(int instructionInt)
        {
            string instruction = instructionInt.ToString();
            //takes last 2 digits as opcode, remaining are parameter modes, in reversed order
            //instruction can be only 1 digit long, then we add zero to them
            if (instruction.Length < 2)
                instruction = instruction.PadLeft(2, '0');
            string opCodeString = instruction.Substring(instruction.Length - 2);
            opCode = GetOpCode(opCodeString);
            
            //first you need to parse opCode to get number of parameters
            string parameters = instruction.Substring(0, instruction.Length - 2);
            //adds missing zeroes to parameter string
            parameters = parameters.PadLeft(opCode.NumberOfParameters, '0');
            //order is reversed
            ParameterMode[] modes = parameters.Reverse().Select(IntToMode).ToArray();
            
            ParameterMode IntToMode(char modeID) =>
                modeID switch
                {
                    ('0') => ParameterMode.Position,
                    ('1') => ParameterMode.Immediate,
                    ('2') => ParameterMode.Relative,
                    _ => ParameterMode.Invalid
                };

            parameterModes = modes;
        }

        public void Execute(Memory memory, int pointer)
        {
            int[] parameters = FindParameters(memory, pointer);
            opCode.Action(memory, parameters);
        }

        private int[] FindParameters(Memory memory, int instructionIndex)
        {
            int[] parameters = parameterModes.Select(ValueByMode).ToArray();
            
            int ValueByMode(ParameterMode mode, int index)
            {
                int number = index + 1;
                int value = mode switch
                {
                    ParameterMode.Immediate => (instructionIndex + number),
                    ParameterMode.Position => memory[instructionIndex + number],
                    ParameterMode.Relative => memory[instructionIndex + number],
                    //implement relative
                    _ => 0
                };
                return value;
            }

            return parameters;
        }
        
        private enum ParameterMode
        {
            Invalid,
            Position,
            Immediate,
            Relative
        };
    }
}