using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public static class Rocket
    {
        
        //Fuel required to launch a given module is based on its mass. Specifically, to find the fuel required for a
        //module, take its mass, divide by three, round down, and subtract 2.
        
        //given an array of module mass, calculates total fuel requirements
        public static int FuelForModules(IEnumerable<int> data)
        {
            return data.Sum(FuelForModule);
        }
        
        //Fuel itself requires fuel just like a module - take its mass, divide by three, round down, and subtract 2.
        //However, that fuel also requires fuel, and that fuel requires fuel, and so on. Any mass that would require
        //negative fuel should instead be treated as if it requires zero fuel; the remaining mass, if any, is instead
        //handled by wishing really hard, which has no mass and is outside the scope of this calculation.
        //So, for each module mass, calculate its fuel and add it to the total. Then, treat the fuel amount you just
        //calculated as the input mass and repeat the process, continuing until a fuel requirement is zero or negative.
        //What is the sum of the fuel requirements for all of the modules on your spacecraft when also taking into
        //account the mass of the added fuel? (Calculate the fuel requirements for each module separately,
        //then add them all up at the end.)

        public static int FuelForModulesRecursive(IEnumerable<int> data)
        {
            return data.Sum(FuelForModuleRecursive);
        }

        //calculates the fuel mass itself
        public static int FuelForModule(int moduleMass) => (moduleMass / 3) - 2;

        //calculates the fuel mass, including fuel for fuel
        public static int FuelForModuleRecursive(int moduleMass)
        {
            int totalFuel = 0;
            int fuelMass = FuelForModule(moduleMass);
            while (fuelMass > 0)
            {
                totalFuel += fuelMass;
                fuelMass = FuelForModule(fuelMass);
            }
            return totalFuel;
        }

    }
}