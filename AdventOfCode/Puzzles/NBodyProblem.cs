using System.Numerics;
using AdventOfCode.Moons;

namespace AdventOfCode.Puzzles
{
    public class NBodyProblem : Puzzle, IPuzzle
    {
        //You decide to start by tracking the four largest moons: Io, Europa, Ganymede, and Callisto.
        //After a brief scan, you calculate the position of each moon (your puzzle input). You just need to simulate
        //their motion so you can avoid them.
        //Each moon has a 3-dimensional position (x, y, and z) and a 3-dimensional velocity. The position of each moon
        //is given in your scan; the x, y, and z velocity of each moon starts at 0.
        //Simulate the motion of the moons in time steps. Within each time step, first update the velocity of every moon
        //by applying gravity. Then, once all moons' velocities have been updated, update the position of every moon by
        //applying velocity. Time progresses by one step once all of the positions are updated.
        //To apply gravity, consider every pair of moons. On each axis (x, y, and z), the velocity of each moon changes
        //by exactly +1 or -1 to pull the moons together. For example, if Ganymede has an x position of 3, and Callisto
        //has a x position of 5, then Ganymede's x velocity changes by +1 (because 5 > 3) and Callisto's x velocity
        //changes by -1 (because 3 < 5). However, if the positions on a given axis are the same, the velocity on that
        //axis does not change for that pair of moons.
        //Once all gravity has been applied, apply velocity: simply add the velocity of each moon to its own position.
        //For example, if Europa has a position of x=1, y=2, z=3 and a velocity of x=-2, y=0,z=3, then its new position
        //would be x=-1, y=2, z=6. This process does not modify the velocity of any moon.
        //Then, it might help to calculate the total energy in the system. The total energy for a single moon is its
        //potential energy multiplied by its kinetic energy. A moon's potential energy is the sum of the absolute values
        //of its x, y, and z position coordinates. A moon's kinetic energy is the sum of the absolute values of its
        //velocity coordinates.
        //What is the total energy in the system after simulating the moons given in your scan for 1000 steps?
        
        //--- Part Two ---
        //Determine the number of steps that must occur before all of the moons' positions and velocities exactly match
        //a previous point in time.
        //you might need to find a more efficient way to simulate the universe.
        
        protected override string InputFile => "day12.txt";

        public override string ResultText => $"Total energy is {totalEnergy}";

        private const int SimLength = 1000;

        private int totalEnergy;
        
        public override void Solve()
        {
            var system = new MoonSystem(PuzzleInput);
            system.Simulate(SimLength);
            totalEnergy = system.TotalEnergy;
        }

        public static string CalculateMoonsEnergy()
        {
            IPuzzle nBody = new NBodyProblem();
            return SolvePuzzle(nBody);
        }
    }

    public class TimeLoop : NBodyProblem
    {
        public long NumberOfIterations { get; private set; }

        public override void Solve()
        {
            var initial = new MoonSystem(PuzzleInput);
            var system = new MoonSystem(PuzzleInput);
            long count = 1;
            do
            {
                system.Simulate(1);
                count++;
            } while (!CheckEquality(initial, system));

            NumberOfIterations = count;
            
            bool CheckEquality(MoonSystem one, MoonSystem two)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (one.Moons[i].Velocity != Vector3.Zero)
                        return false;
                    if (one.Moons[i].Position != two.Moons[i].Position)
                        return false;
                }
                return true;
            }
            
        }
    }
}