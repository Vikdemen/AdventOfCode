using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    public static class ConsoleCommands
    {
        //class that holds references for various console commands
        public enum Command
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
            Exit,
            UnknownCommand
        }

        private static readonly IDictionary<string, Command> CommandDictionary = new Dictionary<string, Command>
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
        
        public static Command GetCommand(string commandName)
        {
            if (CommandDictionary.TryGetValue(commandName, out Command command))
                return command;
            else
                return Command.UnknownCommand;
        }

        public static string ListCommands()
        {
            List<string> commandNames = new List<string>(CommandDictionary.Keys);
            foreach (string commandName in commandNames)
            {
                Console.WriteLine(commandName);
            }

            return string.Empty;
        }
    }
}