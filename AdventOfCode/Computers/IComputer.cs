namespace AdventOfCode.Computers
{
    public interface IComputer
    {
        long[] Run(params int[] inputs);

        bool Finished { get; }
    }
}