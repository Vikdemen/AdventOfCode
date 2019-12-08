namespace AdventOfCode.Puzzles
{
    public class Diagnostics : Puzzle, IPuzzle
    {
        //--- Day 5: Sunny with a Chance of Asteroids ---
        //The Thermal Environment Supervision Terminal (TEST) starts by running a diagnostic program
        //(your puzzle input).
        //After providing 1 to the only input instruction and passing all the tests,
        //what diagnostic code does the program produce?
        
        //--- Part Two ---
        //This time, when the TEST diagnostic program runs its input instruction to get the ID of the system to test,
        //provide it 5, the ID for the ship's thermal radiator controller. This diagnostic test suite only outputs one
        //number, the diagnostic code.
        //What is the diagnostic code for system ID 5?
        
        protected override string InputFile => "day5.txt";
        public override string ResultText => $"Diagnostic code is {Result.ToString()}";

        public int InputInstruction { get; set; }

        public override void Process()
        {
            string instructions = PuzzleInput[0];
            var computer = new IntCode();
            computer.Run(instructions, InputInstruction);
            int code = computer.Output;
        }
    }
}