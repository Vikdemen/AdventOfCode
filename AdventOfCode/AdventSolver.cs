using System;
using AdventOfCode.DataLoading;
using static AdventOfCode.ConsoleCommands;

namespace AdventOfCode
{
    public static class AdventSolver
    {
        public static bool ContinueInput { get; set; } = true;

        public static FileLoader DataLoader { get; private set; }

        public static void Main()
        {
            DataLoader = new FileLoader();
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
            Func<string> action = GetCommand(commandName);
            string result = action();
            return result;
        }
    }
}