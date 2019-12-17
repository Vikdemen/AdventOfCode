using AdventOfCode.IntCode;

namespace AdventOfCode.Computers
{
    public class FeedbackChain : AmplifierChain
    {
        //a class responsible for making a loop of amplfiers and running them repeatedly, passing output to input
        
        public FeedbackChain(long[] instructions, int numberOfAmplifiers) 
            : base(instructions, numberOfAmplifiers) {}

        //memory shouldn't reset between loops
        public override long[] Run(int[] phaseSettings)
        {
            long signal = InitialSignal;
            NewMemory(phaseSettings);
            int halted;
            do {
                halted = 0;
                for (int i = 0; i < NumberOfAmplifiers; i++)
                {
                    Memory amplifier = Amplifiers[i];
                    if (!amplifier.Halted)
                        signal = Amplify(amplifier, signal);
                    else
                        halted++;
                }
            } while (halted != NumberOfAmplifiers);
            return new [] {signal};
        }
        
        private void NewMemory(int[] phaseSettings)
        {
            for (int i = 0; i < NumberOfAmplifiers; i++)
            {
                var amplifier = new Memory(Program);
                amplifier.InputQueue.Enqueue(phaseSettings[i]);
                Amplifiers[i] = amplifier;
            }
            //TODO - throw exception if phase settings are not the same length as chain of amplifiers
        }
    }
}