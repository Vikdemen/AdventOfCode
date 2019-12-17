namespace AdventOfCode.Passwords
{
    public interface IPasswordValidator
    { 
        bool CheckValid(int password);
    }
    public class PasswordValidator: IPasswordValidator
    {
        protected readonly int[] Password = new int [6];

        public bool CheckValid(int password)
        {
            Input(password);
            return Increasing() && CheckForPair();
        }

        //splits the int into an array of digits
        private void Input (int number)
        {
            string numberString = number.ToString();
            for (int i = 0; i < 6; i++)
            {
                Password[i] = (int) char.GetNumericValue(numberString[i]);
            }
        }

        //if there are 2 of same number, returns true, if not - return false
        protected virtual bool CheckForPair()
        {
            for(int i = 1; i < 6; i++)
            {
                if (Password[i] == Password[i - 1])
                    return true;
            }
            return false;
        }
        
        protected bool Increasing()
        {
            for(int i = 1; i < 6; i++)
            {
                if (Password[i - 1] > Password[i])
                    return false;
            }
            return true;
        }
    }
}