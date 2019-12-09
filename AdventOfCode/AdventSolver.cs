using System;
using AdventOfCode.Puzzles;
using static AdventOfCode.ConsoleCommands;

namespace AdventOfCode
{
    public static class AdventSolver
    {
        private static bool continueInput = true;
        public static void Main(string[] args)
        {
            while (continueInput)
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
            Command command = GetCommand(commandName);
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
                Command.UnknownCommand => "Unknown command, please print help for command list",
                _ => "Invalid command"
            };
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
        
    }
}