using System.Collections.Generic;
using System.Linq;
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
        private IntCodeOperation intCodeOperation;
        private ParameterMode[] parameterModes;

        public IntCodeInstruction(int instruction)
        {
            var parsed = ParseInstruction(instruction);
            intCodeOperation = parsed.InstructionCodeStruct;
            parameterModes = parsed.Modes;
        }
        
        private (IntCodeOperation InstructionCodeStruct, ParameterMode[] Modes) ParseInstruction(int input)
        {
            const int maxParameters = 3;
                
            string instruction = input.ToString();
                
            //adds missing zeroes
            instruction = instruction.PadLeft(maxParameters + 2, '0');
            //takes last 2 digits as opcode, remaining are parameter modes, in reversed order
            string opCode = instruction.Substring(maxParameters);
            string parameters = instruction.Substring(0, maxParameters);
                
            IntCodeOperation instructionCode = GetOpCodeStruct(opCode);
                

            ParameterMode[] modes = ParseParameters(parameters);
                
            ParameterMode[] ParseParameters(string unparsedParameters)
            {
                ParameterMode[] parameterModes = unparsedParameters.Reverse().Select(ParseParameter).ToArray();
                    
                ParameterMode ParseParameter(char unparsedParameter) =>
                    unparsedParameter switch
                    {
                        ('0') => ParameterMode.Position,
                        ('1') => ParameterMode.Immediate,
                        ('2') => ParameterMode.Relative,
                        _ => ParameterMode.Invalid
                    };
                    
                return parameterModes;
            }
            return (instructionCode, modes);
        }

        public void Execute(Memory memory, int pointer)
        {
            int[] parameters = FindParameters(memory, pointer);
            intCodeOperation.Action(memory, parameters);
        }

        private int[] FindParameters(Memory memory, int instructionIndex)
        {
            int numberOfParameters = intCodeOperation.NumberOfParameters;
            int[] parameters = new int[numberOfParameters];
            for (int i = 0; i < numberOfParameters; i++)
            {
                ParameterMode parameterMode = parameterModes[i];
                switch (parameterMode)
                {
                    case ParameterMode.Immediate:
                        parameters[i] = instructionIndex + i + 1;
                        break;
                    case ParameterMode.Position:
                        parameters[i] = memory[instructionIndex + i + 1];
                        break;
                    default:
                        break;
                    //TODO throw exception
                }
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