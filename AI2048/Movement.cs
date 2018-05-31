using System;
using System.Collections.Generic;
using System.Linq;

namespace AI2048
{
    public class Movement
    {
        public static int Move(List<int> list)
        {

            move_line(list);
            int score = merge_line(list);
            move_line(list);
            return score;
        }

        private static int merge_line(IList<int> list)
        {
            int score = 0;
            for (int i = 1; i < list.Count; i++)
            {
                if (list[i - 1] == list[i])
                {
                    list[i - 1] = list[i - 1] + list[i];
                    score += list[i - 1];
                    list[i] = 0;
                }
            }
            return score;
        }

        public static int MoveLeft(List<int> test_field)
        {
            var split = split_lines(test_field);
            var score = move_field(split);
            test_field.Clear();
            test_field.AddRange(split.SelectMany((s) => s));
            return score;
        }

        public static int MoveRight(List<int> test_field)
        {
            var split = split_lines(test_field);
            var score = move_field(split, true);
            test_field.Clear();
            test_field.AddRange(split.SelectMany((s) => s));
            return score;
        }

        private static List<List<int>> split_lines(List<int> test_field)
        {
            int border_length = get_border_length(test_field);

            return test_field.Select((s, i) => new { Value = s, Index = i })
                     .GroupBy(item => item.Index / get_border_length(test_field), item => item.Value)
                     .Select(a => a.ToList()).ToList();
        }

        private static int get_border_length(List<int> test_field)
        {
            var square_root = Math.Sqrt(test_field.Count());
            return (int)Math.Floor(square_root);
        }

        public static int MoveUp(List<int> test_field)
        {
            var split = rotate(test_field);
            var score = MoveLeft(split);
            split = rotate(split);
            test_field.Clear();
            test_field.AddRange(split);
            return score;
        }

        private static List<int> rotate(IEnumerable<int> test_field)
        {
            var border_length = Math.Sqrt(test_field.Count());
            
            return test_field.Select((s, i) => new { Value = s, Index = i })
                .OrderBy((a) => a.Index % Math.Floor(border_length))
                .Select(a => a.Value)
                .ToList();
                     
        }

        public static int MoveDown(List<int> test_field)
        {
            var split = rotate(test_field);
            var score = MoveRight(split);
            split = rotate(split);
            test_field.Clear();
            test_field.AddRange(split);
            return score;
        }


        private static int move_field(List<List<int>> split, bool reverse = false)
        {
            var new_field = new List<List<int>>();
            int score = 0;

            foreach (var line in split)
            {
                var list = line.ToList();
                if (reverse)
                {
                    list.Reverse();
                    score+=Move(list);
                    list.Reverse();
                }
                else
                {
                    score+=Move(list);
                }
                new_field.Add(list);
            }
            split.Clear();
            split.AddRange(new_field);
            return score;
        }


        private static void move_line(IList<int> list)
        {
            IEnumerable<int> movable_indexes;
            do
            {
                movable_indexes = Enumerable.Range(1, list.Count - 1)
                    .Where(i => is_element_movable(list, i));
                foreach (var index in movable_indexes)
                {
                    list[index - 1] = list[index];
                    list[index] = 0;
                }
            } while (movable_indexes.Count() != 0);
        }

        private static bool is_element_movable(IList<int> list, int index)
        {
            return list[index] != 0 && list[index - 1] == 0;
        }
    }
}