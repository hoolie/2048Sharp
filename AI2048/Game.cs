using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AI2048
{
    public class Game
    {
        IRandomizer random;
        List<int> board;
        Dictionary<Directions, Func<List<int>, int>> movement_dictionary =
            new Dictionary<Directions, Func<List<int>, int>>
        {
            {Directions.Up,Movement.MoveUp },
            {Directions.Down,Movement.MoveDown },
            {Directions.Left,Movement.MoveLeft },
            {Directions.Right,Movement.MoveRight }
        };
        public Game() : this(new Randomizer()) { }
        internal Game(IRandomizer randomizer)
        {
            random = randomizer;
            Reset();
        }

        public void Reset()
        {
            Score = 0;
            board = new List<int>(new int[16]);
            place_tile();
            place_tile();
        }

        private void place_tile()
        {
            var available_tiles = board.Select((s, i) => i).Where(s => board[s] == 0);
            var selected = random.GenerateValue(available_tiles.Count() - 1);
            var new_tile = available_tiles.ElementAt(selected);
            board[new_tile] = get_new_tile_value();
        }

        internal int get_new_tile_value()
        {

            var number_probability = random.GenerateValue(9);
            return number_probability == 9 ? 4 : 2;
        }

        public int[] Board { get { return board.ToArray(); } }

        public int Score { get; private set; }
        public bool GameOver
        {
            get
            {
                var tmp_board = board.ToList();
                foreach(var movement in Enum.GetValues(typeof(Directions)))
                {

                    if (move_board((Directions)movement,tmp_board))
                        return false;
                }
                return true;
            }
        }

        public bool Move(Directions direction)
        {
            if ( move_board(direction,board))
            {
                place_tile();
                return true;
            }
            return false;
        }

        private bool move_board(Directions direction, List<int> target_board)
        {
            var movement = movement_dictionary[direction];
            var previous_field = target_board.ToArray();

            var points =  movement(target_board);
            if (target_board == board)
            { Score += points; }
            if (previous_field.SequenceEqual(target_board))
            {
                return false;
            }
            return true;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 4; i++)
            {
                var vars = board.Skip(i*4).Take(4);
                sb.AppendFormat("{0}\t{1}\t{2}\t{3}\r\n", vars.Cast<object>().ToArray());
            }
            sb.AppendFormat("Score: {0}", Score);
            if (GameOver)
            {
                sb.Append(" - GameOver");
            }
            return sb.ToString();
        }

        public void Load(int[] board, int score)
        {
            this.board = new List<int>(board);
            Score = score;
        }
    }
}