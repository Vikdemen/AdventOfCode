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
                    return SecureContainer(ReadFile("day4.txt"), true);
                case "diagnostic":
                    return Diagnostic(ReadFile("day5.txt"), 1);
                case "radiator":
                    return Diagnostic(ReadFile("day5.txt"), 5);
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
            int result = Rocket.FuelForModules(Array.ConvertAll(data, int.Parse));
            return $"We need {result.ToString()} fuel in total";
        }

        //day 1 pt 2
        public static string CalculateFuelRecursive(string[] data)
        {
            int result = Rocket.FuelForModulesRecursive(Array.ConvertAll(data, int.Parse));
            return $"We need {result.ToString()} fuel in total";
        }
        
        //day 2
        public static string ProgramAlarm(string[] data)
        {
            string commands = data[0];
            IntCode computer = new IntCode();
            computer.Run(commands);
            int result = computer.MemoryRegister[0];
            return $"Program finished, {result.ToString()}" ;
        }

        public static string GravityAssist(string[] data)
        {
            string commands = data[0];
            int target = 19690720;
            IntCode computer = new IntCode();
            int result = computer.FindNounVerb(commands, target);
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

        public static string SecureContainer(string[] data, bool additionalTest = false)
        {
            data = data[0].Split('-');
            int start = int.Parse(data[0]);
            int finish = int.Parse(data[1]);
            int result;
            if (additionalTest)
                result = Password.CountMoreValid(start, finish);
            else
                result = Password.CountValid(start, finish);
            return $"{result} passwords are valid";
            
        }

        public static string Diagnostic(string[] data, int input)
        {
            var computer = new IntCode();
            computer.Run(data[0], input);
            int code = computer.Output;
            return $"Diagnostic code is {code.ToString()}";
        }

        public static string[] ReadFile(string fileName, string folder = inputFolder)
            => System.IO.File.ReadAllLines(string.Concat(inputFolder, fileName));
    }
}