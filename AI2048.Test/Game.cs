using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI2048.Test
{
    [TestFixture]
    public class GameTest
    {
        Game g;
        DeterministicRandomizer random_mock;
        [SetUp]
        public void Setup()
        {
            random_mock = new DeterministicRandomizer();
            random_mock.Enqueue(new[] { 0, 0, 4, 0 });
            g = new Game(random_mock);
        }

        [Test]
        public void CheckInitialBoard()
        {
            var expected = new[]
            {
                2,0,0,0,
                0,2,0,0,
                0,0,0,0,
                0,0,0,0
            };
            CollectionAssert.AreEqual(expected, g.Board);
        }
        [Test]
        public void RespawnTileAfterMove()
        {
            var expected = new[]
            {
                0,0,2,0,
                0,0,0,0,
                0,0,0,0,
                2,2,0,0
            };
            random_mock.Enqueue(new[]{ 2, 0});
            g.Move(Directions.Down);
            CollectionAssert.AreEqual(expected, g.Board);
        }

        [TestCase(Directions.Down ,new[]
            {
                0,0,2,0,
                0,0,0,0,
                0,0,0,0,
                2,2,0,0
            })]
        [TestCase(Directions.Up, new[]
            {
                2,2,0,0,
                2,0,0,0,
                0,0,0,0,
                0,0,0,0
            })]
        [TestCase(Directions.Left, new[]
            {
                2,0,0,2,
                2,0,0,0,
                0,0,0,0,
                0,0,0,0
            })]
        [TestCase(Directions.Right, new[]
            {
                0,0,2,2,
                0,0,0,2,
                0,0,0,0,
                0,0,0,0
            })]
        public void Move(Directions direction, int[] expected )
        {
            var random_mock = new DeterministicRandomizer();
            random_mock.Enqueue(new[] { 0, 0,
                4, 0,
                2, 0 });
            var g = new Game(random_mock);
            g.Move(direction);
            CollectionAssert.AreEqual(expected, g.Board);
        }

        [TestCase(Directions.Up, 4, new[] { 0, 3, 0 })]
        [TestCase(Directions.Down, 4, new[] { 0, 3, 0 })]
        [TestCase(Directions.Left, 4, new[] { 0, 0, 0 })]
        [TestCase(Directions.Right, 4, new[] { 0, 0, 0 })]

        public void Score(Directions direction, int expected_score, int[] spawn_pos)
        {
            var random_mock = new DeterministicRandomizer();
            var spawns = spawn_pos.SelectMany(s => new[] { s, 0 });
            random_mock.Enqueue(spawns);
            var g = new Game(random_mock);
            Console.WriteLine(g);
            g.Move(direction);
            Console.WriteLine(g);
            Assert.AreEqual(expected_score, g.Score);
        }

        [Test]
        public void DoNotCreateWithoutMove()
        {
            var random_mock = new DeterministicRandomizer();
            random_mock.Enqueue(new[] { 0, 0, 0, 0, 0, 0 });
            var g = new Game(random_mock);
            g.Move(Directions.Up);
            CollectionAssert.AreEqual(new[]{
                2,2,0,0,
                0,0,0,0,
                0,0,0,0,
                0,0,0,0,
            },g.Board);
        }
        [Test]
        public void Spawn4()
        {
            var random_mock = new DeterministicRandomizer();
            random_mock.Enqueue(new[] { 0, 9, 0, 9 });
            var g = new Game(random_mock);
            CollectionAssert.AreEqual(new[]{
                4,4,0,0,
                0,0,0,0,
                0,0,0,0,
                0,0,0,0,
            }, g.Board);
        }

        [Test]
        public void Spawn4Probability()
        {
            Game g = new Game();
            int two_count = 0;
            int four_count = 0;
            for (int i= 0; i<100000;i++)
            {
                var tile = g.get_new_tile_value();
                if (tile == 2)
                {
                    two_count++;
                }
                else if (tile == 4)
                {
                    four_count++;
                }
                else
                {
                    throw new Exception();
                }

            }
            var ratio = four_count / (double)(two_count + four_count);
            Console.WriteLine(ratio);
            Assert.AreEqual(0.1, ratio, 0.01);
        }

        [Test]
        public void LoadGame()
        {
            var board = new[] {
                0,2,4,8,
                2,4,8,16,
                4,8,16,32,
                8,16,32,64
            };
            g.Load(board, 32);
            Assert.AreEqual(32, g.Score);
            CollectionAssert.AreEqual(board, g.Board);
        }

        [Test]
        public void CheckGameOver()
        {
            var board = new[] {
                2,4,8,0,
                2,4,8,16,
                4,8,16,32,
                8,16,32,64
            };
            random_mock.Enqueue(new[] { 0, 9 });
            g.Load(board, 2);
            Assert.IsFalse(g.GameOver);
            g.Move(Directions.Right);
            Assert.IsTrue(g.GameOver);
        }
    }
}
