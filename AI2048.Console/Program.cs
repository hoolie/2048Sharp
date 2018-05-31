using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI2048.ConsoleApp
{
    class Program
    {
        static Game g;
        static void Main(string[] args)
        {
            g = new Game();
            bool procede = true;
            while(procede)
            {
                draw();

                var command = get_user_input();
                procede = process_user_input(command);
            }
        }

        private static void draw()
        {
            System.Console.Clear();
            for(int i = 0;i<4;i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    var value = g.Board[i * 4 + j];
                    set_console_color(value);
                    Console.Write(value);
                    Console.Write('\t');
                }
                System.Console.WriteLine();
            }
            Console.ResetColor();
            Console.Write(string.Format("Score: {0}", g.Score));
            if (g.GameOver)
            {
                Console.Write(" - Game Over");
            }
        }

        private static void set_console_color(int value)
        {
            var index = value / 2;
            if (index >=colors.Count)
            {
                index = colors.Count - 1;
            }
            Console.ForegroundColor = colors[index];
        }

        private static bool process_user_input(ConsoleKey command)
        {
            switch (command)
            {
                case ConsoleKey.R:
                    g = new Game();
                    break;
                case ConsoleKey.Escape:
                    return false;
                case ConsoleKey.UpArrow:
                    g.Move(Directions.Up);
                    break;
                case ConsoleKey.DownArrow:
                    g.Move(Directions.Down);
                    break;
                case ConsoleKey.LeftArrow:
                    g.Move(Directions.Left);
                    break;
                case ConsoleKey.RightArrow:
                    g.Move(Directions.Right);
                    break;
            }
            return true;
        }

        static List<ConsoleColor> colors = new List<ConsoleColor>()
        {
            ConsoleColor.White,
            ConsoleColor.Green,
            ConsoleColor.Yellow,
            ConsoleColor.DarkBlue,
            ConsoleColor.Cyan,
            ConsoleColor.Gray,
            ConsoleColor.Red
        };

        static List<ConsoleKey> valid_keys = new List<ConsoleKey>()
        {
            ConsoleKey.R,
            ConsoleKey.RightArrow,
            ConsoleKey.LeftArrow,
            ConsoleKey.UpArrow,
            ConsoleKey.DownArrow,
            ConsoleKey.Escape
        };
        
        private static ConsoleKey get_user_input()
        {
            do
            {
                var key = System.Console.ReadKey();
                if (valid_keys.Contains(key.Key))
                {
                    return key.Key;
                }

            }
            while (true);
        }
    }
}
