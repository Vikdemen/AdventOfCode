using System;
using System.Net.NetworkInformation;
using AdventOfCode.SpaceImages;

namespace AdventOfCode.Puzzles
{
    public class SpaceImageValidator : Puzzle, IPuzzle
    {
        //--- Day 8: Space Image Format ---

        //Images are sent as a series of digits that each represent the color of a single pixel. The digits fill each
        //row of the image left-to-right, then move downward to the next row, filling rows top-to-bottom until every
        //pixel of the image is filled.
        //Each image actually consists of a series of identically-sized layers that are filled in this way. So, the
        //first digit corresponds to the top-left pixel of the first layer, the second digit corresponds to the pixel to
        //the right of that on the same layer, and so on until the last digit, which corresponds to the bottom-right
        //pixel of the last layer.
        //The image you received is 25 pixels wide and 6 pixels tall.
        //To make sure the image wasn't corrupted during transmission, the Elves would like you to find the layer that
        //contains the fewest 0 digits. On that layer, what is the number of 1 digits multiplied by the number of 2
        //digits?

        protected override string InputFile => "day8.txt";

        public override string ResultText => $"Test finished. Number of 1 * 2 is {Result}";

        protected int Width = 25;
        protected int Height = 6;
        protected SpaceImage Image;

        public override void Solve()
        {
            string input = PuzzleInput[0];
            Image = new SpaceImage(Width, Height, input);
            Result = GetResult();
        }

        protected virtual int GetResult()
        {
            var layer = Image.GetLeastCorruptedLayer();
            return MultiplicationCount(layer);
        }

        public int MultiplicationCount(int[,] layer)
        {
            int oneCount = 0;
            int twoCount = 0;
            foreach (int point in layer)
            {
                if (point == 1)
                    oneCount++;
                if (point == 2)
                    twoCount++;
            }

            return oneCount * twoCount;
        }
    }

    public class SpaceImageReader : SpaceImageValidator
    {
        public override string ResultText => ShowImage();

        protected override int GetResult()
        {
            //doesn't really matter
            return 0;
        }

        private string ShowImage()
        {
            int[,] clearImage = Image.ReadImage();
            return Imager.ImageToString(clearImage);
        }
    }
}