using System.Xml.Schema;
using AdventOfCode;
using AdventOfCode.Passwords;
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
            var passwordValidator = new PasswordValidator();
            bool result = passwordValidator.CheckValid(number);
            Assert.AreEqual(valid, result);
        }

        [TestCase(112233, true)]
        [TestCase(123444, false)]
        [TestCase(111122, true)]
        public void AdditionalCondition(int number, bool valid)
        {
            var passwordValidator = new AdvancedPasswordValidator();
            bool result = passwordValidator.CheckValid(number);
            Assert.AreEqual(valid, result);
        }
    }
}