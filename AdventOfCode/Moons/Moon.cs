using System;
using System.Numerics;

namespace AdventOfCode.Moons
{
    public class Moon
    {
        public string Name { get; }
        public Vector3 Position { get; private set; }
        public Vector3 Velocity { get; private set; }

        public int TotalEnergy => CalculateEnergy(Position) * CalculateEnergy(Velocity);

        public Moon (string name, Vector3 position)
        {
            Name = name;
            Position = position;
            Velocity = Vector3.Zero;
        }

        public void Simulate(Vector3 gravity)
        {
            //update velocity
            Velocity += gravity;
            //then update position
            Position += Velocity;
        }

        //sum of absolute values
        private static int CalculateEnergy(Vector3 vector)
        {
            int sum = 0;
            sum += (int) Math.Abs(vector.X);
            sum += (int) Math.Abs(vector.Y);
            sum += (int) Math.Abs(vector.Z);
            return sum;
        }
    }
}