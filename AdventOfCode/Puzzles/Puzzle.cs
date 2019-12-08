namespace AdventOfCode.Puzzles
{
    public abstract class Puzzle
    {
        protected abstract string InputFile { get; }
        //hardcoded in child classes for now

        public string[] PuzzleInput { get; set; }
        
        public abstract string ResultText { get; }
        public int Result { get; protected set; }

        public void LoadData()
        {
            PuzzleInput = AdventSolver.ReadFile(InputFile);
        }

        public void Process(string[] puzzleInput)
        {
            PuzzleInput = puzzleInput;
        }
        public abstract void Process();
    }

    public interface IPuzzle
    {
        string ResultText { get; }
        int Result { get; }
        void LoadData();
        void Process();
    }
}