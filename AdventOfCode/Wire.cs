using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace AdventOfCode
{
    public class Wire
    {
        public List<Point> Path { get; } = new List<Point>();
        //we consider coordinates of central port to be 0, 0

        //when we receive instructions as a string, we build a path, containing all points except start
        public Wire(string input)
        {
            WireInstruction[] instructions = ParseInstructions(input);
            MakePath(instructions);
        }

        //parses a string to an array of instructions
        //each instruction has direction and number of steps
        private WireInstruction[] ParseInstructions(string input)
        {
            string[] instructionStrings = input.Split(new char[] {','});
            var instructions = new WireInstruction[instructionStrings.Length];
            for (int i = 0; i < instructionStrings.Length; i++)
            {
                instructions[i] = new WireInstruction(instructionStrings[i]);
            }
            return instructions;
        }

        //gives us a set of nodes passed by wires
        private void MakePath(WireInstruction[] instructions)
        {
            Point point = new Point(0, 0);
            foreach (var instruction in instructions)
            { 
                Point[] pathPart = Execute(instruction, point);
                point = pathPart.Last();
                Path.AddRange(pathPart);
            }
        }

        //executes a command
        private Point[] Execute(WireInstruction wireInstruction, Point start)
        {
            Point[] path = new Point[wireInstruction.Distance];
            Point node = start;
            for (int i = 0; i < wireInstruction.Distance; i++)
            {
                node = Step(wireInstruction.Direction, node);
                path[i] = node;
            }

            //returns a neighbouring point
            Point Step(char dir, Point origin)
            {
                switch (dir)
                {
                    case 'R':
                        return new Point(origin.X + 1, origin.Y);
                        break;
                    case 'L':
                        return new Point(origin.X - 1, origin.Y);
                        break;
                    case 'U':
                        return new Point(origin.X, origin.Y + 1);
                        break;
                    case 'D':
                        return new Point(origin.X, origin.Y - 1);
                        break;
                    default:
                        Console.WriteLine("Invalid input");
                        return new Point(0, 0);
                }
            }
            return path;
        }

        //returns all points where 2 wires intersect
        public static List<Point> Intersect(Wire wire0, Wire wire1) =>
            wire0.Path.Intersect(wire1.Path).ToList();

        public static int SignalDelay(Wire wire0, Wire wire1, Point intersection)
            => wire0.Path.IndexOf(intersection) + 1 + wire1.Path.IndexOf(intersection) + 1;

        //returns the manhattan distance of closest to origin intersection
        public static int GetClosest(List<Point> intersections)
        {
            if (intersections.Count == 0)
                return -1;
            //if the wires do not intersect
            int closest = intersections[0].ManhattanDistance;
            foreach (Point intersection in intersections)
            {
                int distance = intersection.ManhattanDistance;
                if (distance < closest)
                    closest = distance;
            }

            return closest;
        }
    }

    //represents the cartesian coordinates of wire node
    public struct Point
    {
        public readonly int X;
        public readonly int Y;
        public int ManhattanDistance => Math.Abs(X) + Math.Abs(Y);

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    //represents the instruction for the wire layer
    public struct WireInstruction
    {
        public readonly char Direction;
        public readonly int Distance;

        //parses the instruction in string form
        public WireInstruction(string instructionString)
        {
            Direction = instructionString[0];
            Distance = int.Parse(instructionString.Substring(1));
        }
    }
}