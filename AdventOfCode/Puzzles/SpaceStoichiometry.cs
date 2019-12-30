namespace AdventOfCode.Puzzles
{
    public class SpaceStoichiometry: Puzzle, IPuzzle
    {
        //--- Day 14: Space Stoichiometry ---
        
        //You ask the nanofactory to produce a list of the reactions it can perform that are relevant to this process
        //(your puzzle input). Every reaction turns some quantities of specific input chemicals into some quantity of an
        //output chemical. Almost every chemical is produced by exactly one reaction; the only exception, ORE, is the
        //raw material input to the entire process and is not produced by a reaction.
        
        //You just need to know how much ORE you'll need to collect before you can produce one unit of FUEL.
        
        //Each reaction gives specific quantities for its inputs and output; reactions cannot be partially run, so only
        //whole integer multiples of these quantities can be used. (It's okay to have leftover chemicals when you're
        //done, though.) For example, the reaction 1 A, 2 B, 3 C => 2 D means that exactly 2 units of chemical D can be
        //produced by consuming exactly 1 A, 2 B and 3 C. You can run the full reaction as many times as necessary; for
        //example, you could produce 10 D by consuming 5 A, 10 B, and 15 C.
        protected override string InputFile => "day14.txt";
        
        public int oreNeeded { get; private set; }
        public override string Solve()
        {
            return $"We need {oreNeeded} ORE for one unit of FUEL";
        }
    }
}