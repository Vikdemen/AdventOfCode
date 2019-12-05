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
                    return CalculateFuel(ReadFile("day1.txt"));
                case "calculate-fuel-r":
                    return CalculateFuelRecursive(ReadFile("day1.txt"));
                case "program-alarm":
                    return ProgramAlarm(ReadFile("day2.txt"));
                case "gravity-assist":
                    return GravityAssist(ReadFile("day2.txt"));
                case "crossed-wires":
                    return GetInterSection(ReadFile("day3.txt"));
                case "signal-delay":
                    return SmallestDelay(ReadFile("day3.txt"));
                case "secure-container":
                    return SecureContainer(ReadFile("day4.txt"));
                case "more-secure":
                    return MoreSecure(ReadFile("day4.txt"));
                case "exit":
                    Environment.Exit(0);
                    return "exiting program";
                    //yep, it will never show that message
                default:
                    return "Invalid command";   
            }
        }

        //day 1
        public static string CalculateFuel(string[] data)
        {
            int result = Rocket.FuelForModules(ParseNumbers(data));
            return $"We need {result.ToString()} fuel in total";
        }

        //day 1 pt 2
        public static string CalculateFuelRecursive(string[] data)
        {
            int result = Rocket.FuelForModulesRecursive(ParseNumbers(data));
            return $"We need {result.ToString()} fuel in total";
        }
        
        //day 2
        public static string ProgramAlarm(string[] data)
        {
            int[] commands = 
                ParseNumbers(data[0].Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries));
            IntCode computer = new IntCode(commands);
            int result = computer.OpList[0];
            return $"Program finished, {result.ToString()}" ;
        }

        public static string GravityAssist(string[] data)
        {
            int[] commands = 
                ParseNumbers(data[0].Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries));
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

        public static string GetInterSection(string[] data)
        {
            Wire wire0 = new Wire(data[0]);
            Wire wire1 = new Wire(data[1]);
            int result = Wire.GetClosest(Wire.Intersect(wire0, wire1));
            if (result == -1)
            {
                return $"No intersections found";
            }
            return $"Distance to closest intersection is {result.ToString()}";
        }

        public static string SmallestDelay(string[] data)
        {
            Wire wire0 = new Wire(data[0]);
            Wire wire1 = new Wire(data[1]);
            var intersections = Wire.Intersect(wire0, wire1);
            int result = 100500;
            foreach (var intersection in intersections)
            {
                int delay = Wire.SignalDelay(wire0, wire1, intersection);
                if (delay < result)
                    result = delay;
            }

            if (result == 100500)
                return "Can't determine the signal delay";
            return $"Smallest delay is {result.ToString()}";
        }

        public static string SecureContainer(string[] data)
        {
            data = data[0].Split(new char[] {'-'});
            int start = int.Parse(data[0]);
            int finish = int.Parse(data[1]);
            int result = Password.CountValid(start, finish);
            return $"{result} passwords are valid";
        }

        public static string MoreSecure(string[] data)
        {
            data = data[0].Split(new char[] {'-'});
            int start = int.Parse(data[0]);
            int finish = int.Parse(data[1]);
            int result = Password.CountMoreValid(start, finish);
            return $"{result} passwords are valid";
        }

        public static string[] ReadFile(string fileName, string folder = inputFolder)
            => System.IO.File.ReadAllLines(inputFolder + fileName);
        //turns the string array into int array
        public static int[] ParseNumbers(string[] data) => Array.ConvertAll(data, int.Parse);
        //doesn't check for invalid input yet
    }
}