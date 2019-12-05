using System.Globalization;
using System.Linq;

namespace AdventOfCode
{
    public class Password
    {
        private int[] numbers = new int[6];
        public bool IsValid => CheckAdjacent() & Increasing();
        public bool IsMoreValid => IsValid & NoLargerGroup();

        //splits the int into an array of digits
        public void Input (int number)
        {
            for (int i = 0; i < 6; i++)
            {
                string numberString = number.ToString();
                numbers[i] = (int)char.GetNumericValue(numberString[i]);
            }
        }

        //if there is 2 adjacent numbers, returns true, if not - return false
        private bool CheckAdjacent()
        {
            for(int i = 1; i < 6; i++)
            {
                if (numbers[i] == numbers[i - 1])
                    return true;
            }
            return false;
        }

        private bool NoLargerGroup()
        {
            int i = 0;
            foreach (int number in numbers)
            {
                int count = 0;
                foreach (int anotherNumber in numbers)
                {
                    if (number == anotherNumber)
                        count++;
                }

                if (count == 2)
                    return true;
            }
            return false;
        }

        private bool Increasing()
        {
            for(int i = 1; i < 6; i++)
            {
                if (numbers[i - 1] > numbers[i])
                    return false;
            }
            return true;
        }

        public static int CountValid (int start, int end)
        {
            Password password = new Password();
            int valid = 0;
            foreach (int number in Enumerable.Range(start, end - start + 1))
            {
                password.Input(number);
                if (password.IsValid)
                    valid++;
            }
            return valid;
        }

        public static int CountMoreValid(int start, int end)
        {
            Password password = new Password();
            int valid = 0;
            foreach (int number in Enumerable.Range(start, end - start + 1))
            {
                password.Input(number);
                if (password.IsMoreValid)
                    valid++;
            }
            return valid;
        }
    }
}