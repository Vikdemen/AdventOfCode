using System;
using AdventOfCode.DataLoading;

namespace AdventOfCode.Puzzles
{
    public abstract class Puzzle
    {
        protected abstract string InputFile { get; }
        //hardcoded in child classes for now

        public string[] PuzzleInput { get; set; }
        
        public abstract string ResultText { get; }
        public long Result { get; protected set; }

        public IDataLoader DataLoader { get; set; }
        
        //if no data loader defined, returns empty array
        public void LoadData(IDataLoader dataLoader)
        {
            PuzzleInput = dataLoader?.GetData(InputFile) ?? Array.Empty<string>();
        }

        public void Process(string[] puzzleInput)
        {
            PuzzleInput = puzzleInput;
        }
        public abstract void Solve();
    }
}