using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI2048.Test
{
    [TestFixture]
    public class LineMovements
    {
        [TestCase(new[] { 0, 4, 0, 0 }, new[] { 4, 0, 0, 0 })]
        [TestCase(new[] { 0, 0, 4, 0 }, new[] { 4, 0, 0, 0 })]
        [TestCase(new[] { 0, 2, 0, 4 }, new[] { 2, 4, 0, 0 })]
        public void Move(int[] origin, int[] result)
        {
            var dut = new List<int>(origin);
            Movement.Move(dut);
            CollectionAssert.AreEqual(result, dut.ToArray());

        }


        [TestCase(new[] { 4, 4, 0, 0 }, new[] { 8, 0, 0, 0 })]
        [TestCase(new[] { 4, 4, 4, 0 }, new[] { 8, 4, 0, 0 })]
        [TestCase(new[] { 4, 4, 4, 4 }, new[] { 8, 8, 0, 0 })]
        [TestCase(new[] { 4, 0, 4, 0 }, new[] { 8, 0, 0, 0 })]
        public void Merge(int[] origin, int[] result)
        {
            var dut = new List<int>(origin);
            Movement.Move(dut);
            CollectionAssert.AreEqual(result, dut.ToArray());
        }

        [TestCase(new[] { 4, 4, 0, 0 }, 8)]
        [TestCase(new[] { 4, 4, 4, 4 }, 16)]
        [TestCase(new[] { 4, 0, 4, 0 }, 8)]
        public void Score(int[] line, int score)
        {
            var dut = new List<int>(line);
            Assert.AreEqual(score,Movement.Move(dut));
        }

       

       




    }
        
}
