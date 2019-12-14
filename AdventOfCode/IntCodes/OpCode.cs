using static AdventOfCode.IntCodes.OpCodeTable;

namespace AdventOfCode.IntCodes
{
    public class OpCode
    {
        public readonly OpCodeID CodeId;
        public readonly int NumberOfParameters;
        public readonly OpCodeAction Action;
             
        public OpCode(OpCodeID codeId, int numberOfParameters, OpCodeAction action)
        {
            CodeId = codeId;
            NumberOfParameters = numberOfParameters;
            Action = action;
        }
    }
}