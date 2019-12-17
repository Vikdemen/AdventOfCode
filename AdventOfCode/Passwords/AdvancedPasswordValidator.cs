using System.Linq;

namespace AdventOfCode.Passwords
{
    public class AdvancedPasswordValidator: PasswordValidator
    {
        //a password validator with expanded sets of rules
        
        //"has a pair of numbers" rule doesn't apply when the group of numbers is longer than 2.
        //thus, a password must contain a group of the same numbers exactly 2 members long
        protected override bool CheckForPair()
        {
            return Password.Select(number => Password.Count(anotherNumber => number == anotherNumber))
                .Any(count => count == 2);
        }
    }
}