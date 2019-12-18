using AdventOfCode.Orbits;

namespace AdventOfCode.Puzzles
{
    public class UniversalOrbitMap: Puzzle, IPuzzle
    {
        //--- Day 6: Universal Orbit Map ---
        //To verify maps, the Universal Orbit Map facility uses orbit count checksums - the total number of direct
        //orbits (like the one shown above) and indirect orbits.
        //What is the total number of direct and indirect orbits in your map data?
        protected override string InputFile => "day6.txt";
        public override string ResultText => $"There are {Result.ToString()} orbits total!";
        protected OrbitMapper Mapper;
        
        public override void Solve()
        {
            PrepareMap();
            GetResult();
        }

        public void PrepareMap()
        {
            Mapper = new OrbitMapper();
            Mapper.GenerateMap(PuzzleInput);
        }

        protected virtual void GetResult ()
        {
            Result = Mapper.GetCheckSum();
        }
    }
    
    public class OrbitTransferPlanner : UniversalOrbitMap
    {
        //--- Part Two ---
        //Now, you just need to figure out how many orbital transfers you (YOU) need to take to get to Santa (SAN).
        //What is the minimum number of orbital transfers required to move from the object YOU are orbiting to the
        //object SAN is orbiting?
        public override string ResultText => $"You need {Result.ToString()} orbital transfers!";

        protected override void GetResult ()
        {
            Result = Mapper.CountTransfers("YOU", "SAN");
            //Between the objects they are orbiting - not between YOU and SAN.)
            Result -= 2;
        }
    }
}