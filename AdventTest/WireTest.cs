using System.Collections.Generic;
using System.Security.Cryptography;
using NUnit.Framework;
using AdventOfCode;
using AdventOfCode.Wires;

namespace AdventTest
{
    [TestFixture]
    public class WireTest
    {
        
        //checks if the wires return correct intersection distance
        [TestCase("U1,R1", "R1,U1", 2)]
        [TestCase("U1,R2", "R1,U2", 2)]
        [TestCase("R8,U5,L5,D3", "U7,R6,D4,L4", 6)]
        [TestCase("R75,D30,R83,U83,L12,D49,R71,U7,L72", "U62,R66,U55,R34,D71,R55,D58,R83", 159)]
        [TestCase("R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51", "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7", 135)]
        public void GetIntersection(string first, string second, int distance)
        {
            var wire0 = new Wire(first);
            var wire1 = new Wire(second);
            int result = Wire.GetClosest(Wire.Intersect(wire0, wire1));
            Assert.AreEqual(distance, result);
        }

        [TestCase("R8,U5,L5,D3", "U7,R6,D4,L4", 30)]
        public void DelayTest(string first, string second, int result)
        {
            var wire0 = new Wire(first);
            var wire1 = new Wire(second);
            var intersections = Wire.Intersect(wire0, wire1);
            int smallestDelay = 100500;
            foreach (var intersection in intersections)
            {
                int delay = Wire.SignalDelay(wire1, wire0, intersection);
                if (delay < smallestDelay)
                    smallestDelay = delay;
            }
            Assert.AreEqual(smallestDelay, result);
        }
    }
}