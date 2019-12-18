using System;
using System.Collections.Generic;
using System.Xml;
using AdventOfCode.Computers;

namespace AdventOfCode.Painting
{
    public class PaintingRobot
    {
        //a class representing a robot that paints tiles
        
        //it is controlled by IntCode computer
        public IComputer Brain { get; set; }

        //and moves over a grid of panels
        //since i have no idea how big i need a grid, i'll try 101x101
        //default value for ints is 0, so grid would be black
        public int[,] TileGrid { get; } = new int[101, 101];

        //direction "up" is considered 0 degrees
        //rotation is clockwise
        private int direction = 0;

        //position on a tile grid
        //the starting position is right in the middle of it to avoid negative indexes
        private (int x, int y) position = (50, 50);

        //a set of every tile painted by a robot, regardless of color
        private readonly HashSet<(int, int)> paintedTiles = new HashSet<(int, int)>();
        //and a way to count them
        public int PaintedCount => paintedTiles.Count;
        
        private const int White = 0;
        private const int Black = 1;
            

        public PaintingRobot(long[] instructions)
        {
            Brain = new Computer(instructions);
        }

        //all input is from camera
        //you can either start on uniformly black grid
        //or a sole white tile
        
        public void Start(bool startOnWhite = false)
        {
            //makes the starting tile white without marking as painted
            if (startOnWhite)
                TileGrid[position.x, position.y] = White;

            //runs until program halts, taking 1 input and giving 2 outputs per cycle.
            while (!Brain.Finished)
            {
                int color = CheckCamera();
                //the brain should take 1 input and produce 2 outputs
                long[] output = Brain.Run(color);
                //first is color to paint a tile underneath
                var newColor = (int) output[0];
                //and another is direction to move
                var turnDirection = (int) output[1];
                Paint(newColor);
                TurnAndMove(turnDirection);
            }
        }

        //returns 1 for white panels, 0 for black panels
        private int CheckCamera()
        {
            return TileGrid[position.x, position.y];
        }

        //1 is turn left, 0 is turn right
        //you always do a 90 degree turn and then move for 1 tile
        private void TurnAndMove(int input)
        {
            switch (input)
            {
                case 0:
                    direction += 90;
                    break;
                case 1:
                    direction -= 90;
                    break;
                default:
                    throw new ArgumentException($"{input} is not a valid direction for painting robot");
            }
            
            //degrees should be between 0 and 360
            if (direction < 0)
                direction = (360 + direction);
            if (direction >= 360)
                direction = direction - 360;
            
            //after turning always move exactly 1 unit forward
            Move();
        }
        
        private void Move()
        {
            position = direction switch
            {
                0 => (position.x, position.y + 1),
                90 => (position.x + 1, position.y),
                180 => (position.x, position.y - 1),
                270 => (position.x - 1, position.y),
                _ => throw new ArgumentException
                    ($"{direction} is not a valid heading for painting robot")
                //doesn't work on diagonals yet
            };
        }

        //paints the tile in a chosen color and marks it as painted
        private void Paint(int color)
        {
            TileGrid[position.x, position.y] = color;
            
            //since it's a hashset, we shouldn't worry about duplicates
            paintedTiles.Add(position);
        }
    }
}