namespace AdventOfCode.Puzzles
{
    public class OrbitChecker: Puzzle, IPuzzle
    {
        //--- Day 6: Universal Orbit Map ---
        //To verify maps, the Universal Orbit Map facility uses orbit count checksums - the total number of direct
        //orbits (like the one shown above) and indirect orbits.
        
        //What is the total number of direct and indirect orbits in your map data?
        protected override string InputFile => "day6.txt";
        public override string ResultText => $"There are {Result.ToString()} orbits total!";
        public override void Process()
        {
            var mapper = new OrbitMapper();
            mapper.GenerateMap(PuzzleInput);
            Result = mapper.GetCheckSum();
        }
    }
}