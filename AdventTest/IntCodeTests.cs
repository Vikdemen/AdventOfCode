using NUnit.Framework;
using AdventOfCode;
using AdventOfCode.IntCodes;

namespace AdventTest
{
    [TestFixture]
    public class IntCodeTests
    {
        [TestCase("1,0,0,0,99", new long[] {2, 0, 0, 0, 99})]
        [TestCase("2,3,0,3,99", new long[] {2, 3, 0, 6, 99})]
        [TestCase("2,4,4,5,99,0", new long[] {2, 4, 4, 5, 99, 9801})]
        [TestCase("1,1,1,4,99,5,6,0,99", new long[] {30, 1, 1, 4, 2, 5, 6, 0, 99})]
        public void IntCodeTest(string instructions, long[] output)
        {
            long[] program = InstructionParser.Parse(instructions);
            var memory = new Memory(program);
            memory.Start();
            var result = memory.MemoryRegister.ToArray();
            CollectionAssert.AreEqual(output, result);
        }

        [TestCase("3,0,4,0,99", 7, 7)]
        [TestCase("3,9,8,9,10,9,4,9,99,-1,8", 8, 1)]
        [TestCase("3,9,8,9,10,9,4,9,99,-1,8", 88, 0)]
        [TestCase("3,9,7,9,10,9,4,9,99,-1,8", -88, 1)]
        [TestCase("3,9,7,9,10,9,4,9,99,-1,8", 88, 0)]
        [TestCase("3,3,1108,-1,8,3,4,3,99", 8, 1)]
        [TestCase("3,3,1108,-1,8,3,4,3,99", 88, 0)]
        [TestCase("3,3,1107,-1,8,3,4,3,99", -88, 1)]
        [TestCase("3,3,1107,-1,8,3,4,3,99", 88, 0)]
        [TestCase("3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9", 0, 0)]
        [TestCase("3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9", 88, 1)]
        [TestCase("3,3,1105,-1,9,1101,0,0,12,4,12,99,1", 0, 0)]
        [TestCase("3,3,1105,-1,9,1101,0,0,12,4,12,99,1", 88, 1)]
        public void ParameterTest(string instructions, int input, int output)
        {
            long[] program = InstructionParser.Parse(instructions);
            var memory = new Memory(program);
            memory.Input = input;
            memory.Start();
            long result = memory.Output;
            Assert.AreEqual(output, result);
        }

        [TestCase(
            "3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99",
            8, 999, 1000, 1001)]
        public void TrinaryCheck(string instructions, int comparison, int less, int equal, int greater)
        {
            long[] program = InstructionParser.Parse(instructions);
            var memory = new Memory(program);
            memory.Input = comparison - 1;
            memory.Start();
            long resultLess = memory.Output;
            memory = new Memory(program);
            memory.Input = comparison;
            memory.Start();
            long resultEqual = memory.Output;
            memory = new Memory(program);
            memory.Input = comparison + 1;
            memory.Start();
            long resultGreater = memory.Output;
            Assert.AreEqual(less, resultLess);
            Assert.AreEqual(equal, resultEqual);
            Assert.AreEqual(greater, resultGreater);
        }

        [Test]
        public void PositionMode()
        {
            int testValue = 7;
            var memory = new Memory(new long[] {004, 2, testValue});
            var intCodeInstruction = new IntCodeInstruction(004);
            intCodeInstruction.Execute(memory, 0);
            long result = memory.Output;
            Assert.AreEqual(result, testValue);
        }

        [Test]
        public void ImmediateMode()
        {
            int testValue = 7;
            var memory = new Memory(new long[] {004, testValue});
            var intCodeInstruction = new IntCodeInstruction(104);
            intCodeInstruction.Execute(memory, 0);
            long result = memory.Output;
            Assert.AreEqual(result, testValue);
        }
        
        [Test]
        public void RelativeMode()
        {
            int testValue = 7;
            var memory = new Memory(new long[] {004, 2, 0, testValue});
            memory.RelativeBase = 1;
            var intCodeInstruction = new IntCodeInstruction(204);
            intCodeInstruction.Execute(memory, 0);
            long result = memory.Output;
            Assert.AreEqual(testValue, result);
        }

        [Test]
        public void Op9()
        {
            int testValue = 77;
            var memory = new Memory(new long[]{9, 2, testValue});
            var instruction = new IntCodeInstruction((int)memory[0]);
            instruction.Execute(memory,0);
            long result = memory.RelativeBase;
            Assert.AreEqual(testValue, result);
        }

        [TestCase("203,0,99,5,0", 77)]
        [TestCase("003,4,99,5,0", 77)]
        public void Op3(string instuctions, int testValue)
        {
            long[] program = InstructionParser.Parse(instuctions);
            var memory = new Memory(program);
            memory.Input = testValue;
            memory.RelativeBase = 4;
            memory.Start();
            Assert.AreEqual(testValue, memory.MemoryRegister[4]);
        }

        [TestCase("1102,34915192,34915192,7,4,7,99,0", 1219070632396864)]
        [TestCase("104,1125899906842624,99", 1125899906842624)]
        public void BigNumber(string instructions, long expected)
        {
            long[] program = InstructionParser.Parse(instructions);
            var memory = new Memory(program);
            memory.Start();
            long result = memory.Output;
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void OutputCopy()
        {
            string instructions = "109,1,204,-1,1001,100,1,100,1008,100,16,101,1006,101,0,99";
            long[] program = InstructionParser.Parse(instructions);
            var memory = new Memory(program);

            memory.Start();
            long result = memory.Output;
            Assert.AreEqual(109, result);
        }
    }
}