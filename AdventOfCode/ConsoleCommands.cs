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
        
        //a list of said commands
        public enum CommandID
        {
            Help,
            Exit,
            ChangeFolder,
            UnknownCommand,
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
            OrbitChecksum,
            TransferToSanta,
            CheckCorruption,
            ShowImage,
            FeedbackLoop,
            AmplificationCircuit,
            SensorBoostTest,
            SensorBoost,
            CountPainted,
            RegistrationImage,
            MoonsEnergy
        }
        
        //returns either command from a dictionary or "unknown" command
        public static CommandID GetCommand(string commandName) =>
            CommandsByName.TryGetValue(commandName, out CommandID command) ? command : CommandID.UnknownCommand;

        private static readonly IDictionary<string, CommandID> CommandsByName = new Dictionary<string, CommandID>
        {
            ["moons-energy"] = CommandID.MoonsEnergy,
            ["registration-image"] = CommandID.RegistrationImage,
            ["count-painted"] = CommandID.CountPainted,
            ["sensor-boost"] = CommandID.SensorBoost,
            ["sensor-test"] = CommandID.SensorBoostTest,
            ["feedback-loop"] = CommandID.FeedbackLoop,
            ["amplification-circuit"] = CommandID.AmplificationCircuit,
            ["show-image"] = CommandID.ShowImage,
            ["check-corruption"] = CommandID.CheckCorruption,
            ["transfer-to-santa"] = CommandID.TransferToSanta,
            ["calculate-fuel"] = CommandID.CalculateFuel,
            ["fuel-for-fuel"] = CommandID.FuelForFuel,
            ["program-alarm"] = CommandID.ProgramAlarm,
            ["gravity-assist"] = CommandID.GravityAssist,
            ["crossed-wires"] = CommandID.CrossedWires,
            ["signal-delay"] = CommandID.SignalDelay,
            ["check-passwords"] = CommandID.CheckPasswords,
            ["check-passwords-new"] = CommandID.CheckPasswordsNew,
            ["test-system"] = CommandID.TestSystem,
            ["test-radiator"] = CommandID.TestRadiator,
            ["orbit-checksum"] = CommandID.OrbitChecksum,
            ["help"] = CommandID.Help,
            ["exit"] = CommandID.Exit
        };

        public static Func<string> GetAction(CommandID commandID) =>
            ActionForCommand[commandID];

        private static readonly IDictionary<CommandID, Func<string>> ActionForCommand
            = new Dictionary<CommandID, Func<string>>
            {
                [CommandID.Exit] = Exit,
                [CommandID.Help] = ListCommands,
                [CommandID.ChangeFolder] = ChangeInputFolder,
                [CommandID.UnknownCommand] = UnknownCommand,
                [CommandID.CalculateFuel] = FuelCalculator.CalculateFuel,
                [CommandID.FuelForFuel] = FuelForFuelCalculator.FuelForFuel,
                [CommandID.ProgramAlarm] = ProgramAlarm,
                [CommandID.GravityAssist] = GravityAssist,
                [CommandID.CrossedWires] = CrossedWires,
                [CommandID.SignalDelay] = SignalDelay,
                [CommandID.CheckPasswords] = CheckPassword,
                [CommandID.CheckPasswordsNew] = CheckPasswordNew,
                [CommandID.TestSystem] = TestSystem,
                [CommandID.TestRadiator] = TestRadiator,
                [CommandID.OrbitChecksum] = OrbitCheckSum,
                [CommandID.TransferToSanta] = TransferToSanta,
                [CommandID.CheckCorruption] = CheckCorruption,
                [CommandID.ShowImage] = ShowImage,
                [CommandID.FeedbackLoop] = FeedbackLoop,
                [CommandID.AmplificationCircuit] = AmplificationCircuit,
                [CommandID.SensorBoostTest] = SensorBoostTest,
                [CommandID.SensorBoost] = SensorBoost,
                [CommandID.CountPainted] = CountPainted,
                [CommandID.RegistrationImage] = RegistrationImage,
                [CommandID.MoonsEnergy] = NBodyProblem.CalculateMoonsEnergy
            };


        private static string ListCommands()
        {
            List<string> commandNames = new List<string>(CommandsByName.Keys);
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
            SolvePuzzle(new ProgramAlarm());

        private static string GravityAssist() => 
            SolvePuzzle(new GravityAssist());

        private static string CrossedWires() => 
            SolvePuzzle(new IntersectionDistanceChecker());

        private static string SignalDelay() => 
            SolvePuzzle(new DelayMeasurer());

        private static string CheckPassword() =>
            SolvePuzzle(new ValidPasswordCounter());

        private static string CheckPasswordNew() =>
            SolvePuzzle(new ValidPasswordCounter(new AdvancedPasswordValidator()));

        private static string TestSystem() => 
            SolvePuzzle(new Diagnostics {InputInstruction = 1});

        private static string TestRadiator() => 
            SolvePuzzle(new Diagnostics {InputInstruction = 5});

        private static string OrbitCheckSum() => 
            SolvePuzzle(new UniversalOrbitMap());

        private static string TransferToSanta() => 
            SolvePuzzle(new OrbitTransferPlanner());

        private static string CheckCorruption() =>
            SolvePuzzle(new SpaceImageValidator());

        private static string ShowImage() =>
            SolvePuzzle(new SpaceImageReader());

        private static string AmplificationCircuit() =>
            SolvePuzzle(new AmplificationCircuit());

        private static string FeedbackLoop() =>
            SolvePuzzle(new FeedbackLoop());

        private static string SensorBoostTest() =>
            SolvePuzzle(new SensorBoost(1));

        private static string SensorBoost() =>
            SolvePuzzle(new SensorBoost(2));

        private static string CountPainted() =>
            SolvePuzzle(new SpacePolice());

        private static string RegistrationImage() =>
            SolvePuzzle(new RegistrationImage());

        public static string SolvePuzzle(IPuzzle puzzle)
        {
            puzzle.LoadData(DataLoader);
            puzzle.Solve();
            return puzzle.ResultText;
        }
        //todo - relocate to Puzzle class
    }
}