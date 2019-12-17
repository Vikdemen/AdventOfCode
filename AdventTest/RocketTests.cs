using System;
using NUnit.Framework;
using AdventOfCode;
using AdventOfCode.Fuel;

namespace AdventTest
{
    [TestFixture]
    public class RocketTests
    {
        [TestCase(12, 2)]
        [TestCase(14, 2)]
        [TestCase(1969, 654)]
        [TestCase(100756, 33583)]
        public void FuelMass(int moduleMass, int expectedFuelMass)
        {
            int requiredFuel = Rocket.FuelForModule(moduleMass);
            Assert.AreEqual(expectedFuelMass, requiredFuel);
        }

        [TestCase(14,2)]
        [TestCase(1969, 966)]
        [TestCase(100756, 50346)]
        public void FuelMassRecursive(int moduleMass, int expectedFuelMass)
        {
            int requiredFuel = Rocket.FuelForModuleRecursive(moduleMass);
            Assert.AreEqual(expectedFuelMass, requiredFuel);
        }

        /*
        [Test]
        public void Day1Pt1()
        {
            string resultText = AdventSolver.ExecuteCommand("calculate-fuel");
            StringAssert.Contains("3331523", resultText);
        }

        [Test]
        public void Day1Pt2()
        {
            string resultText = AdventSolver.ExecuteCommand("fuel-for-fuel");
            StringAssert.Contains("4994396", resultText);
        }
        */
    }
}