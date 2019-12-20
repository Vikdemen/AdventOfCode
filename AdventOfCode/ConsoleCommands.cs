using System;
using System.Collections.Generic;
using AdventOfCode.Passwords;
using AdventOfCode.Puzzles;
using static AdventOfCode.AdventSolver;

namespace AdventOfCode
{
    public static class ConsoleCommands
    {
        //class that holds references for various console commands

        //returns either command from a dictionary or "unknown" command
        public static Func<string> GetCommand(string commandName) =>
            Commands.TryGetValue(commandName, out Func<string> command) ? command : UnknownCommand;

        private static readonly IDictionary<string, Func<string>> Commands
            = new Dictionary<string, Func<string>>
            {
                ["exit"] = Exit,
                ["help"] = ListCommands,
                ["change-input-folder"] = ChangeInputFolder,
                ["calculate-fuel"] = FuelCalculator.CalculateFuel,
                ["fuel-for-fuel"] = FuelForFuelCalculator.FuelForFuel,
                ["program-alarm"] = ProgramAlarm,
                ["gravity-assist"] = GravityAssist,
                ["crossed-wires"] = CrossedWires,
                ["signal-delay"] = SignalDelay,
                ["check-passwords"] = CheckPassword,
                ["check-passwords-new"] = CheckPasswordNew,
                ["test-system"] = TestSystem,
                ["test-radiator"] = TestRadiator,
                ["orbit-checksum"] = OrbitCheckSum,
                ["transfer-to-santa"] = TransferToSanta,
                ["check-corruption"] = CheckCorruption,
                ["show-image"] = ShowImage,
                ["feedback-loop"] = FeedbackLoop,
                ["amplification-circuit"] = AmplificationCircuit,
                ["sensor-test"] = SensorBoostTest,
                ["sensor-boost"] = SensorBoost,
                ["count-painted"] = CountPainted,
                ["registration-image"] = RegistrationImage
            };


        private static string ListCommands()
        {
            List<string> commandNames = new List<string>(Commands.Keys);
            string commandList = "";
            foreach (string commandName in commandNames)
            {
                commandList += commandName + "\r\n";
            }
            return commandList;
        }

        private static string Exit()
        {
            ContinueInput = false;
            return "Program finished, exiting";
        }

        private static string ChangeInputFolder()
        {
            //i should do it with parameters, actually
            Console.WriteLine("Enter the new input folder path, please");
            string path = Console.ReadLine();
            if (DataLoader != null)
                DataLoader.InputFolder = path + @"\";
            else
                return "No file loader instance found";
            return "Input folder changed";
        }

        private static string UnknownCommand() =>
            "Unknown command, please print help for command list";

        private static string ProgramAlarm() => 
            Puzzle.SolvePuzzle(new ProgramAlarm());

        private static string GravityAssist() => 
            Puzzle.SolvePuzzle(new GravityAssist());

        private static string CrossedWires() => 
            Puzzle.SolvePuzzle(new IntersectionDistanceChecker());

        private static string SignalDelay() => 
            Puzzle.SolvePuzzle(new DelayMeasurer());

        private static string CheckPassword() =>
            Puzzle.SolvePuzzle(new ValidPasswordCounter());

        private static string CheckPasswordNew() =>
            Puzzle.SolvePuzzle(new ValidPasswordCounter(new AdvancedPasswordValidator()));

        private static string TestSystem() => 
            Puzzle.SolvePuzzle(new Diagnostics {InputInstruction = 1});

        private static string TestRadiator() => 
            Puzzle.SolvePuzzle(new Diagnostics {InputInstruction = 5});

        private static string OrbitCheckSum() => 
            Puzzle.SolvePuzzle(new UniversalOrbitMap());

        private static string TransferToSanta() => 
            Puzzle.SolvePuzzle(new OrbitTransferPlanner());

        private static string CheckCorruption() =>
            Puzzle.SolvePuzzle(new SpaceImageValidator());

        private static string ShowImage() =>
            Puzzle.SolvePuzzle(new SpaceImageReader());

        private static string AmplificationCircuit() =>
            Puzzle.SolvePuzzle(new AmplificationCircuit());

        private static string FeedbackLoop() =>
            Puzzle.SolvePuzzle(new FeedbackLoop());

        private static string SensorBoostTest() =>
            Puzzle.SolvePuzzle(new SensorBoost(1));

        private static string SensorBoost() =>
            Puzzle.SolvePuzzle(new SensorBoost(2));

        private static string CountPainted() =>
            Puzzle.SolvePuzzle(new SpacePolice());

        private static string RegistrationImage() =>
            Puzzle.SolvePuzzle(new RegistrationImage());
    }
}