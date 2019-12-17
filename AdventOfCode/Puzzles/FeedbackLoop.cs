using AdventOfCode.Computers;

namespace AdventOfCode.Puzzles
{
    public class FeedbackLoop: AmplificationCircuit
    {
        //--- Part Two ---
        //Most of the amplifiers are connected as they were before; amplifier A's output is connected to amplifier B's
        //input, and so on. However, the output from amplifier E is now connected into amplifier A's input. This creates
        //the feedback loop: the signal will be sent through the amplifiers many times.
        //In feedback loop mode, the amplifiers need totally different phase settings: integers from 5 to 9, again each
        //used exactly once. These settings will cause the Amplifier Controller Software to repeatedly take input and
        //produce output many times before halting. Provide each amplifier its phase setting at its first input
        //instruction; all further input/output instructions are for signals.
        //Don't restart the Amplifier Controller Software on any amplifier during this process. Each one should continue
        //receiving and sending signals until it halts.
        //All signals sent or received in this process will be between pairs of amplifiers except the very first signal
        //and the very last signal. To start the process, a 0 signal is sent to amplifier A's input exactly once.
        //Eventually, the software on the amplifiers will halt after they have processed the final loop. When this
        //happens, the last output signal from amplifier E is sent to the thrusters. Your job is to find the largest
        //output signal that can be sent to the thrusters using the new phase settings and feedback loop arrangement.
        //Try every combination of the new phase settings on the amplifier feedback loop. What is the highest signal
        //that can be sent to the thrusters?
        public FeedbackLoop()
        {
            FeedbackMode = true;
        }
        protected override IComputer CreateAmplifier(long[] program)
        {
            return new FeedbackChain(program, NumberOfAmplifiers);
        }
    }
}