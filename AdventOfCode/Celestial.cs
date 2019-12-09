namespace AdventOfCode
{
    public class Celestial
    {
        public string Name { get; }

        public Celestial ParentBody { get; set; }

        public int ParentCount => CountParents();

        public Celestial(string name)
        {
            Name = name;
        }

        private int CountParents()
        {
            int count = 0;
            Celestial celestial = this;
            while (celestial.ParentBody != null)
            {
                count++;
                celestial = celestial.ParentBody;
            }

            return count;
        }
    }
}