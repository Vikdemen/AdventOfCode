﻿using NUnit.Framework;
using AdventOfCode;

namespace AdventTest
{
    [TestFixture]
    public class IntCodeTests
    {
        [TestCase(new int[] {1,0,0,0,99}, new int[] {2,0,0,0,99})]
        [TestCase(new int[] {2,3,0,3,99}, new int[] {2,3,0,6,99})]
        [TestCase(new int[] {2,4,4,5,99,0}, new int[] {2,4,4,5,99,9801})]
        [TestCase(new int[] {1,1,1,4,99,5,6,0,99}, new int[] {30,1,1,4,2,5,6,0,99})]
        public void IntCodeTest(int[] input, int[] output)
        {
            IntCode computer = new IntCode();
            computer.Run(input);
            int[] result = computer.Memory;
            CollectionAssert.AreEqual(output, result);
        }
        
        [TestCase("3,0,4,0,99", 7, 7)]
        [TestCase("3,9,8,9,10,9,4,9,99,-1,8", 8, 1)]
        [TestCase("3,9,8,9,10,9,4,9,99,-1,8", 9, 0)]
        [TestCase("3,9,7,9,10,9,4,9,99,-1,8", 7, 1)]
        [TestCase("3,9,7,9,10,9,4,9,99,-1,8", 9, 0)]
        [TestCase("3,3,1108,-1,8,3,4,3,99", 8, 1)]
        [TestCase("3,3,1108,-1,8,3,4,3,99", 9, 0)]
        [TestCase("3,3,1107,-1,8,3,4,3,99", 7, 1)]
        [TestCase("3,3,1107,-1,8,3,4,3,99", 9, 0)]
        [TestCase("3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9", 0, 0)]
        [TestCase("3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9", 9, 1)]
        [TestCase("3,3,1105,-1,9,1101,0,0,12,4,12,99,1", 0, 0)]
        [TestCase("3,3,1105,-1,9,1101,0,0,12,4,12,99,1", 9, 1)]
        public void ParameterTest(string program, int input, int output)
        {
            IntCode computer = new IntCode();
            computer.Run(program, input);
            int result = computer.Output;
            Assert.AreEqual(output, result);
        }

        [TestCase("3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99",
            8, 999, 1000, 1001)]
        public void TrinaryCheck(string program, int comparison, int less, int equal, int greater)
        {
            IntCode computer = new IntCode();
            computer.Run(program, comparison - 1);
            int resultLess = computer.Output;
            computer.Run(program, comparison);
            int resultEqual = computer.Output;
            computer.Run(program, comparison + 1);
            int resultGreater = computer.Output;
            Assert.AreEqual(less, resultLess);
            Assert.AreEqual(equal, resultEqual);
            Assert.AreEqual(greater, resultGreater);
        }
    }
}