using System.Collections.Generic;

namespace AdventOfCode
{
    public class OrbitMap
    {
        public List<Celestial> Celestials;

        public int GetCheckSum()
        {
            int checkSum = 0;
            foreach (Celestial celestial in Celestials)
            {
                checkSum += celestial.CountParents();
            }
            return checkSum;
        }

        public void AddRecord(string record)
        {
            string[] records = record.Split(')');
            var newBody = new Celestial(records[1]);
        }
    }

    public class Celestial
    {
        public string Name;

        public Celestial ParentBody;

        public Celestial(string name)
        {
            Name = name;
        }

        public int CountParents()
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