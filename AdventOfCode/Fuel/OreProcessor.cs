using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace AdventOfCode.Fuel
{
    public class OreProcessor
    {
        private IDictionary<string, Reaction> reactions;
        
        public OreProcessor(IEnumerable<string> reactionList)
        {
            var reactionsEnum = reactionList.Select(s => new Reaction(s));
            reactions = reactionsEnum.ToDictionary(r => r.Result.Name, r => r);
        }

        public int OrePerFuel()
        {
            return CalculateOre(("FUEL", 1));
        }

        private int CalculateOre((string Name, int Amount) chemical)
        {
            int oreCount = 0;
            IEnumerable<(string Name, int Amount)> reagents = GetReagents(chemical);
            foreach (var reagent in reagents)
            {
                if (reagent.Name == "ORE")
                {
                    oreCount += reagent.Amount;
                }
                else
                {
                    oreCount += CalculateOre(reagent);
                }
            }
            return oreCount;
        }

        //TODO - check math, it gives wrong results
        private IEnumerable<(string Name, int Amount)> GetReagents((string Name, int Amount) required)
        {
            (string name, int amount) = required;
            var reaction = reactions[name];
            (string Name, int Amount)[] reagents = reaction.Reagents
                .Select(r => (r.Name, r.Amount / amount)).ToArray();
            return reagents;
        }
    }

    public class Reaction
    {
        //output of reaction
        public (string Name, int Amount) Result { get; }
        //and required inputs
        public (string Name, int Amount)[] Reagents { get; }

        //lines use 7 A, 1 E => 1 FUEL format
        public Reaction(string reaction)
        {
            //splits and parses the string 
            
            string resultString = reaction.Substring(reaction.IndexOf('=') + 3);
            Result = StringToTuple(resultString);
            
            string reagentString = reaction.Substring(0, reaction.IndexOf('=') - 1);
            string[] reagentStrings = reagentString.Split(',');
            reagentStrings = reagentStrings.Select(s => s.Trim(' ')).ToArray();
            Reagents = reagentStrings.Select(StringToTuple).ToArray();
            
            static (string Name, int Amount) StringToTuple (string line)
            {
                string[] halves = line.Split(' ');
                return (halves[1], int.Parse(halves[0]));
            }
        }
    }
}