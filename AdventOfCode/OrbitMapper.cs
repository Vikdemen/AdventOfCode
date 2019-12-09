using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class OrbitMapper
    {
        public HashSet<Celestial> OrbitMap;

        //Except for the universal Center of Mass (COM), every object in space is in orbit around exactly one other
        //object.

        //hardcoded for 3-letter body names
        public void GenerateMap(string[] data)
        {
            OrbitMap = new HashSet<Celestial>() {new Celestial("COM")};
            foreach (var record in data)
            {
                OrbitMap.Add(new Celestial(record.Substring(4)));
            }

            foreach (var record in data)
            {
                Celestial childBody = OrbitMap.FirstOrDefault(i => i.Name == record.Substring(4));
                Celestial parentBody = OrbitMap.FirstOrDefault(j => j.Name == record.Substring(0, 3));
                childBody.ParentBody = parentBody;
            }
        }
        
        //To verify maps, the Universal Orbit Map facility uses orbit count checksums - the total number of direct
        //orbits (like the one shown above) and indirect orbits.
        //Whenever A orbits B and B orbits C, then A indirectly orbits C. This chain can be any number of objects long:
        //if A orbits B, B orbits C, and C orbits D, then A indirectly orbits D.
        public int GetCheckSum()
        {
            int checkSum = 0;
            foreach (Celestial celestial in OrbitMap)
            {
                checkSum += celestial.ParentCount;
            }
            return checkSum;
        }
    }
}