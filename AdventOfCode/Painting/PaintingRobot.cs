using System.Collections.Generic;
using AdventOfCode.Computers;

namespace AdventOfCode.Painting
{
    public class PaintingRobot
    {
        //a class representing a robot that paints tiles
        
        //it is controlled by IntCode computer
        public IComputer Brain { get; set; }

        //and moves over a grid of panels
        //since i have no idea how big i need a grid, i'll just make a hashset of white panels
        //private readonly HashSet<(int, int)> whiteTiles = new HashSet<(int, int)>();
        public int[,] TileGrid { get; } = new int[101, 101];

        //direction "up" is considered 0 degrees
        //rotation is clockwise
        private int direction = 0;

        //position on a tile grid
        private (int x, int y) position = (50, 50);

        //a set of every tile painted by a robot, regardless of color
        private readonly HashSet<(int, int)> paintedTiles = new HashSet<(int, int)>();
        //and a way to count them
        public int PaintedCount => paintedTiles.Count;

        public PaintingRobot(long[] instructions)
        {
            Brain = new Computer(instructions);
        }

        //all input is from camera
        public void Start(bool startOnWhite = false)
        {
            if (startOnWhite)
                //whiteTiles.Add((0, 0));
                TileGrid[position.x, position.y] = 1;
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
            //whiteTiles.Contains(position) ? 1 : 0;
        }

        private void TurnAndMove(int input)
        {
            if (input == 1)
                direction += 90;
            else
                direction -= 90;

            if (direction < 0)
                direction = (360 + direction);
            if (direction >= 360)
                direction = direction - 360;
            
            Move();
        }

        //after turning always move exactly 1 unit forward
        private void Move()
        {
            position = direction switch
            {
                0 => (position.x, position.y + 1),
                90 => (position.x + 1, position.y),
                180 => (position.x, position.y - 1),
                270 => (position.x - 1, position.y),
                _ => position
                //doesn't work on diagonals yet
            };
        }

        //paints the tile in a chosen color and marks it as painted
        private void Paint(int color)
        {
            /*
            if (color == 1)
                whiteTiles.Add(position);
            else
                whiteTiles.Remove(position);
            */
            TileGrid[position.x, position.y] = color;
            
            //since it's a hashset, we shouldn't worry about duplicates
            paintedTiles.Add(position);
        }
    }
}