using System.Linq;

namespace AdventOfCode.Puzzles
{
    public class SpaceImage
    {
        public readonly int[][,] ImageLayers;
        //i use a jagged array because it's harder to get a layer from multidimensional one

        public SpaceImage(int width, int height, string input)
        {
            //parse from string
            int[] data = input.Select(digit => (int) char.GetNumericValue(digit)).ToArray();
            
            //initialise
            int numberOfLayers = data.Length / (width * height);
            ImageLayers = new int [numberOfLayers][,];
            for (int l = 0; l < numberOfLayers; l++)
            {
                ImageLayers[l] = new int[height, width];
            }
            
            //fill it with data
            for(int i = 0; i < numberOfLayers; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    for (int k = 0; k < width; k++)
                        ImageLayers[i][j,k] = data[k + j*width + i*width*height];
                }
            }
        }
        
        public int[,] GetLeastCorruptedLayer()
        {
            int leastZeroCount = 100500;
            int[,] leastCorruptedLayer = ImageLayers[0];
            foreach (int[,] layer in ImageLayers)
            {
                int zeroCount = 0;
                foreach (int point in layer)
                {
                    if (point == 0)
                        zeroCount++;
                }

                if (zeroCount < leastZeroCount)
                {
                    leastZeroCount = zeroCount;
                    leastCorruptedLayer = layer;
                }
            }
            return leastCorruptedLayer;
        }
    }
}