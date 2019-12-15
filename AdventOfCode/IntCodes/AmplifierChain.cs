using System.Collections.Generic;

namespace AdventOfCode.IntCodes
{
    public interface ICircuit
    {
        int Run(int[] phaseSettings);
    }
    
    public class AmplifierChain: ICircuit
    {
        protected readonly Memory[] Amplifiers;
        protected readonly int NumberOfAmplifiers;
        protected readonly long[] Program;
        protected const int InitialSignal = 0;

        //creates a list of N memory blocks with same instructions
        public AmplifierChain(long[] instructions, int numberOfAmplifiers)
        {
            NumberOfAmplifiers = numberOfAmplifiers;
            Program = instructions;
            Amplifiers = new Memory[NumberOfAmplifiers];
        }

        //runs the program several times, using input from previous stage for the next one
        public virtual int Run(int[] phaseSettings)
        {
            int signal = InitialSignal;
            for (int i = 0; i < NumberOfAmplifiers; i++)
            {
                var amplifier = new Memory(Program) {Input = phaseSettings[i]};
                signal = Amplify(amplifier, signal);
            }
            return signal;
        }

        protected static int Amplify(Memory amplifier, int input)
        {
            amplifier.Input = input;
            amplifier.Start();
            return (int)amplifier.Output;
        }
    }
    


    public class FeedbackChain : AmplifierChain
    {
        public FeedbackChain(long[] instructions, int numberOfAmplifiers) 
            : base(instructions, numberOfAmplifiers)
        {
        }

        //memory shouldn't reset between runs
        public override int Run(int[] phaseSettings)
        {
            int signal = InitialSignal;
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
            return signal;
        }
        
        private void NewMemory(int[] phaseSettings)
        {
            for (int i = 0; i < NumberOfAmplifiers; i++)
            {
                var amplifier = new Memory(Program) {Input = phaseSettings[i]};
                Amplifiers[i] = amplifier;
            }
            //TODO - throw exception if phase settings are not the same length as chain of amplifiers
        }
    }
}