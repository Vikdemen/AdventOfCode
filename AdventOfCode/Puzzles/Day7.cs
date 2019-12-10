using System.Collections.Generic;
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
        protected override string InputFile => "day7.txt";
        public override string ResultText => Result.ToString();
        private int numberOfAmplifiers = 5;
        private int initialInput = 0;

        public override void Process()
        {
            string instuctions = PuzzleInput[0];
            Computer amplifier = new Computer();
            var phaseSettingsVariants = GeneratePhaseSettings();
            int largestSignal = 0;
            foreach (var variant in phaseSettingsVariants)
            {
                amplifier.ChainRun(instuctions, numberOfAmplifiers, initialInput, variant);
                if (amplifier.Output > largestSignal)
                    largestSignal = amplifier.Output;
            }
            Result = largestSignal;
        }

        //generates all possible phase setting variants
        private List<int[]> GeneratePhaseSettings(bool feedback = false)
        {
            int mod = 0;
            if (feedback)
                mod = 5;
            List<int[]> phaseSettingVariants = new List<int[]>();
            for (int a = 0; a < 5; a++)
            {
                for (int b = 0; b < 5; b++)
                {
                    if (b == a)
                        continue;
                    for (int c = 0; c < 5; c++)
                    {
                        if (c==a|c==b)
                            continue;
                        for (int d = 0; d < 5; d++)
                        {
                            if (d==a|d==b|d==c)
                                continue;
                            for (int e = 0; e < 5; e++)
                            {
                                if (e==a|e==b|e==c|e==d)
                                    continue;
                                int[] phaseSettings = new int [5]
                                {
                                    a + mod, b + mod, c + mod, d + mod, e + mod
                                };
                                phaseSettingVariants.Add(phaseSettings);
                            }
                        }
                        
                    }
                }
            }
            return phaseSettingVariants;
        }
    }

}
        