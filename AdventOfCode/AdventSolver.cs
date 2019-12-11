using System;
using AdventOfCode.Passwords;
using AdventOfCode.Puzzles;
using static AdventOfCode.ConsoleCommands;

namespace AdventOfCode
{
    public static class AdventSolver
    {
        public static bool ContinueInput { get; set; } = true;

        public static void Main()
        {
            while (ContinueInput)
            {
                string command = Console.ReadLine();
                string result = ExecuteCommand(command);
                Console.WriteLine(result);
            }
            //infinite loop of input
            //break it by using "exit" command;
        }

        //chooses a command based on input, executes it and returns result.
        private static string ExecuteCommand(string commandName)
        {
            CommandID commandID = GetCommand(commandName);
            CommandAction action = GetAction(commandID);
            string result = action();
            return result;
        }
    }
}