using System;
using System.Collections.Generic;

namespace AI2048.Test
{
    internal class DeterministicRandomizer : IRandomizer
    {
        Queue<int> ReturnValues;
        public DeterministicRandomizer()
        {
            ReturnValues = new Queue<int>();
        }
        public void Enqueue(int value)
        {
            ReturnValues.Enqueue(value);

        }
        public void Enqueue(IEnumerable<int> values)
        {
            foreach (var item in values)
            {
                Enqueue(item);
            }
        }

        
        public int GenerateValue(int max)
        {
            if (ReturnValues.Count == 0)
            {
                throw new InvalidOperationException("A new value was requested, but the queue of ReturnValues is empty!");
            }
            var value = ReturnValues.Dequeue();
            if (value > max)
            {
                throw new InvalidOperationException(string.Format(
                    "A new value with 'max'={0} was requested, but the next element in the queue is higher: {1}",
                    max, value));
            }
            return value;
        }
    }
}