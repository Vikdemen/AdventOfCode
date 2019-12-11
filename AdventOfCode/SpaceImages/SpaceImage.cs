using System.Linq;

namespace AdventOfCode.SpaceImages
{
    public class SpaceImage
    {
        public int NumberOfLayers { get; }
        public int Width { get; }
        public int Height { get; }

        public int[,,] Image { get; }

        public SpaceImage(int width, int height, string input)
        {
            //parse from string
            int[] data = input.Select(digit => (int) char.GetNumericValue(digit)).ToArray();
            
            //initialise
            int numberOfLayers = data.Length / (width * height);
            Width = width;
            Height = height;
            NumberOfLayers = numberOfLayers;


            Image = new int [numberOfLayers, height, width];

            //fill it with data
            for(int z = 0; z < numberOfLayers; z++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                        Image[z, y, x] = data[x + y*width + z*width*height];
                }
            }
        }

        public int[,] GetLayer(int z)
        {
            var layer = new int[Height, Width];
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    layer[y, x] = Image[z, y, x];
                }
            }

            return layer;
        }
        
        private int[] GetStack(int y, int x)
        {
            int[] stack = new int[NumberOfLayers];
            for(int z = 0; z < NumberOfLayers; z++)
            {
                stack[z] = Image[z, y, x];
            }

            return stack;
        }
        
        public int[,] GetLeastCorruptedLayer()
        {
            int leastZeroCount = 100500;
            int[,] leastCorruptedLayer = GetLayer(0);
            for(int z = 0; z < NumberOfLayers; z++)
            {
                int[,]layer = GetLayer(z);

                int zeroCount = CheckCorruption(layer);
                
                if (zeroCount < leastZeroCount)
                {
                    leastZeroCount = zeroCount;
                    leastCorruptedLayer = layer;
                }
            }
            return leastCorruptedLayer;
        }

        public int[,] ReadImage()
        {
            int[,] newImage = new int[Height, Width];
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    newImage[y, x] = ReadPixel(GetStack(y, x));
                }
            }

            return newImage;
        }

        private int CheckCorruption(int[,] layer)
        {
            int zeroCount = 0;
            foreach (var pixel in layer)
            {
                if (pixel == 0)
                    zeroCount++;
            }
            return zeroCount;
        }

        private int ReadPixel(int[] pixelStack)
        {
            //2 is transparent, 0 is white, 1 is black
            int visiblePixel = 2;
            foreach (int pixel in pixelStack)
            {
                switch (pixel)
                {
                    case 2:
                        break;
                    case 1:
                        return 1;
                    case 0:
                        return 0;
                }
            }

            return visiblePixel;
        }
    }
}