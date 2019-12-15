using System;

namespace AdventOfCode.IntCodes
{
    public static class InstructionParser
    {
        //turns a string into instruction and parameter array
        public static long[] Parse(string instructions)
        {
            string[] commands = instructions.Split(',');
            return Array.ConvertAll(commands, long.Parse);
        }
    }
}