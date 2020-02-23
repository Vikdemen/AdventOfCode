using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Computers;
using AdventOfCode.IntCode;

namespace AdventOfCode.Puzzles
{
    public class AmplificationCircuit : Puzzle, IPuzzle
    {
        //--- Day 7: Amplification Circuit ---
        //There are five amplifiers connected in series; each one receives an input signal and produces an output
        //signal. They are connected such that the first amplifier's output leads to the second amplifier's input, the
        //second amplifier's output leads to the third amplifier's input, and so on. The first amplifier's input value
        //is 0, and the last amplifier's output leads to your ship's thrusters.
        //When a copy of the program starts running on an amplifier, it will first use an input instruction to ask the
        //amplifier for its current phase setting (an integer from 0 to 4). Each phase setting is used exactly once.
        //The program will then call another input instruction to get the amplifier's input signal, compute the correct
        //output signal, and supply it back to the amplifier with an output instruction. (If the amplifier has not yet
        //received an input signal, it waits until one arrives.)
        //Your job is to find the largest output signal that can be sent to the thrusters by trying every possible
        //combination of phase settings on the amplifiers. Make sure that memory is not shared or reused between copies
        //of the program.
        //Try every combination of phase settings on the amplifiers.
        //What is the highest signal that can be sent to the thrusters?
        protected override string InputFile => "day7.txt";
        protected const int NumberOfAmplifiers = 5;
        protected bool FeedbackMode;

        public override string Solve()
        {
            long[] program = InstructionParser.Parse(PuzzleInput[0]);
            
            List<int[]> phaseSettingsVariants = PhaseGenerator.GeneratePhaseSettings(FeedbackMode);
            
            var amplifierChain = CreateAmplifier(program);
            
            int largestSignal = phaseSettingsVariants
                .Select(variant => (int)amplifierChain.Run(variant)[0]).Max();
            
            return $"Maximum signal is {largestSignal.ToString()}";
        }

        protected virtual IComputer CreateAmplifier(long[] program)
        {
            return new AmplifierChain(program, NumberOfAmplifiers);
        }
    }
}
        