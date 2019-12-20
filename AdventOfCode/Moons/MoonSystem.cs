using System;
using System.Linq;
using System.Numerics;

namespace AdventOfCode.Moons
{
    public class MoonSystem
    {
        public int TotalEnergy => CalculateEnergy();

        public Moon[] Moons { get; }

        //creates the system from entries, describing moon positions
        public MoonSystem (string[] data)
        {
            Moons = new Moon[4];
            Moons[0] = new Moon("Io", ParsePosition(data[0]));
            Moons[1] = new Moon("Europa", ParsePosition(data[1]));
            Moons[2] = new Moon("Ganymede", ParsePosition(data[2]));
            Moons[3] = new Moon("Callisto", ParsePosition(data[3]));
        }

        //every entry is like <x=1, y=-4, z=3>
        private static Vector3 ParsePosition(string entry)
        {
            //first we delete the brackets
            entry = entry.Substring(1, entry.Length - 2);
            //split them
            string[] values = entry.Split(new[] {", "}, StringSplitOptions.RemoveEmptyEntries);
            //removes x= and parses the value
            int x = int.Parse(values[0].Remove(0,2));
            int y = int.Parse(values[1].Remove(0,2));
            int z = int.Parse(values[2].Remove(0,2));
            return new Vector3(x, y, z);
        }

        public void Simulate(int steps)
        {
            int numberOfMoons = Moons.Length;
            //does a number of iterations
            for (int n = 0; n < steps; n++)
            {
                //i split calculating gravity and simulation, because the position should only change between steps
                var gravityValues = new Vector3[numberOfMoons];
                //for every moon i calculate gravity
                for (int i = 0; i < numberOfMoons; i++)
                {
                    int x = 0;
                    int y = 0;
                    int z = 0;
                    //considering position of every other moon
                    for(int j = 0; j < numberOfMoons; j++)
                        if (i != j)
                        {
                            x += Compare(Moons[j].Position.X, Moons[i].Position.X);
                            y += Compare(Moons[j].Position.Y, Moons[i].Position.Y);
                            z += Compare(Moons[j].Position.Z, Moons[i].Position.Z);
                        }

                    gravityValues[i] = new Vector3(x, y, z);
                }
                
                for (int i = 0; i < numberOfMoons; i++) 
                    Moons[i].Simulate(gravityValues[i]);
            }

            //IComparable has such method, but it's not static
            static int Compare(float v1, float v2)
            {
                if (v1 > v2)
                    return 1;
                if (v1 < v2)
                    return -1;
                return 0;
            }
        }
        
        private int CalculateEnergy() 
            => Moons.Select(moon => moon.TotalEnergy).Sum();

    }
}