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
        protected OrbitMapper Mapper;
        
        public override string Solve()
        {
            PrepareMap();
            return GetResult();
        }

        public void PrepareMap()
        {
            Mapper = new OrbitMapper();
            Mapper.GenerateMap(PuzzleInput);
        }

        protected virtual string GetResult ()
        {
            int totalOrbits = Mapper.GetCheckSum();
            return $"There are {totalOrbits.ToString()} orbits total!";
        }
    }
    
    public class OrbitTransferPlanner : UniversalOrbitMap
    {
        //--- Part Two ---
        //Now, you just need to figure out how many orbital transfers you (YOU) need to take to get to Santa (SAN).
        //What is the minimum number of orbital transfers required to move from the object YOU are orbiting to the
        //object SAN is orbiting?

        protected override string GetResult ()
        {
            int transfers = Mapper.CountTransfers("YOU", "SAN");
            //Between the objects they are orbiting - not between YOU and SAN.)
            transfers -= 2;
            return $"You need {transfers.ToString()} orbital transfers!";
        }
    }
}