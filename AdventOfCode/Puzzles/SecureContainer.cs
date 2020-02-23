using System.Linq;
using AdventOfCode.Passwords;

namespace AdventOfCode.Puzzles
{
    public class ValidPasswordCounter: Puzzle, IPuzzle
    {
        //--- Day 4: Secure Container ---
        //Password is a six-digit number.
        //The value is within the range given in your puzzle input.
        //Two adjacent digits are the same (like 22 in 122345).
        //Going from left to right, the digits never decrease; they only ever increase or stay the same
        //(like 111123 or 135679).
        //How many different passwords within the range given in your puzzle input meet these criteria?
        
        //--- Part Two ---
        //the two adjacent matching digits are not part of a larger group of matching digits.
        //111122 meets the criteria (even though 1 is repeated more than twice, it still contains a double 22).
        //How many different passwords within the range given in your puzzle input meet all of the criteria?
        
        protected override string InputFile => "day4.txt";
        protected IPasswordValidator Validator { get; }

        public ValidPasswordCounter()
        {
            Validator = new PasswordValidator();
        }
        public ValidPasswordCounter(IPasswordValidator validator)
        {
            Validator = validator;
        }
        
        public override string Solve()
        {
            string[] data = PuzzleInput[0].Split('-');
            int start = int.Parse(data[0]);
            int finish = int.Parse(data[1]);
            int validPasswords = CountValid(start, finish);
            return $"{validPasswords} passwords are valid";
        }

        private int CountValid (int start, int end)
        {
            int count = end - start + 1;
            int validPassCount = Enumerable.Range(start, count).Count(Validator.CheckValid);
            return validPassCount;
        }
        
    }
}