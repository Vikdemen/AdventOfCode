using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mime;
using AdventOfCode.Puzzles;

namespace AdventOfCode
{
    public static class AdventSolver
    {
        private const string inputFolder = @"AdventData\";

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
                return command switch
                {
                    Command.CalculateFuel => SolvePuzzle(new FuelCalculator {fuelForFuel = false}),
                    Command.FuelForFuel => SolvePuzzle(new FuelCalculator {fuelForFuel = true}),
                    Command.ProgramAlarm => SolvePuzzle(new ProgramAlarm()),
                    Command.GravityAssist => SolvePuzzle(new GravityAssist()),
                    Command.CrossedWires => SolvePuzzle(new IntersectionDistanceChecker()),
                    Command.SignalDelay => SolvePuzzle(new DelayMeasurer()),
                    Command.CheckPasswords => SolvePuzzle(new PasswordValidator {additionalTest = false}),
                    Command.CheckPasswordsNew => SolvePuzzle(new PasswordValidator {additionalTest = true}),
                    Command.TestSystem => SolvePuzzle(new Diagnostics {InputInstruction = 1}),
                    Command.TestRadiator => SolvePuzzle(new Diagnostics {InputInstruction = 5}),
                    Command.Help => ListCommands(),
                    Command.Exit => Exit(),
                    _ => "Invalid command"
                };
            }
            return "Unknown command, please print help for command list";
        }

        private static string SolvePuzzle(IPuzzle puzzle)
        {
            puzzle.LoadData();
            puzzle.Process();
            return puzzle.ResultText;
        }

        private static string Exit()
        {
            Environment.Exit(0);
            return string.Empty;
        }

        public static string[] ReadFile(string fileName, string folder = inputFolder)
            => System.IO.File.ReadAllLines
                (string.Concat(inputFolder, fileName));
        
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

        private static string ListCommands()
        {
            List<string> commandNames = new List<string>(commandDictionary.Keys);
            foreach (string commandName in commandNames)
            {
                Console.WriteLine(commandName);
            }

            return string.Empty;
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