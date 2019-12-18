using System.Text;

namespace AdventOfCode
{
    public static class Imager
    {
        //a class responsible for transforming 2d array of integers in ASCII art
        
        //returns a string representation of ASCII image
        public static string ImageToString(int[,] image)
        {
            //we assume the x, y format
            int width = image.GetLength(0);
            int height = image.GetLength(1);
            //minor performance boost to avoid copying strings
            var ascii = new StringBuilder(width * height);
            
            for (int y = 0; y < height; y++)
            {
                var row = new StringBuilder(width);
                for (int x = 0; x < width; x++)
                {
                    row.Append(image[x, y]);
                }
                //TODO - fix the dimensions on password image

                //1 represent the filled pixels, 0 empty pixels
                row = row.Replace('1', '@');
                row = row.Replace('0', ' ');
                ascii.AppendLine(row.ToString());
            }
            return ascii.ToString();
        }
            
    }
}