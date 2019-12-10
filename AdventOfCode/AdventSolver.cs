using System;
using AdventOfCode.Passwords;
using AdventOfCode.Puzzles;
using static AdventOfCode.ConsoleCommands;

namespace AdventOfCode
{
    public static class AdventSolver
    {
        private static bool continueInput = true;
        public static void Main()
        {
            while (continueInput)
            {
                string command = Console.ReadLine();
                string result = ExecuteCommand(command);
                Console.WriteLine(result);
            }
            Console.Write("Program finished, exiting");
            Console.ReadKey();
            //infinite loop of input
            //break it by using "exit" command;
        }

        //chooses a command based on input, executes it and returns result.
        private static string ExecuteCommand(string commandName)
        {
            Command command = GetCommand(commandName);
            return command switch
            {
                Command.CalculateFuel => SolvePuzzle(new FuelCalculator ()),
                Command.FuelForFuel => SolvePuzzle(new FuelForFuelCalculator()),
                Command.ProgramAlarm => SolvePuzzle(new ProgramAlarm()),
                Command.GravityAssist => SolvePuzzle(new GravityAssist()),
                Command.CrossedWires => SolvePuzzle(new IntersectionDistanceChecker()),
                Command.SignalDelay => SolvePuzzle(new DelayMeasurer()),
                Command.CheckPasswords => SolvePuzzle(new ValidPasswordCounter()),
                Command.CheckPasswordsNew => SolvePuzzle(new ValidPasswordCounter(new AdvancedPasswordValidator())),
                Command.TestSystem => SolvePuzzle(new Diagnostics {InputInstruction = 1}),
                Command.TestRadiator => SolvePuzzle(new Diagnostics {InputInstruction = 5}),
                Command.OrbitChecksum => SolvePuzzle(new OrbitChecker()),
                Command.TransferToSanta => SolvePuzzle(new OrbitTransferPlanner()),
                Command.Help => ListCommands(),
                Command.Exit => Exit(),
                Command.UnknownCommand => "Unknown command, please print help for command list",
                _ => "Invalid command"
            };
        }

        private static string SolvePuzzle(IPuzzle puzzle)
        {
            puzzle.LoadData();
            puzzle.Solve();
            return puzzle.ResultText;
        }

        private static string Exit()
        {
            continueInput = false;
            return "Exiting program";
        }
        
    }
}