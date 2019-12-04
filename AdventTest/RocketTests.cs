using System;
using NUnit.Framework;
using AdventOfCode;

namespace AdventTest
{
    [TestFixture]
    public class RocketTests
    {
        [Test]
        public void FuelMass()
        {
            int mass = Rocket.FuelForModule(100756);
            Assert.AreEqual(33583, mass);
        }

        [Test]
        public void FuelMassRecursive()
        {
            int mass = Rocket.FuelForModuleRecursive(100756);
            Assert.AreEqual(50346, mass);
        }
    }
}