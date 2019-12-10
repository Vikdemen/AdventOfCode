using System.Collections.Generic;

namespace AdventOfCode
{
    public class OrbitMapper
        {
            private IDictionary<string, string> orbitMap;
            //Except for the universal Center of Mass (COM), every object in space is in orbit around exactly one other
            //object
            
            //In the map data, this orbital relationship is written AAA)BBB, which means "BBB is in orbit around AAA".
            public void GenerateMap(string[] data)
            {
                //bodies are represented as dictionary entries with child as key and parent as value
                orbitMap = new Dictionary<string, string> {{"COM", string.Empty}};
                //COM has nothing to orbit;
                foreach (string record in data)
                {
                    //parsing
                    string[] split = record.Split(')');
                    string childBody = split[1];
                    string parentBody = split[0];
                    
                    orbitMap.Add(childBody, parentBody);
                }
            }
            
            private string GetParent(string child) => orbitMap[child];
            
            //To verify maps, the Universal Orbit Map facility uses orbit count checksums - the total number of direct
            //orbits (like the one shown above) and indirect orbits.
            //Whenever A orbits B and B orbits C, then A indirectly orbits C. This chain can be any number of objects
            //long: if A orbits B, B orbits C, and C orbits D, then A indirectly orbits D.
            public int GetCheckSum()
            {
                int checkSum = 0;
                foreach (KeyValuePair<string, string> record in orbitMap)
                {
                    string body = record.Key;
                    checkSum += CountParents(body);
                }
                return checkSum;
            }

            //jumps from child body to its parents, until COM is reached
            private int CountParents(string start)
            {
                int count = 0;
                string body = start;
                while (GetParent(body) != string.Empty)
                {
                    count++;
                    body = GetParent(body);
                }
                return count;
            }

            //Now, you just need to figure out how many orbital transfers you (YOU) need to take to get to Santa (SAN)
            //You start at the object YOU are orbiting; your destination is the object SAN is orbiting. An orbital
            //transfer lets you move from any object to an object orbiting or orbited by that object.
            //What is the minimum number of orbital transfers required to move from the object YOU are orbiting to the
            //object SAN is orbiting? (Between the objects they are orbiting - not between YOU and SAN.)

            public int CountTransfers(string start, string end)
            {
                //makes a chain of transfers between end body and COM
                var targetOrbits = new List<string>();
                string body = end;
                while (body != string.Empty)
                {
                    targetOrbits.Add(body);
                    body = GetParent(body);
                }

                body = start;
                int count = 0;
                //counts the transfers between start body and closest body that your target indirectly orbits
                while (!targetOrbits.Contains(body))
                {
                    body = GetParent(body);
                    count++;
                }
                //then you count the transfers between that common body and your target
                count += targetOrbits.IndexOf(body);
                return count;
            }
        }
}