using System;
using System.Collections.Generic;
using System.Threading;

namespace AdventOfCode
{
    public class IntCode
    {
        //An Intcode program is a list of integers separated by commas (like 1,0,0,3,99). To run one, start by looking
        //at the first integer (called position 0). Here, you will find an opcode - either 1, 2, or 99. The opcode
        //indicates what to do; for example, 99 means that the program is finished and should immediately halt.
        //Encountering an unknown opcode means something went wrong.
        private int[] opList;
        public int[] OpList => opList;
        private bool halted = false;

        public IntCode(int [] instructions)
        {
            opList = new int [instructions.Length];
            instructions.CopyTo(opList, 0);
            CodeStart();
        }
        

        public void CodeStart()
        {
            int i = 0;
            while (i+4 < opList.Length & !halted)
            { 
                Process(i);
                i += 4;
            }
        }

        private void Process(int index)
        {
            switch (opList[index])
            {
                case 1:
                    Op1(index);
                    break;
                case 2:
                    Op2(index);
                    break;
                case 99:
                    Halt();
                    break;
                default:
                    Halt();
                    break;
            }
        }

        //Opcode 1 adds together numbers read from two positions and stores the result in a third position.
        //The three integers immediately after the opcode tell you these three positions - the first two indicate
        //the positions from which you should read the input values, and the third indicates the position at which
        //the output should be stored.
        private void Op1(int index)
        {
            int value1Index = opList[index + 1];
            int value2Index = opList[index + 2];
            int resultIndex = opList[index + 3];
            opList[resultIndex] = opList[value1Index] + opList[value2Index];
        }
        
        //Opcode 2 works exactly like opcode 1, except it multiplies the two inputs instead of adding them.
        //Again, the three integers after the opcode indicate where the inputs and outputs are, not their values.
        private void Op2(int index)
        {
            int value1Index = opList[index + 1];
            int value2Index = opList[index + 2];
            int resultIndex = opList[index + 3];
            int newValue = opList[value1Index] * opList[value2Index];
            opList[resultIndex] = newValue;
        }

        private void Halt()
        {
            halted = true;
        }
    }
}