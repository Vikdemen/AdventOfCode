using System;

namespace AdventOfCode.Puzzles
{
    public class FuelCalculator: Puzzle, IPuzzle
    {
        //--- Day 1: The Tyranny of the Rocket Equation ---
        //Fuel required to launch a given module is based on its mass. Specifically, to find the fuel required for a
        //module, take its mass, divide by three, round down, and subtract 2.
        //What is the sum of the fuel requirements for all of the modules on your spacecraft?
        
        
        
        protected override string InputFile => "day1.txt";
        public override string ResultText => $"We need {Result.ToString()} fuel in total";

        public override void Solve()
        {
            Result = Rocket.FuelForModules(Array.ConvertAll(PuzzleInput, int.Parse));
        }
    }

    public class FuelForFuelCalculator : FuelCalculator
    {
        //--- Part Two ---
        //Fuel itself requires fuel just like a module - take its mass, divide by three, round down, and subtract 2.
        //However, that fuel also requires fuel, and that fuel requires fuel, and so on. Any mass that would require
        //negative fuel should instead be treated as if it requires zero fuel; the remaining mass, if any, is instead
        //handled by wishing really hard, which has no mass and is outside the scope of this calculation.
        //What is the sum of the fuel requirements for all of the modules on your spacecraft when also taking into
        //account the mass of the added fuel? (Calculate the fuel requirements for each module separately, then add them
        //all up at the end.)
        public override void Solve()
        {
            Result = Rocket.FuelForModulesRecursive(Array.ConvertAll(PuzzleInput, int.Parse));
        }
    }
}