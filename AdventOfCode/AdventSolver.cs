using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mime;
using AdventOfCode.Puzzles;

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
            if (commandDictionary.TryGetValue(commandName, out Command command))
            {
                switch (command)
                {
                    case Command.CalculateFuel:
                        return SolvePuzzle(new FuelCalculator {fuelForFuel = false});
                    case Command.FuelForFuel:
                        return SolvePuzzle(new FuelCalculator {fuelForFuel = true});
                    case Command.ProgramAlarm:
                        return SolvePuzzle(new ProgramAlarm());
                    case Command.GravityAssist:
                        return SolvePuzzle(new GravityAssist());
                    case Command.CrossedWires:
                        return SolvePuzzle(new IntersectionDistanceChecker());
                    case Command.SignalDelay:
                        return SolvePuzzle(new DelayMeasurer());
                    case Command.CheckPasswords:
                        return SolvePuzzle(new PasswordValidator {additionalTest = false});
                    case Command.CheckPasswordsNew:
                        return SolvePuzzle(new PasswordValidator {additionalTest = true});
                    case Command.TestSystem:
                        return SolvePuzzle(new Diagnostics {InputInstruction = 1});
                    case Command.TestRadiator:
                        return SolvePuzzle(new Diagnostics {InputInstruction = 5});
                    case Command.Help:
                        //TODO - list all commands
                        return "I'm helping!";
                    case Command.Exit:
                        Environment.Exit(0);
                        return "Exiting program";
                    //yep, it will never show that message
                    default:
                        return "Invalid command";
                }
                    
            }
            return "Unknown command, please print help for command list";
        }

        private static string SolvePuzzle(IPuzzle puzzle)
        {
            puzzle.LoadData();
            puzzle.Process();
            return puzzle.ResultText;
        }

        public static string[] ReadFile(string fileName, string folder = inputFolder)
            => System.IO.File.ReadAllLines(string.Concat(inputFolder, fileName));
        
        private enum Command
        {
            CalculateFuel,
            FuelForFuel,
            ProgramAlarm,
            GravityAssist,
            CrossedWires,
            SignalDelay,
            CheckPasswords,
            CheckPasswordsNew,
            TestSystem,
            TestRadiator,
            Help,
            Exit
        }

        private static readonly IDictionary<string, Command> commandDictionary = new Dictionary<string, Command>
        {
            ["calculate-fuel"] = Command.CalculateFuel,
            ["fuel-for-fuel"] = Command.FuelForFuel,
            ["program-alarm"] = Command.ProgramAlarm,
            ["gravity-assist"] = Command.GravityAssist,
            ["crossed-wires"] = Command.CrossedWires,
            ["signal-delay"] = Command.SignalDelay,
            ["check-passwords"] = Command.CheckPasswords,
            ["check-passwords-new"] = Command.CheckPasswordsNew,
            ["test-system"] = Command.TestSystem,
            ["test-radiator"] = Command.TestRadiator,
            ["help"] = Command.Help,
            ["exit"] = Command.Exit
        };
    }
}