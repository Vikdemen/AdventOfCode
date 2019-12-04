using System;
using System.Net;
using System.Net.Mime;

namespace AdventOfCode
{
    public static class AdventSolver
    {
        private const string inputFolder = @"C:\AdventOfCode\";
        
        
        public static void Main(string[] args)
        {
            while (true)
            {
                string command = Console.ReadLine();
                string result = ExecuteCommand(command);
                Console.WriteLine(result);
            }
            //infinite loop of input
            //break it by using "exit" command;
        }

        //chooses a command based on input
        public static string ExecuteCommand(string commandName)
        {
            switch (commandName)
            {
                case "calculate-fuel":
                    return CalculateFuel();
                case "calculate-fuel-r":
                    return CalculateFuelRecursive();
                case "program-alarm":
                    return ProgramAlarm();
                case "gravity-assist":
                    return GravityAssist();
                case "exit":
                    Environment.Exit(0);
                    return "exiting program";
                    //yep, it will never show that message
                default:
                    return "Invalid command";   
            }
        }

        //day 1
        public static string CalculateFuel()
        {
            string[] data = ReadFile("day1.txt");
            int result = Rocket.FuelForModules(ParseNumbers(data));
            return $"We need {result.ToString()} fuel in total";
        }

        //day 1 pt 2
        public static string CalculateFuelRecursive()
        {
            string[] data = ReadFile("day1.txt");
            int result = Rocket.FuelForModulesRecursive(ParseNumbers(data));
            return $"We need {result.ToString()} fuel in total";
        }
        
        //day 2
        public static string ProgramAlarm()
        {
            string data = ReadFile("day2.txt")[0];
            int[] commands = 
                ParseNumbers(data.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries));
            IntCode computer = new IntCode(commands);
            int result = computer.OpList[0];
            return $"Program finished, {result.ToString()}" ;
        }

        public static string GravityAssist()
        {
            string data = ReadFile("day2.txt")[0];
            int[] commands = 
                ParseNumbers(data.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries));
            int noun = 0;
            int verb = 0;
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    commands[1] = i;
                    commands[2] = j;
                    IntCode computer = new IntCode(commands);
                    int output = computer.OpList[0];
                    if (output == 19690720)
                    {
                        noun = i;
                        verb = j;
                    }
                }
            }
            int result = noun * 100 + verb;
            return $"Program finished, {result.ToString()}" ;
        }

        public static string[] ReadFile(string fileName, string folder = inputFolder)
            => System.IO.File.ReadAllLines(inputFolder + fileName);

        //turns the string array into int array
        public static int[] ParseNumbers(string[] data) => Array.ConvertAll(data, int.Parse);
        //doesn't check for invalid input yet
    }
}