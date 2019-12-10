using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Puzzles
{
    public class IntersectionDistanceChecker: Puzzle, IPuzzle
    {
        //--- Day 3: Crossed Wires ---
        //Opening the front panel reveals a jumble of wires. Specifically, two wires are connected to a central port and
        //extend outward on a grid. You trace the path each wire takes as it leaves the central port, one wire per line
        //of text (your puzzle input).
        //The wires twist and turn, but the two wires occasionally cross paths. To fix the circuit, you need to find the
        //intersection point closest to the central port. Because the wires are on a grid, use the Manhattan distance
        //for this measurement. While the wires do technically cross right at the central port where they both start,
        //this point does not count, nor does a wire count as crossing with itself.
        //What is the Manhattan distance from the central port to the closest intersection?
        protected override string InputFile => "day3.txt";
        public override string ResultText => $"Distance to closest intersection is {Result.ToString()}";

        protected Wire Wire0;
        protected Wire Wire1;
        
        public override void Solve()
        {
            Wire0 = new Wire(PuzzleInput[0]);
            Wire1 = new Wire(PuzzleInput[1]);
            Result = Calculate();
        }

        protected virtual int Calculate() =>
            Wire.GetClosest(Wire.Intersect(Wire0, Wire1));
    }

    public class DelayMeasurer: IntersectionDistanceChecker
    {
        //It turns out that this circuit is very timing-sensitive; you actually need to minimize the signal delay.
        //To do this, calculate the number of steps each wire takes to reach each intersection; choose the intersection
        //where the sum of both wires' steps is lowest. If a wire visits a position on the grid multiple times, use the
        //steps value from the first time it visits that position when calculating the total value of a specific
        //intersection.
        //The number of steps a wire takes is the total number of grid squares the wire has entered to get to that
        //location, including the intersection being considered.
        //What is the fewest combined steps the wires must take to reach an intersection?
        public override string ResultText => $"Smallest signal delay is {Result.ToString()}";
        protected override int Calculate()
        {
            List<Point> intersections = Wire.Intersect(Wire0, Wire1);
            int result = intersections.Select(GetDelay).Min();

            int GetDelay(Point intersection) =>
                Wire.SignalDelay(Wire0, Wire1, intersection);

            return result;
        }
    }
}