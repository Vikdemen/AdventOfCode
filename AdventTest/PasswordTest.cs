using System.Xml.Schema;
using AdventOfCode;
using NUnit.Framework;

namespace AdventTest
{
    public class PasswordTest
    {
        [TestCase(111111, true)]
        [TestCase(223450, false)]
        [TestCase(123789, false)]
        public void ValidityTest(int number, bool valid)
        {
            Password password = new Password();
            password.Input(number);
            bool result = password.IsValid;
            Assert.AreEqual(valid, result);
        }

        [TestCase(112233, true)]
        [TestCase(123444, false)]
        [TestCase(111122, true)]
        public void AdditionalCondition(int number, bool valid)
        {
            Password password = new Password();
            password.Input(number);
            bool result = password.IsMoreValid;
            Assert.AreEqual(valid, result);
        }
    }
}