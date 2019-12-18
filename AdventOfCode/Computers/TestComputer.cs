using System.Collections;
using System.Collections.Generic;

namespace AdventOfCode.Computers
{
    public class TestComputer: IComputer
    {
        //a mock computer that produces a series of outputs for tesing painting robot
        public bool Finished { get; set; }

        public List<int> inputList = new List<int>();
        //records all inputs

        private Queue<long> outputQueue = new Queue<long>();

        public TestComputer(params int[] data)
        {
            foreach (var value in data)
            {
                outputQueue.Enqueue(value);
            }
        }


        public long[] Run(params int[] inputs)
        {
            inputList.AddRange(inputs);
            var output = new long[2];
            output[0] = outputQueue.Dequeue();
            output[1] = outputQueue.Dequeue();
            if (outputQueue.Count == 0)
                Finished = true;
            return output;
        }
    }
}