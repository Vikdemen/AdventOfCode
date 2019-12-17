using System.Text;

namespace AdventOfCode.Puzzles
{
    public static class Imager
    {
        public static string ShowImage(int[,] image)
        {
            int width = image.GetLength(0);
            int height = image.GetLength(1);
            var ascii = new StringBuilder();
            for (int y = 0; y < height; y++)
            {
                string row = "";
                for (int x = 0; x < width; x++)
                {
                    row += image[x, y].ToString();
                }
                //TODO - fix the dimensions on password image

                //primitive graphics
                row = row.Replace('1', '8');
                row = row.Replace('0', ' ');
                ascii.AppendLine(row);
            }

            return ascii.ToString();
        }
            
    }
}