using System.Linq;
using AdventOfCode.IntCode;

namespace AdventOfCode.Computers
{
    //class for making a chain of IntCode computers and running them in a chain

    public class AmplifierChain: IComputer
    {
        protected readonly Memory[] Amplifiers;
        protected readonly int NumberOfAmplifiers;
        protected readonly long[] Program;
        protected const int InitialSignal = 0;

        public bool Finished => Amplifiers.All(amp => amp.Halted);

        //creates a list of N memory blocks with same instructions
        public AmplifierChain(long[] instructions, int numberOfAmplifiers)
        {
            NumberOfAmplifiers = numberOfAmplifiers;
            Program = instructions;
            Amplifiers = new Memory[NumberOfAmplifiers];
        }

        //runs the program several times, using input from previous stage for the next one
        public virtual long[] Run(int[] phaseSettings)
        {
            long signal = InitialSignal;
            for (int i = 0; i < NumberOfAmplifiers; i++)
            {
                var amplifier = new Memory(Program);
                amplifier.InputQueue.Enqueue(phaseSettings[i]);
                signal = Amplify(amplifier, signal);
            }
            return new [] {signal};
        }

        protected static long Amplify(Memory amplifier, long input)
        {
            amplifier.InputQueue.Enqueue(input);
            amplifier.Start();
            return amplifier.OutputQueue.Dequeue();
        }
    }
}