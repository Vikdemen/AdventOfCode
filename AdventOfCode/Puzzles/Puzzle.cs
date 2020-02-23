using System;
using AdventOfCode.DataLoading;

namespace AdventOfCode.Puzzles
{
    public abstract class Puzzle
    {
        protected abstract string InputFile { get; }

        protected string[] PuzzleInput { get; set; }
        
        //if no data loader defined, returns empty array
        public void LoadData(IDataLoader dataLoader)
        {
            PuzzleInput = dataLoader?.GetData(InputFile) ?? Array.Empty<string>();
        }
        
        public void LoadData(string[] data)
        {
            PuzzleInput = data;
        }

        public abstract string Solve();

        public static string SolvePuzzle(IPuzzle puzzle)
        {
            puzzle.LoadData(AdventSolver.DataLoader);
            return puzzle.Solve();
        }
    }
}