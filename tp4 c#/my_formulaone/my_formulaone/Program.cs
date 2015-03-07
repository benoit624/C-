using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace my_formulaone
{
    class Program
    {
        public static Random rnd = new Random();
        private static int Mod(int a, int b)
        {
            int r = 0;
            if (a % b != 0)
            {
                r++;
            }
            return a / b + r;
        }
        private static bool[][] string_of_int_array(string input, int width)
        {
            int length = input.Length, height = Mod(length, width), n = 0;
            bool[][] matrix = new bool[height][];
            for (int i = 0; i < height; i++)
            {
                matrix[i] = new bool[width];
                for (int j = 0; j < width; j++)
                {
                    if (n < input.Length)
                    {
                        if (input[n] == '1')
                        {
                            matrix[i][j] = true;
                        }
                        else
                        {
                            matrix[i][j] = false;
                        }
                        n++;
                    }

                }


            }
            return matrix;
        }
        private static int mod(int a, int b)
        {
            if (a % b == 0)
                return 0;
            else
                return 1;
        }
        private static void print(bool[][] matrix, int pos, int posx)
        {
            int height = matrix.Length;
            if (pos + 20 < height)
            {
                height = pos + 20;
            }
            for (int i = pos; i < height; i++)
            {
                int width = matrix[i].Length;
                for (int j = 0; j < width; j++)
                {
                    if (j == posx + 1 && i == pos + 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("╬");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else if (j == posx + 1 && i == pos)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("¥");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else if (Math.Pow((j - posx - 1), 2) + Math.Pow((i - pos - 1), 2) == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("¤");
                        Console.ForegroundColor = ConsoleColor.White;

                    }
                    else if (j == posx + 1 && i == pos + 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("Ö");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        if (matrix[i][j])
                            Console.Write("x");
                        else
                            Console.Write(" ");
                    }

                }
                Console.WriteLine();
            }

        }
        static bool draw_road(bool[][] race, ref int where,int car)
        {
            where++;
            int height = race.Length;
            bool live=true;
            for (int k = 0; k < height; k++)
            {
                int width = race[k].Length;
                for (int l = 0; l < width; l++)
                {
                    if ((Math.Pow((l - car - 1), 2) + Math.Pow((k - where - 1), 2) <= 1) && race[k][l])
                    {
                        live = false;
                    }

                }
            }
            return (race.Length > where + 2) && live;
        }
        private static string gen(int n)
        {
            string str = "";
            Random rnd = new Random();
            for (int i = 0; i < n; i++)
            {
                int al = rnd.Next(2);
                str += al + "" + al + "" + al;


            }
            return str;
        }
        private static string gen2(int n)
        {
            string str = "";
            for (int j = 0; j < 40 * 20; j++)
            {
                str += 0;
            }
            for (int i = 0; i < n; i++)
            {
                
                int m = rnd.Next(17);
                int l = 18;
                for (int k = 0; k < m; k++)
                {
                    for (int j = 0; j < l-k; j++)
                    {
                        str += 1;
                    }
                    for (int j = 0; j < 10; j++)
                    {
                        str += 0;
                    }
                    for (int j = 0; j < 30 - (l-k); j++)
                    {
                        str += 1;
                    }
                }
                for (int k = m; k > 0; k--)
                {
                    for (int j = 0; j < l - k; j++)
                    {
                        str += 1;
                    }
                    for (int j = 0; j < 10; j++)
                    {
                        str += 0;
                    }
                    for (int j = 0; j < 30 - (l - k); j++)
                    {
                        str += 1;
                    }
                }
                
            }
            return str;
        }
        public static void deplacement(object players)
        {
            player player = (player)players;
            ConsoleKeyInfo touche = new ConsoleKeyInfo();
            while (true)
            {
                touche = Console.ReadKey(true);
                if (touche.Key == ConsoleKey.Q)
                    player.posx--;
                if (touche.Key == ConsoleKey.D)
                    player.posx++;
            }
        }
        static void Main(string[] args)
        {
            string str = "1000" + "0010" + "0001";
            int i = 0, j = 0;
            str = gen2(100);
            print(string_of_int_array(str, 40), 0, j);
            bool[][] race = string_of_int_array(str, 40);
            player Player = new player();
            Thread th = new Thread(new ParameterizedThreadStart(deplacement));

            th.Start(Player);
            j = Player.posx;
            while (draw_road(race, ref i,j))
            {
                j = Player.posx;
                Player.posy = i;
                Console.Clear();
                print(string_of_int_array(str, 40), i, j);
                Console.WriteLine(i);
                if (i < 149)//60
                    Thread.Sleep(150 - i);
                //else
                    //Thread.Sleep(90);
                    
            }
            Console.WriteLine("END");
            if (i+2 < race.Length)
            {
                Console.WriteLine("YOU LOOSE!!");
            }
            else
            {
                Console.WriteLine("YOU WIN!!!!!!!!");
            }

        }
    }
}
