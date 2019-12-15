namespace AdventOfCode.Puzzles
{
    public class MonitoringStation: Puzzle, IPuzzle
    {
        //The map indicates whether each position is empty (.) or contains an asteroid (#). The asteroids are much
        //smaller than they appear on the map, and every asteroid is exactly in the center of its marked position. The
        //asteroids can be described with X,Y coordinates where X is the distance from the left edge and Y is the
        //distance from the top edge (so the top-left corner is 0,0 and the position immediately to its right is 1,0).
        //Your job is to figure out which asteroid would be the best place to build a new monitoring station. A
        //monitoring station can detect any asteroid to which it has direct line of sight - that is, there cannot be
        //another asteroid exactly between them. This line of sight can be at any angle, not just lines aligned to the
        //grid or diagonally. The best location is the asteroid that can detect the largest number of other asteroids.
        protected override string InputFile => "day10.txt";
        public override string ResultText => Result.ToString();
        public override void Solve()
        {
            bool[,] map = MapParser.Parse(PuzzleInput);
            int result = 0;
            Result = result;
        }
    }

    public static class MapParser
    {
        public static bool[,] Parse(string[] data)
        {
            int width = data[0].Length;
            int height = data.Length;
            var map = new bool[width, height];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    char asteroid = data[y][x];
                    map[x, y] = asteroid == '#';
                }
            }
            return map;
        }
    }
}