namespace AdventOfCode.Asteroids
{
    public class AsteroidMap
    {
        //a class responsible for showing positions of asteroids
        //unfinished
        private bool[,] map;

        public AsteroidMap(bool[,] map)
        {
            this.map = map;
        }

        public int CountAsteroidsInLOS(int x, int y)
        {
            int count = 0;
            for (int j = 0; j < y; j++)
            {
                for (int i = 0; i < x; i++)
                {
                    //if the position has asteroid
                    if (map[i, j])
                        if(CheckLOS())
                        {
                        }

                    count++;
                }
            }

            return count;
        }

        private static bool CheckLOS()
        {
            //not implemented
            return false;
        }
    }
}