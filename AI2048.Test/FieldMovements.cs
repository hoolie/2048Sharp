using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI2048.Test
{
    [TestFixture]
    public class FieldMovements
    {
        int[] default_field = new[] {
            0,2,2,0,
            2,2,0,0,
            0,2,4,0,
            0,4,0,4
        };
        
        

        [Test]
        public void MoveLeft()
        {
            int[] expected = new[] {
                4,0,0,0,
                4,0,0,0,
                2,4,0,0,
                8,0,0,0
            };
            var test_field = default_field.ToList();
            Movement.MoveLeft(test_field);
            CollectionAssert.AreEqual(expected, test_field.ToArray());

        }
        [Test]
        public void ScoreLeft()
        {
            var test_field = default_field.ToList();
            Assert.AreEqual(16,Movement.MoveLeft(test_field));
        }

        [Test]
        public void MoveRight()
        {
            int[] expected = new[] {
                0,0,0,4,
                0,0,0,4,
                0,0,2,4,
                0,0,0,8
            };
            var test_field = default_field.ToList();
            Movement.MoveRight(test_field);
            CollectionAssert.AreEqual(expected, test_field.ToArray());
        }
        [Test]
        public void ScoreRight()
        {
            var test_field = default_field.ToList();
            Assert.AreEqual(16, Movement.MoveRight(test_field));
        }
        [Test]
        public void MoveDown()
        {
            int[] expected = new[] {
                0,0,0,0,
                0,2,0,0,
                0,4,2,0,
                2,4,4,4
            };
            var test_field = default_field.ToList();
            Movement.MoveDown(test_field);
            CollectionAssert.AreEqual(expected, test_field.ToArray());
        }
        [Test]
        public void ScoreDown()
        {
            var test_field = default_field.ToList();
            Assert.AreEqual(4, Movement.MoveDown(test_field));
        }

        [Test]
        public void MoveUp()
        {
            int[] expected = new[] {
                2,4,2,4,
                0,2,4,0,
                0,4,0,0,
                0,0,0,0
            };
            var test_field = default_field.ToList();
            Movement.MoveUp(test_field);
            CollectionAssert.AreEqual(expected, test_field.ToArray());
        }
        [Test]
        public void ScoreUp()
        {
            var test_field = default_field.ToList();
            Assert.AreEqual(4, Movement.MoveUp(test_field));
        }

    }
}
