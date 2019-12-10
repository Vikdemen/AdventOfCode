using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Wire
    {
        private List<Point> Path { get; } = new List<Point>();
        //we consider coordinates of central port to be 0, 0
        //but do not include them in path

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
            string[] instructionStrings = input.Split(',');
            var instructions = new WireInstruction[instructionStrings.Length];
            for (int i = 0; i < instructionStrings.Length; i++)
            {
                instructions[i] = new WireInstruction(instructionStrings[i]);
            }
            return instructions;
        }

        //gives us a set of nodes passed by wires
        private void MakePath(IEnumerable<WireInstruction> instructions)
        {
            var end = new Point(0, 0);
            foreach (WireInstruction instruction in instructions)
            { 
                AddWireSegment(instruction, end);
                end = Path.Last();
            }
        }

        //adds a segment for wire based on instruction
        private void AddWireSegment(WireInstruction wireInstruction, Point start)
        {
            var segment = new Point[wireInstruction.Distance];
            Point node = start;
            for (int i = 0; i < wireInstruction.Distance; i++)
            {
                node = Step(wireInstruction.WireDirection, node);
                segment[i] = node;
            }

            //returns a neighbouring point
            Point Step(Direction direction, Point origin)
            {
                return direction switch
                {
                    Direction.Right => new Point(origin.X + 1, origin.Y),
                    Direction.Left => new Point(origin.X - 1, origin.Y),
                    Direction.Up => new Point(origin.X, origin.Y + 1),
                    Direction.Down => new Point(origin.X, origin.Y - 1),
                    _ => new Point (origin.X, origin.Y)
                    //TODO - throw exception
                    //for invalid input
                };
            }
            Path.AddRange(segment);
        }

        //returns all points where 2 wires intersect
        public static List<Point> Intersect(Wire wire0, Wire wire1) =>
            wire0.Path.Intersect(wire1.Path).ToList();

        public static int SignalDelay(Wire wire0, Wire wire1, Point intersection)
            => wire0.Path.IndexOf(intersection) + 1 + wire1.Path.IndexOf(intersection) + 1;
        
        //returns manhattan distance of intersection closest to origin
        //if the wires do not intersect, returns -1
        public static int GetClosest(List<Point> intersections) =>
            intersections.Any() ? intersections.Min(point => point.ManhattanDistance) : -1;
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
        public readonly Direction WireDirection;
        public readonly int Distance;

        //parses the instruction in string form
        //doesn't filter for invalid input
        public WireInstruction(string instructionString)
        {
            char wireDirection = instructionString[0];
            WireDirection = wireDirection switch
            {
                'U' => Direction.Up,
                'D' => Direction.Down,
                'R' => Direction.Right,
                'L' => Direction.Left,
                _ => Direction.Invalid
            };

            Distance = int.Parse(instructionString.Substring(1));
        }
    }

    public enum Direction
    { Up, Down, Right, Left, Invalid };
}