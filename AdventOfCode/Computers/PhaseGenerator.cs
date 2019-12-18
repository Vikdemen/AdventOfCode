using System.Collections.Generic;

namespace AdventOfCode.Computers
{
    public static class PhaseGenerator
    { 
        //generates every possible variation of sequence, containing non-repeated digits 0-4
        //5-9 in feedback mode
        //hardcoded for sequences of 5, unfortunately
        public static List<int[]> GeneratePhaseSettings(bool feedback = false)
        {
            int mod = 0;
            if (feedback)
                mod = 5;
            var phaseSettingVariants = new List<int[]>();
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
                                int[] phaseSettings = {
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