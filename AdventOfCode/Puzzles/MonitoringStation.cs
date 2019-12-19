using AdventOfCode.Asteroids;

namespace AdventOfCode.Puzzles
{
    public class MonitoringStation: Puzzle, IPuzzle
    {
        //--- Day 10: Monitoring Station ---
        
        //The map indicates whether each position is empty (.) or contains an asteroid (#). The asteroids are much
        //smaller than they appear on the map, and every asteroid is exactly in the center of its marked position. The
        //asteroids can be described with X,Y coordinates where X is the distance from the left edge and Y is the
        //distance from the top edge (so the top-left corner is 0,0 and the position immediately to its right is 1,0).
        //Your job is to figure out which asteroid would be the best place to build a new monitoring station. A
        //monitoring station can detect any asteroid to which it has direct line of sight - that is, there cannot be
        //another asteroid exactly between them. This line of sight can be at any angle, not just lines aligned to the
        //grid or diagonally. The best location is the asteroid that can detect the largest number of other asteroids.

        private (int x, int y) bestLocation;
        protected override string InputFile => "day10.txt";
        public override string ResultText => 
            $"Best position for monitoring station is {bestLocation.x.ToString()}, {bestLocation.y.ToString()}";
        public override void Solve()
        {
            var map = new AsteroidMap(PuzzleInput);
            bestLocation = map.BestLocation;
        }
        
    }
}