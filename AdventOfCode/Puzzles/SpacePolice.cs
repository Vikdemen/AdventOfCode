using AdventOfCode.IntCode;
using AdventOfCode.Painting;

namespace AdventOfCode.Puzzles
{
    public class SpacePolice : Puzzle, IPuzzle
    {
        //--- Day 11: Space Police ---
        //You'll need to build a new emergency hull painting robot. The robot needs to be able to move around on the
        //grid of square panels on the side of your ship, detect the color of its current panel, and paint its current
        //panel black or white. (All of the panels are currently black.)
        //The Intcode program will serve as the brain of the robot. The program uses input instructions to access the
        //robot's camera: provide 0 if the robot is over a black panel or 1 if the robot is over a white panel. Then,
        //the program will output two values:
        //First, it will output a value indicating the color to paint the panel the robot is over: 0 means to paint the
        //panel black, and 1 means to paint the panel white.
        //Second, it will output a value indicating the direction the robot should turn: 0 means it should turn left 90
        //degrees, and 1 means it should turn right 90 degrees.
        //After the robot turns, it should always move forward exactly one panel. The robot starts facing up.
        //The robot will continue running for a while like this and halt when it is finished drawing. Do not restart the
        //Intcode computer inside the robot during this process.
        //Build a new emergency hull painting robot and run the Intcode program on it.
        //How many panels does it paint at least once?
        
        protected override string InputFile => "day11.txt";

        protected virtual bool StartOnWhite => false;

        public override string Solve()
        {
            string instructions = PuzzleInput[0];
            var robot = new PaintingRobot(InstructionParser.Parse(instructions));
            robot.Start(StartOnWhite);
            int paintedPanels = robot.PaintedCount;
            return $"The robot painted {paintedPanels.ToString()} panels at least once";
        }
    }

    public class RegistrationImage: SpacePolice
    {
        //--- Part Two ---
        //a valid registration identifier is always eight capital letters. After starting the robot on a single white
        //panel instead, what registration identifier does it paint on your hull?

        protected override bool StartOnWhite => true;

        public override string Solve()
        {
            string instructions = PuzzleInput[0];
            var robot = new PaintingRobot(InstructionParser.Parse(instructions));
            robot.Start(StartOnWhite);
            return Imager.ImageToString(robot.TileGrid);
        }
    }
}