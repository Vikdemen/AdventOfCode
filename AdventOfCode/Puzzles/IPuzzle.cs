using AdventOfCode.DataLoading;

namespace AdventOfCode.Puzzles
{
    public interface IPuzzle
    {
        string ResultText { get; }
        long Result { get; }
        void LoadData(IDataLoader dataLoader);
        void Solve();
    }
}