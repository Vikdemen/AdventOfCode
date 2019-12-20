using AdventOfCode.DataLoading;

namespace AdventOfCode.Puzzles
{
    public interface IPuzzle
    {
        void LoadData(IDataLoader dataLoader);
        void LoadData(string[] data);
        string Solve();
    }
}